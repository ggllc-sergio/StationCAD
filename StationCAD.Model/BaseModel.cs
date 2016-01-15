using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
