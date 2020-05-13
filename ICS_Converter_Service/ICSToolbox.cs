using ICS_Converter_Service.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ICS_Converter_Service
{
    public class ICSToolbox
    {
        public ICSToolbox()
        {
        }

        public List<Event> ICSStringToEventsObj(string _icalString)
        {
            var _separator = "";
            if (_icalString.Contains("\r\n"))
            {
                _separator = "\r\n";
            }
            else
            {
                _separator = "\r";
            }


            var _lines = _icalString.Split(_separator).ToList();
            var _events = new List<Event>();
            var _newEvent = false;
            Event _tempEvent = new Event();

            _lines.ForEach(x =>
            {
                if (x == "BEGIN:VEVENT")
                {
                    _newEvent = true;
                    _tempEvent = new Event();
                }

                if (x == "END:VEVENT")
                {
                    _newEvent = false;
                    _events.Add(_tempEvent);
                }

                if (_newEvent)
                {
                    if (x.StartsWith("DTSTART"))
                    {
                        _tempEvent.Start = DateTime.ParseExact(x.Split(":")[1], "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    else if (x.StartsWith("SUMMARY"))
                    {
                        _tempEvent.Name = x.Split(":")[1];
                    }
                    //else if (x.StartsWith("DTEND"))
                    //{
                    //    _tempEvent.End = DateTime.ParseExact(x.Split(":")[1], "yyyymmdd", CultureInfo.InvariantCulture);
                    //}
                }
            });

            return _events;
        }
    }
}
