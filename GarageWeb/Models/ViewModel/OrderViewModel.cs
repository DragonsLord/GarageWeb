using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GarageWeb.Models.ViewModel
{
    public class OrderViewModel
    {
        [Display(Name = "ПІБ")]
        public string Name { get; set; }

        [Display(Name = "Адреса для доставки")]
        public string DeliveryAddress { get; set; }

        [Display(Name="Телефон")]
        public string Phone { get; set; }

        public double ToPay { get; set; }

        public byte TargetHour { get; set; }

        public byte TargetMinute { get; set; }
        
        public string Card1 { get; set; }
        public string Card2 { get; set; }
        public string Card3 { get; set; }
        public string Card4 { get; set; }

        public string Card => new System.Text.StringBuilder(Card1).Append(Card2).Append(Card3).Append(Card4).ToString();

        public string CardCVV { get; set; }

        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }

        public string GetUserIP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
    }
}