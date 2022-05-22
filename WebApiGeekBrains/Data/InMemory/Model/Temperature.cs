using System;
using System.Collections.Generic;
using System.Linq;
using WebApiGeekBrains.BLL;

namespace WebApiGeekBrains.Data.InMemory.Model
{

    public class Temperature : ITemperature
    {
        private Dictionary<DateTime, int> _data = new Dictionary<DateTime, int>();
        public Temperature()
        {
            GeneratorDateTime();
        }

        private void GeneratorDateTime()
        {
            Random random = new Random();
            RandomDateTime date = new RandomDateTime(random);

            for (int i = 0; i < 20; i++)
            {
                var dates =date.GeNData;
                if (!_data.ContainsKey(dates))
                {
                    _data.Add(dates, random.Next(1, 15));
                }

            }
        }

        public void UpdateValue(DateTime date, int temperature) => UpdateValue(date, temperature, out _);
        public void AddValue(DateTime date, int temperature)
        {
            UpdateValue(date, temperature, out bool isUpdated);

            if (isUpdated)
                return;

            _data.Add(date, temperature);
        }
        public void DeleteValue(DateTime date)
        {
            if (_data.ContainsKey(date))
            {
                _data.Remove(date);
            }
        }
        public void DeleteRange(DateTime from, DateTime to)
        {
            _data = _data.Where(data => data.Key < from || data.Key > to).ToDictionary(
                key => key.Key,
                value => value.Value, EqualityComparer<DateTime>.Default);
        }
        public IEnumerable<int> GetTemperatureValues(DateTime from, DateTime to)
        {
            foreach (var item in _data.Where(data => data.Key >= from && data.Key <= to))
            {
                yield return item.Value;
            }
        }
        public IEnumerable<int> GetTemperatureValues()
        {
            foreach (var item in _data)
            {
                yield return item.Value;
            }
        }
        private void UpdateValue(DateTime date, int temperature, out bool success)
        {
            success = false;
            if (_data.ContainsKey(date))
            {
                _data[date] = temperature;
                success = true;
            }
        }

    }
}