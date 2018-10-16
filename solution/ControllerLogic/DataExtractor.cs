using System;
using System.Linq;
using System.Xml;
using solution.Models;

namespace solution.ControllerLogic
{
    public class DataExtractor : IDataExtractor
    {
        private const decimal TaxRate = 0.15M;
        public ApiResult<ViewModel> GetDataFromText(string data)
        {
            if(string.IsNullOrEmpty(data)) return ApiResult<ViewModel>.UnsuccessfulResult("The input data can't be empty!");
            var xml = string.Format("<root>{0}</root>", data);
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch (Exception e)
            {
                return ApiResult<ViewModel>.UnsuccessfulResult($"Parse failed, the input data should be XML-like content or XML style opening and closing tags. Error message {e.ToString()}");
            }
            decimal total = 0M;
            if(doc.GetElementsByTagName("total").Count == 0
               || string.IsNullOrEmpty(doc.GetElementsByTagName("total").Cast<XmlNode>().FirstOrDefault()?.InnerText)
               || !decimal.TryParse(doc.GetElementsByTagName("total").Cast<XmlNode>().FirstOrDefault()?.InnerText, out total)) return ApiResult<ViewModel>.UnsuccessfulResult("Missing Total");
            var gst = calculateGst(total);
            var result = new ViewModel
            {
                CostCentre = string.IsNullOrEmpty(doc.GetElementsByTagName("cost_centre").Cast<XmlNode>().FirstOrDefault()?.InnerText) ? "UNKNOWN" : doc.GetElementsByTagName("cost_centre").Cast<XmlNode>().FirstOrDefault()?.InnerText,
                Total = total,
                PaymentMethod = doc.GetElementsByTagName("payment_method").Cast<XmlNode>().FirstOrDefault()?.InnerText,
                Vendor = doc.GetElementsByTagName("vendor").Cast<XmlNode>().FirstOrDefault()?.InnerText,
                Description = doc.GetElementsByTagName("description").Cast<XmlNode>().FirstOrDefault()?.InnerText,
                Date = string.IsNullOrEmpty(doc.GetElementsByTagName("date").Cast<XmlNode>().FirstOrDefault()?.InnerText) || !DateTime.TryParse(doc.GetElementsByTagName("date").Cast<XmlNode>().FirstOrDefault()?.InnerText, out var date) ? new DateTime(2018,1,1) : date,
                GST = gst,
                SubTotal = total - gst
            };
            return ApiResult<ViewModel>.SuccessfulResult(result);
        }

        private decimal calculateGst(decimal total)
        {
            return total - total / (1 + TaxRate);
        }
    }
}