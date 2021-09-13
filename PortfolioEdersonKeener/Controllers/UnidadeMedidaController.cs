using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEdersonKeener.Data;
using PortfolioEdersonKeener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEdersonKeener.Controllers
{
    [Authorize]
    public class UnidadeMedidaController : Controller
    {
        private ApplicationDbContext _context;
        public UnidadeMedidaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.UnidadeMedidas.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Titulo = "Cadastrar Unidade Medida";
            return View("UnidadeMedidaForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(UnidadeMedida unidadeMedida)
        {
            if (!ModelState.IsValid)
            {
                if (unidadeMedida.Id == 0)
                {
                    ViewBag.Titulo = "Cadastrar Unidade Medida";
                }
                else
                {
                    ViewBag.Titulo = "Editar Unidade Medida";
                }

                return View("UnidadeMedidaForm");
            }

            if (unidadeMedida == null)
            {
                return NotFound();
            }

            if (unidadeMedida.Id == 0 || unidadeMedida.Id == null)
            {
                _context.UnidadeMedidas.Add(unidadeMedida);
            }
            else
            {
                var unidadeMedidaInDB = _context.Marcas.Single(x => x.Id == unidadeMedida.Id);

                unidadeMedidaInDB.Nome = unidadeMedida.Descricao;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "UnidadeMedida");
        }

        public IActionResult Editar(int id)
        {
            var unidadeMedida = _context.UnidadeMedidas.Find(id);
            if (unidadeMedida == null)
            {
                return NotFound();
            }

            ViewBag.Titulo = "Editar Unidade Medida";

            return View("UnidadeMedidaForm", unidadeMedida);
        }

        public IActionResult Excluir(int id)
        {
            var unidadeMedidaDel = _context.UnidadeMedidas.Find(id);
            if (unidadeMedidaDel == null)
            {
                return NotFound();
            }

            _context.UnidadeMedidas.Remove(unidadeMedidaDel);
            _context.SaveChanges();

            return RedirectToAction("Index", "UnidadeMedida");
        }
    }
}
