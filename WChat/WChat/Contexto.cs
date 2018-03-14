using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WChat

{
    public class Contexto : DbContext
    {
        public string WChat = "Data Source=.\\SQLEXPRESS;Initial Catalog =WChat;Integrated Security=SSPI";

        public Contexto() : base("WChat") { 
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contexto, WChat.Migrations.Configuration>());
        }

        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Pendencia> Pendencias { get; set; }
        public DbSet<PendenciaHistorico> PendenciasHistoricos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<ParametroResponsavel> ParametrosResponsaveis { get; set; }
        public DbSet<Diario> Diarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Responsavel>()
                .HasRequired(p => p.ClienteDefault)
                .WithMany(d => d.ResponsaveisDefaults)
                .HasForeignKey(p => p.ClienteDefaultId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Responsavel>()
                .HasOptional(p => p.ClienteRestrito)
                .WithMany(d => d.ResponsaveisRestritos)
                .HasForeignKey(p => p.ClienteRestritoId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Pendencia>()
                .HasRequired(p => p.ResponsavelAtual)
                .WithMany(d => d.PendenciasAtuais)
                .HasForeignKey(p => p.ResponsavelAtualId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Pendencia>()
                .HasRequired(p => p.ResponsavelAbertura)
                .WithMany(d => d.PendenciasAberturas)
                .HasForeignKey(p => p.ResponsavelAberturaId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Cliente>()
                .HasOptional(p => p.Centralizadora)
                .WithMany(d => d.Clientes)
                .HasForeignKey(p => p.CentralizadoraId)
                .WillCascadeOnDelete(false);
        }
    }
}
