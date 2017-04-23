using System.Web.Mvc;

namespace GarageWeb.Models
{
    public class BusketModelBinder : IModelBinder
    {
        private const string Key = "Bucket";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Busket bucket = (Busket)controllerContext.HttpContext.Session[Key];

            if (bucket == null)
            {
                bucket = new Busket();
                controllerContext.HttpContext.Session[Key] = bucket;
            }
            return bucket;
        }
    }
}