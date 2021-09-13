using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "O nome da categoria deve ter no maxímo 50 caracteres!")]
        [Required(ErrorMessage = "O nome da categoria é obrigatório!")]
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
