using Domain;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class PetrobowlDbContext : IdentityDbContext<Usuario>
    {
        public PetrobowlDbContext(DbContextOptions<PetrobowlDbContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userName = "system";
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = userName;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = userName;
                        break;


                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Pregunta>()
                .HasMany(p => p.Respuestas)
                .WithOne(r => r.Pregunta)
                .HasForeignKey(r => r.PreguntaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BancoPregunta>()
                .HasMany(b => b.Preguntas)
                .WithOne(b => b.BancoPregunta)
                .HasForeignKey(b => b.BancoPreguntaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Competencia>()
                .HasMany(b => b.BancoPreguntas)
                .WithOne(b => b.Competencia)
                .HasForeignKey(b => b.CompetenciaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Competencia>()
                .HasMany(b => b.Equipos)
                .WithOne(b => b.Competencia)
                .HasForeignKey(b => b.CompetenciaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Equipo>()
                .HasMany(b => b.Personas)
                .WithOne(b => b.Equipo)
                .HasForeignKey(b => b.EquipoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Resultado>()
                .HasOne(b => b.Equipos)
                .WithMany(b => b.Resultados)
                .HasForeignKey(b => b.EquipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Resultado>()
                .HasOne(b => b.Competencias)
                .WithMany(b => b.Resultados)
                .HasForeignKey(b => b.CompetenciaId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Usuario>().Property(x => x.Id).HasMaxLength(36); //250
            builder.Entity<Usuario>().Property(x => x.NormalizedUserName).HasMaxLength(90);
            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(36);
            builder.Entity<IdentityRole>().Property(x => x.NormalizedName).HasMaxLength(90);
        }


        public DbSet<Respuesta>? Respuestas { get; set; }
        public DbSet<Pregunta>? Preguntas { get; set; }
        public DbSet<BancoPregunta>? BancoPreguntas { get; set; }
        public DbSet<Competencia>? Competencias { get; set; }
        public DbSet<Equipo>? Equipos { get; set; }
        public DbSet<Persona>? Personas { get; set; }
        public DbSet<Resultado>? Resultados { get; set; }




    }
}
