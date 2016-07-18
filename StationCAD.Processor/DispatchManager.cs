using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using System.Linq.Expressions;

namespace StationCAD.Processor
{
    public class DispatchManager
    {
        public ChesCoPAEventMessage ParseEventText(string rawMessage)
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
                                    result.Comments.Add(new EventComment { Comment = commentPieces[0].Trim(), TimeStamp = commentPieces[1].Trim() });
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

            return result;
        }

        protected void ParseLine(string line, ref ChesCoPAEventMessage eventMessage)
        {

        }

    }

    public class ChesCoPAEventMessage
    {
        public ChesCoPAEventMessage()
        {
            this.Units = new List<UnitEntry>();
            this.Comments = new List<EventComment>();
        }

        public string Title { get; set; }

        [DisplayName("Call Time")]
        public string CallTime { get; set; }

        [DisplayName("Event")]
        public string Event { get; set; }

        [DisplayName("Event Type Code")]
        public string EventTypeCode { get; set; }

        [DisplayName("Event SubType Code")]
        public string EventSubTypeCode { get; set; }

        [DisplayName("ESZ")]
        public string ESZ { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Cross Street")]
        public string CrossStreet { get; set; }

        [DisplayName("Location Information")]
        public string LocationInformaiton { get; set; }

        [DisplayName("Development")]
        public string Development { get; set; }

        [DisplayName("Municipality")]
        public string Municipality { get; set; }

        [DisplayName("Caller Information")]
        public string CallerInformation { get; set; }

        [DisplayName("Caller Name")]
        public string CallerName { get; set; }

        [DisplayName("Caller Phone Number")]
        public string CallerPhoneNumber { get; set; }

        [DisplayName("Alt Phone Number")]
        public string AltPhoneNumber { get; set; }

        [DisplayName("Caller Address")]
        public string CallerAddress { get; set; }

        [DisplayName("Caller Source")]
        public string CallerSource { get; set; }

        public List<UnitEntry> Units { get; set; }

        [DisplayName("Event Comments")]
        public List<EventComment> Comments { get; set; }
    }

    public class UnitEntry
    {
        public string Unit { get; set; }
        public string Disposition { get; set; }
        public string TimeStamp { get; set; }
    }

    public class EventComment
    {
        public string TimeStamp { get; set; }
        public string Comment { get; set; }
    }

    public static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
        where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }

            return (T)attribute;
        }

        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (attr == null)
            {
                return memberInfo.Name;
            }

            return attr.DisplayName;
        }

        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }
    }
}
