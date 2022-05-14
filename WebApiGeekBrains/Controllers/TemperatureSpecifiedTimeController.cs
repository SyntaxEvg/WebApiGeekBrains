using Microsoft.AspNetCore.Mvc;
using System;
using WebApiGeekBrains.Data.InMemory.Model;

namespace WebApiGeekBrains.Controllers
{

    /// <summary>
    /// 
    //Возможность сохранить температуру в указанное время
    //Возможность отредактировать показатель температуры в указанное время
    //Возможность удалить показатель температуры в указанный промежуток времени
    //Возможность прочитать список показателей температуры за указанный промежуток времени

    /// </summary>
    /// 
    
    public class TemperatureSpecifiedTimeController : Controller
    {
        private readonly ITemperature _temperatureModel;

        public TemperatureSpecifiedTimeController(ITemperature temperatureModel)
        {
            _temperatureModel = temperatureModel;
        }


       
        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            _temperatureModel.AddValue(date, temperature);
            return Ok();
        }
        [HttpGet("all")]
        public IActionResult ReadAll()
        {
            return Ok(_temperatureModel.GetTemperatureValues());
        }
        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return Ok(_temperatureModel.GetTemperatureValues(from, to));
        }
       
        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            _temperatureModel.UpdateValue(date, temperature);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime date)
        {
            _temperatureModel.DeleteValue(date);
            return Ok();
        }
        [HttpDelete("DeleteRange")]
        public IActionResult Delete([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            _temperatureModel.DeleteRange(from, to);
            return Ok();
        }



    }
}
