using System.Linq.Expressions;
using System.Reflection;
using FerminToroMS.Core.Database;
using FerminToroMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FerminToroMS.Infrastructure.Database;


public class FerminToroDbContext : DbContext, IFerminToroDbContext
{
    public FerminToroDbContext(DbContextOptions<FerminToroDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<ValoresEntity> Valores { get; set; } = null!;
    public virtual DbSet<EstudianteEntity> Estudiantes { get; set; } = null!;
    public virtual DbSet<EmpleadoEntity> Empleados { get; set; } = null!;
    public virtual DbSet<AbonoEntity> Abonos { get; set; } = null!;
    public virtual DbSet<CronogramaEntity> Cronogramas { get; set; } = null!;
    public virtual DbSet<CursoEntity> Cursos { get; set; } = null!;
    public virtual DbSet<DatoEmpresaJuridicaEntity> Datos_Empresa_Juridica { get; set; } = null!;
    public virtual DbSet<DeudasEntity> Deudas { get; set; } = null!;
    public virtual DbSet<Empleado_PermisoEntity> Permisos_Empleados { get; set; } = null!;
    public virtual DbSet<Fechas_PagoEntity> Fechas_Limite_Pago { get; set; } = null!;
    public virtual DbSet<InscripcionEntity> Inscripciones { get; set; } = null!;
    public virtual DbSet<Metodo_PagoEntity> Metodos_Pago { get; set; } = null!;
    public virtual DbSet<ModuloEntity> Modulos { get; set; } = null!;
    public virtual DbSet<Modulos_AprobadoEntity> Modulos_Aprobados { get; set; } = null!;
    public virtual DbSet<PagoEntity> Pagos { get; set; } = null!;
    public virtual DbSet<PeriodoEntity> Periodos { get; set; } = null!;
    public virtual DbSet<PermisosEntity> Permisos { get; set; } = null!;
    public virtual DbSet<Precio_Mod_TurnoEntity> Precios { get; set; } = null!;
    public virtual DbSet<PromocionEntity> Promociones { get; set; } = null!;
    public virtual DbSet<PaisesEntity> Paises { get; set; } = null!;
    public virtual DbSet<EstadosVnzlaEntity> Estados_Venezuela { get; set; } = null!;
    public virtual DbSet<RepresentanteEntity> Representantes { get; set; } = null!;
    public DbContext DbContext
    {
        get
        {
            return this;
        }
    }

    public IDbContextTransactionProxy BeginTransaction()
    {
        return new DbContextTransactionProxy(this);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    virtual public void SetPropertyIsModifiedToFalse<TEntity, TProperty>(TEntity entity,
        Expression<Func<TEntity, TProperty>> propertyExpression) where TEntity : class
    {
        Entry(entity).Property(propertyExpression).IsModified = false;
    }

    virtual public void ChangeEntityState<TEntity>(TEntity entity, EntityState state)
    {
        if (entity != null)
        {
            Entry(entity).State = state;
        }
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                Entry((BaseEntity)entityEntry.Entity).Property(x => x.CreatedAt).IsModified = false;
                Entry((BaseEntity)entityEntry.Entity).Property(x => x.CreatedBy).IsModified = false;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(string user, CancellationToken cancellationToken = default)
    {
        var state = new List<EntityState> { EntityState.Added, EntityState.Modified };

        var entries = ChangeTracker.Entries().Where(e =>
            e.Entity is BaseEntity && state.Any(s => e.State == s)
        );

        var dt = DateTime.UtcNow;

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = dt;
                entity.CreatedBy = user;
                Entry(entity).Property(x => x.UpdatedAt).IsModified = false;
                Entry(entity).Property(x => x.UpdatedBy).IsModified = false;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entity.UpdatedAt = dt;
                entity.UpdatedBy = user;
                Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                Entry(entity).Property(x => x.CreatedBy).IsModified = false;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveEfContextChanges(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken) >= 0;
    }

    public async Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(user, cancellationToken) >= 0;
    }
}
