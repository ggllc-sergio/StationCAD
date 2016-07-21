using System;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Linq.Expressions;

using StationCAD.Model;

namespace StationCAD.Processor
{
    public class DispatchManager <T>
        where T : DispatchEvent
    {

        public void ProcessEvent(string rawEvent)
        {

            // Parse the raw message

            // Persist to the Database

            // Create Notifications

            // Task Parallel Library - Send notifications
        }

        public DispatchEvent ParseEventText(string rawMessage)
        {
            ChesCoPAEventMessage result = new ChesCoPAEventMessage();

            if (rawMessage.Length > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ChesCoPAEventMessage));
                var lines = rawMessage.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                PropertyDescriptor prop = null;
                PropertyDescriptor currentProperty = null;
                PropertyDescriptor previousProperty = null;
                string propertyName = string.Empty;
                string eventAddress = string.Empty;

                foreach(string line in lines)
                {
                    if (result.Title == null)
                    { result.Title = line; }
                    else
                    {
                        var pieces = line.Split(new[] { ':' }, 2);
                        propertyName = (pieces.Count() > 1 ? pieces[0] : propertyName);
                        foreach (PropertyDescriptor property in properties)
                        {
                            // Look for an existing property
                            if (property.DisplayName == propertyName)
                            {
                                currentProperty = property;
                                prop = property;
                            }
                        }
                        if (prop == null && previousProperty != null)
                            prop = previousProperty;

                        if (prop != null)
                        {
                            switch (prop.DisplayName)
                            {
                                case "Address":
                                    if (pieces.Count() == 1)
                                    {
                                        eventAddress = string.Format("{0} {1}", eventAddress, line);
                                        prop.SetValue(result, eventAddress);
                                    }
                                    break;
                                case "Units":
                                    var unitPieces = line.Split(new[] { '\t' });
                                    if (unitPieces.Count() == 3)
                                        result.Units.Add(new UnitEntry { Unit = unitPieces[0].Trim(), Disposition = unitPieces[1].Trim(), TimeStamp = unitPieces[2].Trim() });
                                    break;
                                case "Event Comments":
                                    var commentPieces = line.Split(new[] { '-' }, 2);
                                    if (commentPieces.Count() == 2)
                                        result.Comments.Add(new EventComment { TimeStamp = commentPieces[0].Trim(), Comment = commentPieces[1].Trim() });
                                    break;
                                default:
                                    if (pieces.Count() > 1 && pieces[1].Length > 0)
                                    {
                                        prop.SetValue(result, pieces[1].Trim());
                                        previousProperty = currentProperty;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        protected void ParseLine(string line, ref DispatchEvent eventMessage)
        {

        }

    }


}
