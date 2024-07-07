using BookStore.API.Domain;
using BookStore.API.Services;
using BookStore.Infractructure;
using BookStore.Infrastructure.Mappers;
using BookStore.Persistence.DataAccess;
using BookStore.Persistence.Repository;
using BookStore.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Presentation;

public class Startup
{
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddCors(options => 
            options.AddPolicy("AllowAll", builder => builder
                .AllowAnyOrigin()
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .AllowAnyHeader())
        );
        
        services.AddDbContext<BookDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        
        services.AddAutoMapper(typeof(BookProfile));
        services.AddScoped<IRepository<Book>, Repository<Book>>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddSingleton<ValidationFilter>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", () => Results.Redirect("api/Books"));
        });
    }
}