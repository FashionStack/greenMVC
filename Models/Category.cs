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
        [DisplayName("ID da Categoria")]
        public int CategoryId { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Tipo do Tamanho")]
        public char SizeType { get; set; }

        [DisplayName("Ativo")]
        public bool Status { get; set; }
    }
}
