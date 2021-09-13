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
    public class MarcaController : Controller
    {
        private ApplicationDbContext _context;
        public MarcaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Marcas.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Titulo = "Cadastrar Marca";
            return View("MarcaForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(Marca marca)
        {
            if (!ModelState.IsValid)
            {
                if (marca.Id == 0)
                {
                    ViewBag.Titulo = "Cadastrar Marca";
                }
                else
                {
                    ViewBag.Titulo = "Editar Marca";
                }
                return View("MarcaForm");
            }

            if (marca == null)
            {
                return NotFound();
            }

            if (marca.Id == 0 || marca.Id == null)
            {
                _context.Marcas.Add(marca);
            }
            else
            {
                var marcaInDB = _context.Marcas.Single(x => x.Id == marca.Id);

                marcaInDB.Nome = marca.Nome;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Marca");
        }

        public IActionResult Editar(int id)
        {
            var marca = _context.Marcas.Find(id);
            if (marca == null)
            {
                return NotFound();
            }

            ViewBag.Titulo = "Editar Marca";

            return View("MarcaForm", marca);
        }

        public IActionResult Excluir(int id)
        {
            var marcaDel = _context.Marcas.Find(id);
            if (marcaDel == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marcaDel);
            _context.SaveChanges();

            return RedirectToAction("Index", "Marca");
        }
    }
}
