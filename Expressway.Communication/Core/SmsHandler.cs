using Expressway.Contracts.Communication;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Expressway.Communication.Core
{
    public class SmsHandler : ISmsHandler
    {

        public SmsHandler()
        {
        }

        public async Task<bool> SendMessage(string recipient, string message)
        {
            try
            {
                WebClient client = new WebClient();

                client.QueryString.Add("id", "94712265669");
                client.QueryString.Add("pw", "1334");
                client.QueryString.Add("to", recipient);
                client.QueryString.Add("text", message);
                string baseurl = "http://www.textit.biz/sendmsg";
                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
