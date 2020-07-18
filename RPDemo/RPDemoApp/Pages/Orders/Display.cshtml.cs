using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPDemoApp.Models;

namespace RPDemoApp.Pages.Orders
{
    public class DisplayModel : PageModel
    {
        private IOrderData _orderData;
        private IFoodData _foodData;

        // Get the Id attribute from the Get
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public OrderUpdateModel UpdateModel { get; set; }

        public OrderModel Order{ get; set; }
        public string ItemPurchased { get; set; }
        public DisplayModel(IOrderData orderData, IFoodData foodData)
        {
            _orderData = orderData;
            _foodData = foodData;
        }
        public async Task<PageResult> OnGet()
        {
            Order = await _orderData.GetOrderById(Id);
            if (Order != null)
            {
                var food = await _foodData.GetFood();
                ItemPurchased = food.Where(x => x.Id == Order.FoodId).FirstOrDefault()?.Title;
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
                return Page();

            await _orderData.UpdateOrderName(UpdateModel.Id, UpdateModel.OrderName);
            return RedirectToPage("./Display", new { UpdateModel.Id });
        }
    }
}