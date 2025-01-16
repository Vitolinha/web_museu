using Microsoft.EntityFrameworkCore;
using web_museu.Data;
using web_museu.Repositorio;
using Microsoft.AspNetCore.Http;
using web_museu.Helper;
using web_museu.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conex�o com o banco de dados
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoBancoSQL")));

// Registrar IHttpContextAccessor como Singleton
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registrar servi�os do reposit�rio
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IQuestionarioRepositorio, QuestionarioRepositorio>();

builder.Services.AddSingleton<EmailService>();

// Registrar servi�o para gerenciar sess�es
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();
// Configurar op��es de sess�o
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define tempo de expira��o da sess�o
});

// Adicionar suporte a MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar o pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Habilitar middleware de sess�o
app.UseAuthorization();

// Configurar rotas padr�o
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
