using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb
{
    public static class Constants
    {
        public static string ApiBaseUrl = "https://localhost:44306/";
        public static string ApiDepartment = ApiBaseUrl + "api/v1/Department";
        public static string ApiWeather = ApiBaseUrl + "api/v2/WeatherForecast";
        public static string ApiUser = ApiBaseUrl + "api/v1/Users/";

    }
}
