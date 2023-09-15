using FerminToroMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FerminToroMS.Core.Database
{
    public interface IFerminToroDbContext
    {
        DbSet<ValoresEntity> Valores { get; }

        DbSet<EstudianteEntity> Estudiantes { get; }
        DbSet<EmpleadoEntity> Empleados { get; }
        DbSet<AbonoEntity> Abonos { get; }
        DbSet<CronogramaEntity> Cronogramas { get; }
        DbSet<CursoEntity> Cursos { get; }
        DbSet<DatoEmpresaJuridicaEntity> Datos_Empresa_Juridica { get; }
        DbSet<DeudasEntity> Deudas { get; }
        DbSet<Empleado_PermisoEntity> Permisos_Empleados { get; }
        DbSet<Fechas_PagoEntity> Fechas_Limite_Pago { get; }
        DbSet<InscripcionEntity> Inscripciones { get; }
        DbSet<Metodo_PagoEntity> Metodos_Pago { get; }
        DbSet<ModuloEntity> Modulos { get; }
        DbSet<Modulos_AprobadoEntity> Modulos_Aprobados { get; }
        DbSet<PagoEntity> Pagos { get; }
        DbSet<PeriodoEntity> Periodos { get; }
        DbSet<PermisosEntity> Permisos { get; }
        DbSet<Precio_Mod_TurnoEntity> Precios { get; }
        DbSet<PromocionEntity> Promociones { get; }
        DbSet<PaisesEntity> Paises { get; }
        DbSet<EstadosVnzlaEntity> Estados_Venezuela { get; }
        DbSet<RepresentanteEntity> Representantes { get; }
        DbSet<InscripcionesCongeladasEntity> InscripcionesCongeladas { get; }
        DbSet<PagosAprobadosEntity> Pagos_Aprobados { get; }
        DbContext DbContext
        {
            get;
        }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);

    }
}
