using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace ExchangeRate.Value
{
    public class ServiceRateValue
    {
        
        public double GetValue(int unitCode)
        {
            string data = string.Empty;
            DateTime today = DateTime.Now;
            data = today.Date.ToShortDateString();
            
            string url = string.Format("https://nationalbank.kz/ru/exchangerates/ezhednevnye-oficialnye-rynochnye-kursy-valyut/report?rates%5B%5D={0}&beginDate={1}&endDate={1}", unitCode, data);
           
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse myhttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            StreamReader myStreamReader = new StreamReader(myhttpWebResponse.GetResponseStream());

            string html = myStreamReader.ReadToEnd();
            string[] arr = html.Split(new string[] { "<td class=" }, StringSplitOptions.None);
            string Value = arr[3].Substring(14, 6);
            Value = Value.Replace('.', ',');
            double value = Convert.ToDouble(Value);
            return value;
        }

     
    }
}
