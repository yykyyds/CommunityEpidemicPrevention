using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IEmailService
    {
        void SendConfig(string EmailSubject, string EmailTo, string EmialContent, string? path = null);
        void AddAttchment(string path);
        bool SendEmail();
    }
}
