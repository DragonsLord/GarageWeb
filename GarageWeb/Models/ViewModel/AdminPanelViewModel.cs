using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageWeb.Models.ViewModel
{
    public class AdminPanelViewModel
    {
        public string Message { get; set; }

        public ChangeLoginViewModel ChangeLoginBlock { get; set; }
    }
}