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
    public class CreateModel : PageModel
    {
        private IConfiguration Configuration;
        public CreateModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public List<Category> Category { get; set; }

        string baseUrl => Configuration.GetConnectionString("ApiUrl");

        public async Task<IActionResult> OnPostAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client
                    .PostAsJsonAsync("api/Products", Product);

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

        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Categories");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Category = JsonConvert.DeserializeObject<List<Category>>(result);
                }
            }
        }
    }
}
