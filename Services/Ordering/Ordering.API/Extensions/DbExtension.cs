using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;
using Polly;

namespace Ordering.API.Extensions
{
    public static class DbExtension
    {
        public static void MigrateDatabase<TContext>(this IHost host,Action<TContext,IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TContext>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation($"Migrating database started with context {typeof(TContext).Name}");

                    //retry stragagy
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (exception, timeSpan, retryCount, context) =>
                        {
                            logger.LogWarning(exception, $"Retry {retryCount} of migrating database {typeof(TContext).Name}");
                        });

                    retry.Execute(() => CallSeader(seeder,context,services));

                    logger.LogInformation($"Migrated database Completed with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database {typeof(TContext).Name}");
                }
            }

        }

        private static void CallSeader<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
             context.Database.Migrate();
             seeder(context, services);
        }

    }
}
