using Microsoft.AspNetCore.Mvc;
using ShoppingWEBUI.Core.ViewModel;
using ShoppingWEBUI.Helper.SessionHelper;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("/Admin/Anasayfa")]
        public IActionResult Index()
        {

            UserViewModel user = new()
            {
                AdSoyad = SessionManager.LoggedUser.AdSoyad,
                ID =Convert.ToInt32(SessionManager.LoggedUser.UserID),
                EPosta = SessionManager.LoggedUser.EPosta,
                Adres = SessionManager.LoggedUser.Adres,
            };

         
            return View(user);

        }
    }
}
