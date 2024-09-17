using GerenciadorDeTarefas.Data;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Service;

public class TarefaService
{
    private readonly GerenciadorDeTarefasContext _context;

    public TarefaService(GerenciadorDeTarefasContext context)
    {
        _context = context;
    }

    public async Task<List<Tarefa>> TodasTarefas()
    {
        return await _context.Tarefa.ToListAsync();
    }

    public async Task<Tarefa>ObterPorTitulo(string titulo)
    {
        return await _context.Tarefa.FirstOrDefaultAsync(t => t.Titulo == titulo);
    }

    public async Task<Tarefa> ObterPorData(DateTime data)
    {
        return await _context.Tarefa.FirstOrDefaultAsync(t => t.Data == data);
    }

    public async Task<Tarefa> ObterPorStatus(EnumStatusTarefa status)
    {
        return await _context.Tarefa.FirstOrDefaultAsync(t => t.Status == status);
    }

    public async Task<Tarefa> BuscaId(int id)
    {
        return await _context.Tarefa.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task InseriTarefa(Tarefa tarefa)
    {
        _context.Add(tarefa);
        await _context.SaveChangesAsync();
    }
    public async Task Atualizar(Tarefa tarefa)
    {
        bool existe = await _context.Tarefa.AnyAsync(t => t.Id == tarefa.Id);
        if (!existe)
        {
            throw new Exception();
        }
        try
        {
            _context.Update(tarefa);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {

            throw new Exception(e.Message);
        }
    }

    public async Task Remover(int id)
    {
        var obj = _context.Tarefa.Find(id);
        _context.Tarefa.Remove(obj);
        await _context.SaveChangesAsync();
    }

}
