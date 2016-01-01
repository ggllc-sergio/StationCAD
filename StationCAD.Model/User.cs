using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public class User
    {

        public Guid ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid OrganizationID { get; set; }
        public string IdentificationNumber { get; set; }

    }


}
