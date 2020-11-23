using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApp.Services
{
    public interface IMessageService
    {
        string Send();
    }
    public class EmailMessService : IMessageService
    {
        public string Send()
        {
            return "Send to Email.";
        }
    }
    public class SmsMessService : IMessageService
    {
        public string Send()
        {
            return "Send to sms.";
        }
    }
}
