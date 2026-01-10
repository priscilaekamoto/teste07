using api.Application.Pessoas.Commands;
using api.Application.Pessoas.Handlers;
using api.Application.Pessoas.Queries;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Application.Mediator.Dispatcher;
using api.Application.Mediator.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using api.Application.Pessoas.Validators;
using api.Application.Categorias.Commands;
using api.Application.Categorias.Handlers;
using api.Application.Categorias.Queries;
using api.Application.Transacoes.Commands;
using api.Application.Transacoes.Handlers;
using api.Application.Transacoes.Queries;
using api.Application.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDispatcher, Dispatcher>();

// register unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// register handlers (use os tipos de Interfaces corretos)
builder.Services.AddScoped<IQueryHandler<GetAllPessoasQuery, List<PessoaDto>>, GetAllPessoasHandler>();
builder.Services.AddScoped<IQueryHandler<GetPessoaByIdQuery, PessoaDto?>, GetPessoaByIdHandler>();
builder.Services.AddScoped<ICommandHandler<CreatePessoaCommand, PessoaDto>, CreatePessoaHandler>();
builder.Services.AddScoped<ICommandHandler<DeletePessoaCommand, bool>, DeletePessoaHandler>();
builder.Services.AddScoped<ICommandHandler<CreateCategoriaCommand, CategoriaDto>, CreateCategoriaHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllCategoriasQuery, List<CategoriaDto>>, GetAllCategoriasHandler>();
builder.Services.AddScoped<IQueryHandler<GetCategoriaByIdQuery, CategoriaDto?>, GetCategoriaByIdHandler>();
builder.Services.AddScoped<ICommandHandler<CreateTransacaoCommand, TransacaoDto>, CreateTransacaoHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllTransacoesQuery, List<TransacaoDto>>, GetAllTransacoesHandler>();
builder.Services.AddScoped<IQueryHandler<GetTransacaoByIdQuery, TransacaoDto?>, GetTransacaoByIdHandler>();

// Add services to the container.
builder.Services.AddControllers();

// FluentValidation: ativa validação automática e registra validators do assembly
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePessoaCommandValidator>();

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
