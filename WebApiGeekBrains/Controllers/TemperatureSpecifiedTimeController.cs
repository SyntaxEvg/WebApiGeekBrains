using Microsoft.AspNetCore.Mvc;

namespace WebApiGeekBrains.Controllers
{

    /// <summary>
    /// 
    //Возможность сохранить температуру в указанное время
    //Возможность отредактировать показатель температуры в указанное время
    //Возможность удалить показатель температуры в указанный промежуток времени
    //Возможность прочитать список показателей температуры за указанный промежуток времени

    /// </summary>
    public class TemperatureSpecifiedTimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
