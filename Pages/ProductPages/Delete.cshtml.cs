using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GreenMVC.Context;
using GreenMVC.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace GreenMVC.Pages.ProductPages
{
    public class DeleteModel : PageModel
    {
        private readonly GreenMVC.Context.GreenStockContextMVC _context;
        private IConfiguration Configuration;

        public DeleteModel(GreenMVC.Context.GreenStockContextMVC context, IConfiguration configuration)
        {
            _context = context;
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client
                    .DeleteAsync("api/Products/" + id);


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
