using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MetricsAgent.DataAccessLayer;

namespace MetricsAgent
{
    public class Startup
    {
        public IConfiguration _Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            string connectionString = _Configuration.GetConnectionString("LocalDB"); 
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<IDatabaseSettingsProvider, DatabaseSettingsProvider>();
            services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });
            });
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            List<string> MetricList = new()
            {
                "cpumetrics",
                "dotnetmetrics",
                "hddmetrics",
                "networkmetrics",
                "rammetrics"

            };
            var rnd = new Random();
            using (var command = new SQLiteCommand(connection))
            {
                // «адаЄм новый текст команды дл€ выполнени€
                // ”дал€ем таблицу с метриками, если она есть в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // ќтправл€ем запрос в базу данных
                command.ExecuteNonQuery();
                command.CommandText =
                    @"CREATE TABLE cpumetrics(id INTEGER
                    PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                //данные за последние 10 дней
                //перед запуском нужно выставить текущее число +1 и 10 - 1
                int i = 0;
                foreach (var item in MetricList)
                {

                    command.CommandText = $"INSERT INTO {item}(value, time) VALUES(@value, @time)";

                    command.Parameters.AddWithValue(
                        "@value",
                        rnd.Next(0, 100));
                    command.Parameters.AddWithValue(
                        "@time",
                        DateTimeOffset.Now.ToUnixTimeSeconds() - 86_400 * ++i);

                    command.Prepare();
                    command.ExecuteNonQuery();
                }

               
                
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
