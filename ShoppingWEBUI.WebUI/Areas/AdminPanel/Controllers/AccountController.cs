using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ShoppingWEBUI.Core.DTO;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Core.ViewModel;
using ShoppingWEBUI.Helper.SessionHelper;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Security.AccessControl;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        [HttpGet("/AdminAccount/Login")]
        public IActionResult Index()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return View();
        }

        [HttpPost("/Account/AdminLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginDTO loginDTO)
        {
            var url = "http://localhost:5183/Login";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(loginDTO);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<LoginDTO>>(restResponse.Content);

            if (restResponse.StatusCode==HttpStatusCode.NotFound && responseObject?.Data==null)
            {
                ViewBag.LoginError = "Buraya Bir Data Geliyor";
                ViewData["LoginError"] = "Kullanıcı Adı Veya Şifre Yanlış";
                TempData["LoginError"] = "Buraya Başka Bir Data Geliyor";
                return View("Index");
            }
            else if(restResponse.StatusCode!=HttpStatusCode.OK) 
            {
                ViewData["LoginError"] = "Hata Oluştu";
                return View("Index");
            }

            //_httpContextAccessor.HttpContext.Session.SetString("UserAdSoyad",responseObject.Data.AdSoyad);

            //_httpContextAccessor.HttpContext.Session.SetString("UserID", responseObject.Data.UserID.ToString());

            //_httpContextAccessor.HttpContext.Session.SetString("EPosta", responseObject.Data.EPosta.ToString());

            SessionManager.LoggedUser = responseObject.Data;

            return RedirectToAction("Index", "Home");
        }

    }
}
