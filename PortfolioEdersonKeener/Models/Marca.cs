using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Models
{
    [Table("Marca")]
    public class Marca
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "Marca")]
        [StringLength(50, ErrorMessage = "O nome da marca deve ter no máximo 50 caracteres!")]
        [Required(ErrorMessage = "A marca é obrigatória!")]
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
