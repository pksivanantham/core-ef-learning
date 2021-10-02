using Microsoft.EntityFrameworkCore;
using core_ef_learning.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

public class BookServiceContext : DbContext
{
    private readonly IConfiguration configuration;
    private readonly ILogger<BookServiceContext> logger;

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public BookServiceContext(DbContextOptions<BookServiceContext> dbContextOptions, IConfiguration configuration, ILogger<BookServiceContext> logger) : base(dbContextOptions)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        logger.LogWarning("OnConfiguring");

        logger.LogInformation($"Is Options configured {optionsBuilder.IsConfigured}");

        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = configuration.GetConnectionString("BookService");

            optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.LogTo(Console.WriteLine);
        }

        

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        logger.LogWarning("OnModelCreating");
    }

    public override void Dispose()
    {
        logger.LogInformation("Disposing the context<--------");
    }

}