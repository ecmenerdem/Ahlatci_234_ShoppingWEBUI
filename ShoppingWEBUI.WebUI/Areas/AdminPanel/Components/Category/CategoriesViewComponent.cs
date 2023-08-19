using Microsoft.AspNetCore.Mvc;
using ShoppingWEBUI.Core.DTO;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Components.Category
{
    public class CategoriesViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<CategoryDTO> categoryDTOList)
        {
            return View("~/Areas/AdminPanel/Views/Shared/Component/Category/Categories.cshtml", categoryDTOList);
        }
    }
}
