using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefas.Models;

namespace GerenciadorDeTarefas.Data
{
    public class GerenciadorDeTarefasContext : DbContext
    {
        public GerenciadorDeTarefasContext (DbContextOptions<GerenciadorDeTarefasContext> options)
            : base(options)
        {
        }

        public DbSet<GerenciadorDeTarefas.Models.Tarefa> Tarefa { get; set; } = default!;
    }
}
