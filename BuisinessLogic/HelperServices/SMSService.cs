using KarmaSMSCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisinessLogic.HelperServices
{
    public interface ISMSService
    {
        void SendSMS(string phoneNumber);
    }
    public class SMSService : ISMSService
    {
        private readonly IConfiguration _configuration;
        public SMSService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendSMS(string phoneNumber)
        {
            SOAPKarmaSMSSoapClient smsClient = new SOAPKarmaSMSSoapClient(SOAPKarmaSMSSoapClient.EndpointConfiguration.SOAPKarmaSMSSoap12);
            ArrayOfString strArr = new ArrayOfString();
            strArr.Add(phoneNumber);
            smsClient.SendSMSAsync(strArr, getMessage(), _configuration["SMS:Sender"], 1, _configuration["SMS:Username"], _configuration["SMS:Password"]);
        }

        private string getMessage()
        {
            return "Hi From Wallet API";
        }
    }

    
}
