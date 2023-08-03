using FerminToroMS.Infrastructure.Settings;

namespace FerminToroMS.Providers.Interface
{
    public interface IProviders
    {
        IServiceCollection AddDatabaseService(IServiceCollection services, IConfiguration configuration,
            string environment, bool isRequired);

        IServiceCollection AddControllers(IServiceCollection services, IConfiguration configuration,
            AppSettings appSettings);

        IServiceCollection AddSwagger(IServiceCollection services, string versionNumber, AppSettings appSettings);
        IServiceCollection AddCors(IServiceCollection services);
    }
}
