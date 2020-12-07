using Microsoft.EntityFrameworkCore;
using ProyectoJose.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace ProyectoJose.Services
{
   public class PruebaContext : DbContext
    {

        public DbSet<T_Registro> T_Registros { get; set; }

        public DbSet<Trabajador> Trabajadors { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<TipoDia>TipoDias { get; set; }

        public DbSet<Periodo>Periodos { get; set; }

        public DbSet<Epi>Epis { get; set; }

        public DbSet<Festivo>Festivos { get; set; }

        public DbSet<TrabajadorEpi> TrabajadorEpis { get; set; }

        public DbSet<TrabajadorCurso> TrabajadorCursos { get; set; }


// arranque sqlite y base
        public PruebaContext()
        {

            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();

        }
 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ProyJoseAntonio.db3");
          
            //proveedor base
            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrabajadorCurso>()
                  .HasKey(t => new { t.IdTrabajador, t.IdCurso });

            modelBuilder.Entity<TrabajadorCurso>()
               .HasOne<Trabajador>(pt => pt.Trabajador)
               .WithMany(p => p.TrabajadoresCurso);

            modelBuilder.Entity<TrabajadorCurso>()
               .HasOne<Curso>(pt => pt.Curso)
               .WithMany(t => t.TrabajadoresCurso);

            modelBuilder.Entity<TrabajadorEpi>()
                  .HasKey(tr => new { tr.IdTrabajador, tr.IdEpi });

            modelBuilder.Entity<TrabajadorEpi>()
               .HasOne<Trabajador>(pe => pe.Trabajador)
               .WithMany(x => x.TrabajadoresEpi);

            modelBuilder.Entity<TrabajadorEpi>()
               .HasOne<Epi>(e => e.Epi)
               .WithMany(te => te.TrabajadoresEpi);

         
        }


    }
}
