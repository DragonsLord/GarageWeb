using System.Web.Mvc;

namespace GarageWeb.Models
{
    public class BasketModelBinder : IModelBinder
    {
        private const string Key = "Basket";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Basket basket = (Basket)controllerContext.HttpContext.Session[Key];

            if (basket == null)
            {
                basket = new Basket();
                controllerContext.HttpContext.Session[Key] = basket;
            }
            return basket;
        }
    }
}