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
    public class CategoriaController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Categoria
        public ActionResult Index()
        {
            return View(_context.Categorias.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Titulo = "Nova Categoria";
            return View("CategoriaForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    ViewBag.Titulo = "Nova Categoria";
                }
                else
                {
                    ViewBag.Titulo = "Editar Categoria";
                }
                return View("CategoriaForm");
            }

            if (categoria == null)
            {
                return NotFound();
            }

            if (categoria.Id == 0 || categoria.Id == null)
            {
                _context.Categorias.Add(categoria);
            }
            else
            {
                var categoriaInDB = _context.Categorias.Single(x => x.Id == categoria.Id);

                categoriaInDB.Nome = categoria.Nome;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Categoria");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }

            ViewBag.Titulo = "Editar Categoria";
            return View("CategoriaForm", categoria);
        }

        public ActionResult Excluir(int id)
        {
            var categoriaExcluida = _context.Categorias.Find(id);

            if (categoriaExcluida == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoriaExcluida);
            _context.SaveChanges();

            return RedirectToAction("Index", "Categoria");
        }
    }
}
