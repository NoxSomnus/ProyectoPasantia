using FerminToroMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Core.Database
{
    public interface IFerminToroDbContext
    {
        DbSet<ValoresEntity> Valores { get; }

        DbSet<EstudianteEntity> Estudiantes { get; }

        DbSet<EmpleadoEntity> Empleados { get; }

        DbContext DbContext
        {
            get;
        }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);

    }
}
