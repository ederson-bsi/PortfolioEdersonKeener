using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "Tipo de Movimento")]
        [Required(ErrorMessage = "O Tipo do Movimento é obrigatório!")]
        public TipoMovimentoEstoque TipoMovimento { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória!")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa!")]
        [Display(Name = "Quantidade")]
        public int? Quantidade { get; set; }

        public int? EstoqueAtual { get; set; }

        [Display(Name = "Data do Movimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataMovimento { get; set; }

        [ForeignKey("Produto")]
        [Required(ErrorMessage = "O produto é obrigatório!")]
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
    }

    public enum TipoMovimentoEstoque
    {
        [Display(Name = "ENTRADA")]
        ENTRADA,
        [Display(Name = "SAIDA")]
        SAIDA
    }
}
