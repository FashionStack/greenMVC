using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GreenMVC.Context;
using GreenMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace GreenMVC.Pages.ProductPages
{
    public class EditStockModel : PageModel
    {
        private IConfiguration Configuration;
        public EditStockModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [BindProperty]
        public Product Product { get; set; }
        string baseUrl => Configuration.GetConnectionString("ApiUrl");

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Products/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Product = JsonConvert.DeserializeObject<Product>(result);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var productAmount = new ProductAmount()
                {
                    ProductId = Product.ProductId,
                    Amount = Product.Amount
                };

                HttpResponseMessage response = await client
                    .PutAsJsonAsync("api/Products/amount", productAmount);

                if (response.IsSuccessStatusCode)
                {
                    //Produtos/Index
                    return RedirectToPage("./Index");
                }
                else
                {
                    return RedirectToPage("./Create");
                }
            }
        }
    }
}