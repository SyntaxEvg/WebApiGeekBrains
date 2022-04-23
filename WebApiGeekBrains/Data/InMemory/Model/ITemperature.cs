using System;
using System.Collections.Generic;

namespace WebApiGeekBrains.Data.InMemory.Model
{
    public interface ITemperature
    {
        void AddValue(DateTime date, int temperature);
        void DeleteRange(DateTime from, DateTime to);
        void DeleteValue(DateTime date);
        IEnumerable<int> GetTemperatureValues();
        IEnumerable<int> GetTemperatureValues(DateTime from, DateTime to);
        void UpdateValue(DateTime date, int temperature);
    }
}