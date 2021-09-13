using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioEdersonKeener.Data;
using PortfolioEdersonKeener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private ApplicationDbContext _context;

        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Produtos.Include(x => x.Categoria).Include(x => x.Marca).Include(x => x.UnidadeMedida).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ListaCategoria = _context.Categorias.ToList();
            ViewBag.ListaMarca = _context.Marcas.ToList();
            ViewBag.ListaUnidadeMedida = _context.UnidadeMedidas.ToList();
            ViewBag.Titulo = "Cadastrar Novo Produto";

            return View("ProdutoForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                if (produto.Id == 0)
                {
                    ViewBag.Titulo = "Cadastrar Novo Produto";
                }
                else
                {
                    ViewBag.Titulo = "Editar Produto";
                }

                ViewBag.ListaCategoria = _context.Categorias.ToList();
                ViewBag.ListaMarca = _context.Marcas.ToList();
                ViewBag.ListaUnidadeMedida = _context.UnidadeMedidas.ToList();

                return View("ProdutoForm");
            }

            if (produto == null)
            {
                return NotFound();
            }

            if (produto.Id == 0 || produto.Id == null)
            {
                _context.Produtos.Add(produto);
            }
            else
            {
                var produtoInDB = _context.Produtos.Include(x => x.Categoria).Include(x => x.Marca).Include(x => x.UnidadeMedida).Single(x => x.Id == produto.Id);

                produtoInDB.Nome = produto.Nome;
                produtoInDB.CodigoProduto = produto.CodigoProduto;
                produtoInDB.EstoqueMinimo = produto.EstoqueMinimo;
                produtoInDB.EstoqueAtual = produto.EstoqueAtual;
                produtoInDB.PrecoCusto = produto.PrecoCusto;
                produtoInDB.PrecoVenda = produto.PrecoVenda;
                produtoInDB.Validade = produto.Validade;
                produtoInDB.IdCategoria = produto.IdCategoria;
                produtoInDB.IdMarca = produto.IdMarca;
                produtoInDB.IdUnidadeMedida = produto.IdUnidadeMedida;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Produto");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var produto = _context.Produtos.Include(x => x.Categoria).Include(x => x.Marca).Include(x => x.UnidadeMedida).First(x => x.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.ListaCategoria = _context.Categorias.ToList();
            ViewBag.ListaMarca = _context.Marcas.ToList();
            ViewBag.ListaUnidadeMedida = _context.UnidadeMedidas.ToList();
            ViewBag.Titulo = "Editar Produto";

            return View("ProdutoForm", produto);
        }

        public IActionResult Excluir(int id)
        {
            var produtoDel = _context.Produtos.Find(id);
            if (produtoDel == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produtoDel);
            _context.SaveChanges();

            return RedirectToAction("Index", "Produto");
        }
    }
}
