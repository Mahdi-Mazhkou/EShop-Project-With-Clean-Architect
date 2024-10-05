using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public  interface IEmailSender
    {
        bool SendEmail(string to, string subject, string body);
    }
}
