using System;

namespace solution.Models
{
    public class ViewModel
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GST { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}