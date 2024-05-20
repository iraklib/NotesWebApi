using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace NotesWebApi
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var server   = configuration["MYSQL_SERVER_NAME"] ?? configuration.GetConnectionString("MYSQL_SERVER_NAME");
            var dataBase = configuration["MYSQL_DATABASE"]    ?? configuration.GetConnectionString("MYSQL_DATABASE");
            var userId   = configuration["MYSQL_USER"]        ?? configuration.GetConnectionString("MYSQL_USER");
            var password = configuration["MYSQL_PASSWORD"]    ?? configuration.GetConnectionString("MYSQL_PASSWORD");

            var connString = $"server={server}; database={dataBase}; userid={userId}; pwd={password};";
            
            Console.WriteLine("ConnectionString: {0}", connString);

            WaitForDBInit(connString);

            services.AddDbContext<AppDbContext>(o => o.UseMySql(connString, ServerVersion.AutoDetect(connString)));
        }

        // Try to connect to the db with exponential backoff on fail.
        private static void WaitForDBInit(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            int retries = 1;
            while (retries < 7)
            {
                try
                {
                    Console.WriteLine("Connecting to db. Trial: {0}", retries);
                    connection.Open();
                    connection.Close();
                    break;
                }
                catch (MySqlException)
                {
                    Thread.Sleep((int)Math.Pow(2, retries) * 1000);
                    retries++;
                }
            }
        }
    }
}
