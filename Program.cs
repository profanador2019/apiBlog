using apiBlog.Repository;
using apiBlog.Services;
using apiBlog.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<Context>("Data Source=Blog.db");
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuariosRepository>();
builder.Services.AddScoped<RolesRepository>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<ComentarioService>();
builder.Services.AddScoped<ComentarioRepository>();

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

app.Run();
