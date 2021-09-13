using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Models
{
    [Table("Unidade_Medida")]
    public class UnidadeMedida
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(50, ErrorMessage = "A descrição deve ter no máximo 50 caracteres!")]
        [Required(ErrorMessage = "A descrição é obrigatória!")]
        public string Descricao { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
