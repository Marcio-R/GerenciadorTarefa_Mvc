using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefas.Data;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Service;
using GerenciadorDeTarefas.Models.Enum;

namespace GerenciadorDeTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly TarefaService ContextService;

        public TarefasController(TarefaService contextService)
        {
            ContextService = contextService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await ContextService.TodasTarefas();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(EnumStatusTarefa)));
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return View(tarefa);
            }
            await ContextService.InseriTarefa(tarefa);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await ContextService.BuscaId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(EnumStatusTarefa)));
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return View(tarefa);
            }
            if (id != tarefa.Id)
            {
                return BadRequest();
            }
            try
            {
                await ContextService.Atualizar(tarefa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await ContextService.BuscaId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await ContextService.Remover(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await ContextService.BuscaId(id.Value);
            if (id == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }
}
