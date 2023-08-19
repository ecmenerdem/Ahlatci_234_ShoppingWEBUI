using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using RestSharp;
using ShoppingWEBUI.Core.DTO;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Core.ViewModel;
using ShoppingWEBUI.Helper.SessionHelper;
using System.Net;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/Admin/Urunler")]
        public async Task<IActionResult> Index()
        {
            ProductViewModel productViewModel = new()
            {
                Products = await GetProductList(),
                Categories = await GetCategoryList()
            };
            return View(productViewModel);
        }

        [HttpGet("/Admin/Urun/{productGUID}")]
        public async Task<IActionResult> GetProduct(Guid productGUID)
        {
            var url = "http://localhost:5183/Product/" + productGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<ProductDTO>>(restResponse.Content);

            var product = responseObject.Data;

            return Json(new { success = true, product });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO, IFormFile productImage)
        {
            if (productImage != null)
            {
                string fileName = productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                };

                productDTO.FeaturedImage = fileName;

                var url = "http://localhost:5183/AddProduct";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
                var body = JsonConvert.SerializeObject(productDTO);
                request.AddBody(body, "application/json");
                RestResponse restResponse = await client.ExecuteAsync(request);

                var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Json(new { success = true, data = responseObject.Data });
                }
                else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
                }
                else
                {
                    return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
                }
            }
            else
            {
                return Json(new { });
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO, IFormFile productImage)
        {

            if (productImage != null)
            {
                string fileName = productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUpload", fileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                };

                productDTO.FeaturedImage = fileName;
            }

            var url = "http://localhost:5183/UpdateProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(productDTO);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }


        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/RemoveProduct/{productGUID}")]

        public async Task<IActionResult>RemoveProduct(Guid productGUID)
        {
            var url = "http://localhost:5183/RemoveProduct/" + productGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
        }

        public async Task<List<ProductDTO>> GetProductList()
        {
            var url = "http://localhost:5183/Products";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<ProductDTO>>>(restResponse.Content);

            var products = responseObject.Data;

            return products;
        }
        public async Task<List<CategoryDTO>> GetCategoryList()
        {
            var url = "http://localhost:5183/Categories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CategoryDTO>>>(restResponse.Content);

            var categories = responseObject.Data;

            return categories;
        }
        public async Task<CategoryDTO> GetCategory(Guid? categoryGUID)
        {
            var url = "http://localhost:5183/Category/" + categoryGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<CategoryDTO>>(restResponse.Content);

            var category = responseObject.Data;

            return category;
        }

    }
}
