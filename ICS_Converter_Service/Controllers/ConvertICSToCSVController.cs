using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;
using ICS_Converter_Service.Models;
using System.Globalization;

namespace ICS_Converter_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertICSToCSVController : ControllerBase
    {
        [HttpGet]
        public FileContentResult Get(string url)
        {
            var _icalString = url.GetStringAsync().Result;

            var _eventsObj = new ICSToolbox().ICSStringToEventsObj(_icalString);

            var _csvString = new CSVToolbox().EventsObjToCSVString(_eventsObj);


            return File(new System.Text.ASCIIEncoding().GetBytes(_csvString), "text/csv", "data.csv");
        }
    }
}