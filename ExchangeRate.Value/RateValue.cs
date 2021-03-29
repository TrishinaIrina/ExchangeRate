using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Value
{
    public class RateValue
    {
        public string currency { get; set; }
        public double curValue { get; set; }
        public DateTime createDate { get; set; }

        public RateValue() { }
       
        public RateValue(string unit)
        {
            try
            {
                currency = unit;
                createDate = DateTime.Now;
                ServiceRateValue request = new ServiceRateValue();
                int unitCode = 0;
                if (unit == "рубль") unitCode = 16;
                else if (unit == "юань") unitCode = 8;
                else if (unit == "доллар") unitCode = 5;
                curValue = request.GetValue(unitCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public override string ToString()
        {
            return string.Format("{0} США $ — {1} тнг.     обновление базы данных: {2:dd.MM.yyyy, HH:mm:ss}", currency, curValue, createDate);
        }
    }
}



