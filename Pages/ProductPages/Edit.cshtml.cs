using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenMVC.Context;
using GreenMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace GreenMVC.Pages.ProductPages
{
    public class EditModel : PageModel
    {
        private IConfiguration Configuration;
        public EditModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public List<Category> Categories { get; set; }

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
                HttpResponseMessage productsResponse = await client.GetAsync("api/Products/" + id);

                if (productsResponse.IsSuccessStatusCode)
                {
                    string result = productsResponse.Content.ReadAsStringAsync().Result;
                    Product = JsonConvert.DeserializeObject<Product>(result);
                }

                HttpResponseMessage categoriesResponse = await client.GetAsync("api/Categories");

                if (categoriesResponse.IsSuccessStatusCode)
                {
                    string result = categoriesResponse.Content.ReadAsStringAsync().Result;
                    Categories = JsonConvert.DeserializeObject<List<Category>>(result);
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
                HttpResponseMessage response = await client
                    .PutAsJsonAsync("api/Product/" + Product.ProductId, Product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }
            }
        }
    }
}
