using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model.Notifications.Mailgun
{
    public class SMSEmailNotification : EmailNotification
    {

        public string MobileNumber { get; set; }

        public MobileCarrier Carrier { get; set; }
        
        public string SMSEmailRecipient
        {
            get
            {
                string domain = string.Empty;
                switch (Carrier)
                {
                    case MobileCarrier.ATT:
                    case MobileCarrier.Cingular:
                    case MobileCarrier.Tracfone:
                        /// AT&T, Cingular, Tracfone: 10digitphonenumber@txt.att.net
                        domain = "{0}@txt.att.net";
                        break;
                    case MobileCarrier.CellularOne:
                        /// Cellular One: 10digitphonenumber@mobile.celloneusa.com
                        domain = "{0}@mobile.celloneusa.com";

                        break;
                    case MobileCarrier.MetroPCS:
                        /// MetroPCS: 10digitphonenumber@mymetropcs.com
                        domain = "{0}@mymetropcs.com";

                        break;
                    case MobileCarrier.Nextel:
                        /// Nextel: 10digitphonenumber@messaging.nextel.com
                        domain = "{0}@messaging.nextel.com";

                        break;
                    case MobileCarrier.Sprint:
                        /// Sprint: 10digitphonenumber@messaging.sprintpcs.com
                        domain = "{0}@messaging.sprintpcs.com";

                        break;
                    case MobileCarrier.TMobile:
                        /// T-Mobile: 10digitphonenumber@tmomail.net
                        domain = "{0}@tmomail.net";

                        break;
                    case MobileCarrier.Verizon:
                        /// Verizon: 10digitphonenumber@vtext.com
                        domain = "{0}@vtext.com";

                        break;
                    case MobileCarrier.VirginMobile:
                        /// Virgin Mobile: 10digitphonenumber@vmobl.com
                        domain = "{0}@vmobl.com";

                        break;
                    default:
                        throw new ApplicationException("Carrier not recognized.");
                }
                return string.Format(domain, this.MobileNumber);
            }
        }
        

    }

}
