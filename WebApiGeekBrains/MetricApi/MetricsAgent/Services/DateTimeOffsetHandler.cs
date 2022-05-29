using Dapper;
using System;
using System.Data;

namespace MetricsAgent.Services
{
    /// <summary>
    /// Обработчик времени parse times
    /// </summary>
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
           => DateTimeOffset.FromUnixTimeSeconds((long)value);

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
            => parameter.Value = value;
    }
}
