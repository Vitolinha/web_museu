using Microsoft.EntityFrameworkCore;
using web_museu.Data;
using web_museu.Repositorio;
using Microsoft.AspNetCore.Http;
using web_museu.Helper;
using web_museu.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conexão com o banco de dados
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoBancoSQL")));

// Registrar IHttpContextAccessor como Singleton
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registrar serviços do repositório
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IQuestionarioRepositorio, QuestionarioRepositorio>();

builder.Services.AddSingleton<EmailService>();

// Registrar serviço para gerenciar sessões
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();
// Configurar opções de sessão
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define tempo de expiração da sessão
});

// Adicionar suporte a MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Habilitar middleware de sessão
app.UseAuthorization();

// Configurar rotas padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
