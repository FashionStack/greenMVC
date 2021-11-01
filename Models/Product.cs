using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMVC.Models
{
    public class Product
    {
        [DisplayName("ID do Produto")]
        public long ProductId { get; set; }

        [DisplayName("Categoria")]
        public Category Category { get; set; }

        [DisplayName("ID da Categoria")]
        public int CategoryId { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Tamanho  ")]
        public string Size { get; set; }

        [DisplayName("SKU")]
        public string SKU { get; set; }

        [DisplayName("REF")]
        public string ReferenceCode { get; set; }

        [DisplayName("  Quantidade ")]
        public int Amount { get; set; }

        [DisplayName("URL da Imagem")]
        public string ImageUrl { get; set; }
    }
}
