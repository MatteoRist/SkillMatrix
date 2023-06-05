using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();

builder.Services.AddDbContext<MatrixContext>(
    dbContextOptions => 
    dbContextOptions.UseSqlServer("Data Source=DELLTTRG1;Initial Catalog=skillsMatrix;Integrated Security=True;Trust Server Certificate=True"
));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MatrixContext>();
    db.Database.Migrate();
}


app.Run();
