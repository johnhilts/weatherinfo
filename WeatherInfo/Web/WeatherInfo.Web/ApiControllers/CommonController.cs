using Common;
using System;
using System.Web.Http;

namespace WeatherInfo.Web.ApiControllers
{
    public class CommonController : BaseApiController
    {
        [HttpGet]
        public string QueryTimeText(DateTime queryTime)
        {
            var helper = new DateTimeHelper();
            return helper.GetTimeText(queryTime, DateTime.Now);
        }
    }
}
