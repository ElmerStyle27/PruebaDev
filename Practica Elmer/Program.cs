using MediatR;
using Microsoft.EntityFrameworkCore;
using Practica_Elmer.DB.Entities;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediatR(Assembly.GetCallingAssembly());
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
            opciones.UseSqlServer(connectionString));


builder.Services.AddControllers()
    /*.AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; ;
        o.SerializerSettings.Converters.Add(new TimespanConverter());
        o.SerializerSettings.Converters.Add(new StringEnumConverter());
    })*/
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
       // option.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCors(builder => builder
.WithOrigins("https://localhost:44363").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
