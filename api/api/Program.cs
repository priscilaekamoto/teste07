using api.Application.Pessoas.Commands;
using api.Application.Pessoas.Dtos;
using api.Application.Pessoas.Handlers;
using api.Application.Pessoas.Queries;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Application.Mediator.Dispatcher;
using api.Application.Mediator.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDispatcher, Dispatcher>();

// register unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// register handlers (use os tipos de Interfaces corretos)
builder.Services.AddScoped<IQueryHandler<GetAllPessoasQuery, List<PessoaDto>>, GetAllPessoasHandler>();
builder.Services.AddScoped<IQueryHandler<GetPessoaByIdQuery, PessoaDto?>, GetPessoaByIdHandler>();
builder.Services.AddScoped<ICommandHandler<CreatePessoaCommand, PessoaDto>, CreatePessoaHandler>();
builder.Services.AddScoped<ICommandHandler<DeletePessoaCommand, bool>, DeletePessoaHandler>();

// Add services to the container.
builder.Services.AddControllers();

// EF Core - DbContext (MySQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
