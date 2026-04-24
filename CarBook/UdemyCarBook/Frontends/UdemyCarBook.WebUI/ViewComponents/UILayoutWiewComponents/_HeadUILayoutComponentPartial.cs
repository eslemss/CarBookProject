using Microsoft.AspNetCore.Mvc;

namespace UdemyCarBook.WebUI.ViewComponents.UILayoutWiewComponents
{
    public class _HeadUILayoutComponentPartial : ViewComponent
    {
       public IViewComponentResult Invoke()
       {
          return View();
       }
    }
}
