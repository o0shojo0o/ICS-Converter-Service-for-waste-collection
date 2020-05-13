using ICS_Converter_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Converter_Service
{
    public class CSVToolbox
    {
        public CSVToolbox()
        { }

        public string EventsObjToCSVString(List<Event> _events)
        {
            var _csvString = new StringBuilder();

            // Header selektieren
            var _header = _events
                .GroupBy(x => x.Name)
                .OrderBy(x => x.Key)
                .Select(x => x.Key)
                .ToList();

            // Anhand des Headers die Gruppen bilden
            var _groupList = new List<List<Event>>();
            _header.ForEach(x =>
            {
                _groupList.Add(
                    _events
                    .Where(y => y.Name == x)
                    // Innerhalb der Gruppe das Datum aufsteigend sortieren
                    .OrderBy(y => y.Start)
                    .ToList());
            });

            // Die Länge bzw. die Menge der Datensätze der CSV ermitteln 
            var _maxCount = 0;
            _groupList.ForEach(x =>
            {
                // Prüfen ob die Gruppe mehr Datensätze hat wie der aktuelle maxCount gross ist 
                if (x.Count > _maxCount)
                {
                    _maxCount = x.Count;
                }
            });


            // CSV Header erstellen
            _csvString.AppendLine(String.Join(";", _header));

            // Datensätze / Rows erstellen
            for (int i = 0; i < _maxCount; i++)
            {
                // Pro Row aus jeder Gruppe ein Datensatz
                foreach (var x in _groupList)
                {
                    // Hat die Gruppe noch einen Datensatz?
                    if (x.Count <= i)
                    {
                        // Wenn kein Datensatz mehr vorhanden ist nichts hinzufügen 
                        _csvString.Append("");
                    }
                    else
                    {
                        // Wenn vorhanden Datensatz schreiben
                        _csvString.Append(x[i].Start.ToShortDateString());
                    }

                    // War es die Letzte Gruppe die Row beenden
                    if (_groupList.IndexOf(x) == _groupList.Count() - 1)
                    {
                        _csvString.Append(Environment.NewLine);
                    }
                    // Wenn es nicht die letzte Gruppe ist, ein Trenner einfügen
                    else
                    {
                        _csvString.Append(";");
                    }
                }
            }

            return _csvString.ToString();
        }
    }
}
