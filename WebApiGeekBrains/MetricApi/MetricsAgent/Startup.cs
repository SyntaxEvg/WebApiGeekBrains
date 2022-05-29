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
using Quartz.Spi;
using MetricsAgent.Jobs.JobFactory;
using Quartz;
using Quartz.Impl;
using MetricsAgent.Jobs;
using MetricsAgent.Jobs.Schedule;
using MetricsAgent.Services;
using AutoMapper;
using Dapper;

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
           
            services.AddControllers().AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter())); ;
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            AddTaskJob(ref services);
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<IDatabaseSettingsProvider, DatabaseSettingsProvider>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.FirstOrDefault());
            });
            //создание и добавление
            services.AddSingleton(ConfigureMapper(services));
        }

        /// <summary>
        /// startzadacha
        /// </summary>
        /// <param name="services"></param>
        private void AddTaskJob(ref IServiceCollection services)
        {
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricJob),
                cronExpression: "0/5 * * * * ?")); // запускать каждые 5 секунд

            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<HddMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();
        }

        private IMapper ConfigureMapper(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();          
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
            return mapper;
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
                // ”дал€ем таблицу с метриками, если она есть в базе данных
                foreach (var item in MetricList)
                {
                    command.CommandText = $"DROP TABLE IF EXISTS {item}";
                    // ќтправл€ем запрос в базу данных
                    command.ExecuteNonQuery();
                    command.CommandText =
                        $"CREATE TABLE {item}(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();
                    //данные за последние 10 дней
                    //перед запуском нужно выставить текущее число +1 и 10 - 1
                }

                int i = 0;
                foreach (var item in MetricList)
                {

                    command.CommandText = $"INSERT INTO {item}(value, time) VALUES(@value, @time)";

                    command.Parameters.AddWithValue(
                        "@value",
                        rnd.Next(0, 100));
                    command.Parameters.AddWithValue(
                        "@time",
                        DateTimeOffset.Now.ToUnixTimeSeconds() - 86400 * ++i);

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
