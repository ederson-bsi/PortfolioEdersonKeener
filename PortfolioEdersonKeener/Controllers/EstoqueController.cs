using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioEdersonKeener.Data;
using PortfolioEdersonKeener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Controllers
{
    [Authorize]
    public class EstoqueController : Controller
    {
        private ApplicationDbContext _context;
        public EstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Estoques.Include(x => x.Produto).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Titulo = "Adicionar Movimentação de Estoque";
            ViewBag.ListaProduto = _context.Produtos.ToList();

            return View("EstoqueForm");
        }

        public Boolean ValidacaoEstoque(Estoque estoque)
        {
            var produtoInDB = _context.Produtos.Single(x => x.Id == estoque.IdProduto);

            if (estoque.TipoMovimento.ToString().Equals("SAIDA"))
            {
                if (produtoInDB.EstoqueMinimo > (produtoInDB.EstoqueAtual - estoque.Quantidade))
                {
                    ViewBag.MsgError = "Valor de estoque mínimo excedido, o valor do Estoque atual não pode ser menor que o Estoque mínimo!";

                    return false;
                }
                produtoInDB.EstoqueAtual = ((int)(produtoInDB.EstoqueAtual - estoque.Quantidade));
                return true;
            }
            else
            {
                produtoInDB.EstoqueAtual = ((int)(produtoInDB.EstoqueAtual + estoque.Quantidade));
                _context.SaveChanges();
            }

            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(Estoque estoque)
        {
            if (!ModelState.IsValid)
            {
                if (estoque.Id == 0)
                {
                    ViewBag.Titulo = "Adicionar Movimentação de Estoque";
                }
                else
                {
                    ViewBag.Titulo = "Editar Movimentação de Estoque";
                }
                
                ViewBag.ListaProduto = _context.Produtos.ToList();
                return View("EstoqueForm");
            }

            var produtoInDB = _context.Produtos.Find(estoque.IdProduto);
            if (estoque == null)
            {
                return NotFound();
            }

            if (estoque.Id == 0 || estoque.Id == null)
            {
                if (ValidacaoEstoque(estoque) == false)
                {
                    ViewBag.ListaProduto = _context.Produtos.ToList();
                    return View("EstoqueForm");
                }

                estoque.EstoqueAtual = produtoInDB.EstoqueAtual;
                estoque.DataMovimento = DateTime.Now;
                _context.Estoques.Add(estoque);
            }
            else
            {
                var estoqueInDB = _context.Estoques.Include(x => x.Produto).Single(x => x.Id == estoque.Id);
                estoqueInDB.IdProduto = estoque.IdProduto;
                estoqueInDB.TipoMovimento = estoque.TipoMovimento;

                if (estoqueInDB.Quantidade != estoque.Quantidade)
                {
                    ValidacaoEstoque(estoque);
                    estoqueInDB.Quantidade = estoque.Quantidade;
                    estoqueInDB.EstoqueAtual = produtoInDB.EstoqueAtual;
                }

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Estoque");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var estoque = _context.Estoques.Include(x => x.Produto).First(x => x.Id == id);
            if (estoque == null)
            {
                return NotFound();
            }

            ViewBag.Titulo = "Editar Movimentação de Estoque";
            ViewBag.ListaProduto = _context.Produtos.ToList();

            return View("EstoqueForm", estoque);
        }
    }
}
