using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Services;

public class ConnectionStrings
{
    public string? DefaultDatabase { get; set; }
}

internal class Program
{
    private static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        // get connection strings

        ConnectionStrings connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

        // Add controllers to the container.

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
        });

        // Add services to the container.

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Swagger API",
                Description = "Api Swagger Documentation v1.0"
            });
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISkillRepository, SkillRepository>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<IRecordRepository, RecordRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        builder.Services.AddDbContext<MatrixContext>(
            dbContextOptions =>
            dbContextOptions.UseSqlServer(connectionStrings.DefaultDatabase)
        );

        //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddApiVersioning(setupAction =>
        {
            setupAction.AssumeDefaultVersionWhenUnspecified = true;
            setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            setupAction.ReportApiVersions = true;
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseCors("AllowAllOrigins");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Swagger Documentation v1.0");
            });
            //app.UseDeveloperExceptionPage();
        }
        else
        {
            //app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseExceptionHandler("/Home/Error");

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();


        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<MatrixContext>();
            db.Database.Migrate();
        }


        app.Run();
    }
}