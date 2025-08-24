using FastAQ.Models.AQEntity;
using FastAQ.Models.Config.FAQMySQLConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FastAQ.Services.AQDBServices;

public class AQDBServices : DbContext
{   
    public DbSet<AQEntity> aQEntities { get; set; }
    public AQDBServices(IOptions<FAQMySQLConfig> options)
        : base(Prepare(options))
    {
    }

    

    public static DbContextOptions<AQDBServices> Prepare(IOptions<FAQMySQLConfig> config)

    {
        Console.WriteLine(config.Value.ConnectionStrings);
        var optionsBuilder = new DbContextOptionsBuilder<AQDBServices>();
        optionsBuilder.UseMySql(
            serverVersion:ServerVersion.AutoDetect(config.Value.ConnectionStrings) ,
            connectionString: config.Value.ConnectionStrings
            
        );
        return optionsBuilder.Options;
    }
}