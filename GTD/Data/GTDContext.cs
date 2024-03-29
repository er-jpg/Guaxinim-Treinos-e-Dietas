﻿using GTD.Models;
using GTD.Models.Infra;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Data
{
    public class GTDContext : IdentityDbContext<Models.Infra.ApplicationUser>
    {
        public GTDContext(DbContextOptions<GTDContext> options) : base(options) {}
        public DbSet<Plano> Plano { get; set; }
        public DbSet<GTD.Models.Treino> Treino { get; set; }
        public DbSet<GTD.Models.Diario> Diario { get; set; }
        public DbSet<GTD.Models.Dieta> Dieta { get; set; }
        public DbSet<GTD.Models.Semana> Semana { get; set; }
        // n:m na gambiarra dessa merda pqp se foder porra ngm me ajuda nesse caralho
        public DbSet<DietaSemana> DietaSemana { get; set; }
        public DbSet<TreinoSemana> TreinoSemana { get; set; }

        // n:m mas não tem no nosso sistema
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dieta Semana
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DietaSemana>().
                HasKey(ds => new { ds.DietaSemanaID });

            modelBuilder.Entity<DietaSemana>().
                HasOne(d => d.Dieta).WithMany(ds => ds.DietaSemana).
                HasForeignKey(d => d.DietaID);

            modelBuilder.Entity<DietaSemana>().
                HasOne(s => s.Semana).WithMany(ds => ds.DietaSemana).
                HasForeignKey(s => s.SemanaID);

            // Treino Semana
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TreinoSemana>().
                HasKey(ts => new { ts.TreinoSemanaID });

            modelBuilder.Entity<TreinoSemana>().
                HasOne(t => t.Treino).WithMany(ts => ts.TreinoSemana).
                HasForeignKey(t => t.TreinoID);

            modelBuilder.Entity<TreinoSemana>().
                HasOne(s => s.Semana).WithMany(ts => ts.TreinoSemana).
                HasForeignKey(s => s.SemanaID);
        }
    }
}
