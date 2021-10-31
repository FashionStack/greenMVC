using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GreenMVC.Context;
using GreenMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace GreenMVC.Pages.CategoryPages
{
    public class DetailsModel : PageModel
    {
        private IConfiguration Configuration;
        public DetailsModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [BindProperty]
        public Category Category { get; set; }

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
                HttpResponseMessage response = await client.GetAsync("api/Categories/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Category = JsonConvert.DeserializeObject<Category>(result);
                }
            }

            return Page();
        }

    }
}
