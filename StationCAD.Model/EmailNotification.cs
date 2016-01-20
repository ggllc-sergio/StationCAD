using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public class EmailNotification
    {

        public string OrganizationName { get; set; }

        public string OrganizationEmail
        {
            get
            { return OrganizationName.Replace(" ", "_"); }
            set
            { OrganizationEmail = value; }
        }

        public string Recipient { get; set; }

        public string MessageSubject { get; set; }

        public string MessageBody { get; set; }

    }
}
