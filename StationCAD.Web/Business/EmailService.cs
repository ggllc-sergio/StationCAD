using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using StationCAD.Processor.Notifications;

namespace StationCAD.Web.Business
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendasync(message);
        }
        
        private async Task configSendasync(IdentityMessage message)
        {
            Email.SendEmailMessage(message.Destination, message.Subject, message.Body, message.Body);
        }
    }
}