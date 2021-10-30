using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Nome da categoria")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Qual tipo de tamanho ?")]
        public char SizeType { get; set; }
        [Required]
        [DisplayName("Essa roupa e sustentavel ? ")]
        public bool Status { get; set; }
        public List<Product> Product { get; set; }
    }
}
