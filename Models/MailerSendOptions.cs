using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaobabBackEndService.Models
{
    public class MailerSendOptions
    {
        public string ApiUrl { get; set; }

        public string ApiKey { get; set;}

        public string FromEmail { get; set; }

        public string FromName { get; set; }
        
    }
}