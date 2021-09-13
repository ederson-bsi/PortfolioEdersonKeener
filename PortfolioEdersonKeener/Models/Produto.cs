using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int? Id { get; set; }

        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres!")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "Cód. Produto")]
        [Range(0, int.MaxValue, ErrorMessage = "O código do produto não pode ser negativo!")]
        [Required(ErrorMessage = "O código do produto é obrigatório!")]
        public int? CodigoProduto { get; set; }

        [Display(Name = "Estoque Mín")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque mínimo não pode ser negativo!")]
        [Required(ErrorMessage = "O estoque mínimo é obrigatório!")]
        public int? EstoqueMinimo { get; set; }

        [Display(Name = "Estoque Atual")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque atual não pode ser negativo!")]
        [Required(ErrorMessage = "O estoque atual é obrigatório!")]
        public int? EstoqueAtual { get; set; }

        [Display(Name = "Preço de Custo")]
        [Required(ErrorMessage = "O preço de custo é obrigatório!")]
        [Range(0, float.MaxValue, ErrorMessage = "O preço de custo não pode ser negativo!")]
        public float? PrecoCusto { get; set; }

        [Display(Name = "Preço de Venda")]
        [Required(ErrorMessage = "O preço de venda é obrigatório!")]
        [Range(0, float.MaxValue, ErrorMessage = "O preço de venda não pode ser negativo!")]
        public float? PrecoVenda { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Validade")]
        [Required(ErrorMessage = "A data de validade é obrigatória!")]
        public DateTime? Validade { get; set; }

        //Campos FK da tabela Categoria
        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "A categoria é obrigatória!")]
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        //Campo FK da tabela Marca
        [ForeignKey("Marca")]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "A marca é obrigatória!")]
        public int IdMarca { get; set; }
        public Marca Marca { get; set; }

        //Campo FK da tabela Unidade_Medida
        [Display(Name = "Unidade de Medida")]
        [ForeignKey("UnidadeMedida")]
        [Required(ErrorMessage = "A unidade de medida é obrigatória!")]
        public int IdUnidadeMedida { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; }

        public virtual ICollection<Estoque> Estoques { get; set; }
    }
}
