using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NMS.Initializer;
using NMS.Data;
using NMS.Logger;
//using NMS.Repository;
using NMS.Core.IRepositories;
using NMS.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Note System Management", Version = "v1" });
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<INoteRepository,NoteRepository>();


//builder.Services.AddScoped<CategoryRepository>(); // 
//builder.Services.AddScoped<NoteRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Logging.NMSFileLogger(options =>
{
    builder.Configuration.GetSection("Logging").GetSection("NMS").GetSection("Options").Bind(options);
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    // var services = scope.ServiceProvider;
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

    dbInitializer.Initialize();

}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//var dbInitializer = app.Services.GetRequiredService<IDbInitializer>();
//// use dbInitializer
//dbInitializer.Initialize();
//app.Services.GetRequiredService<IDbInitializer>(); 
app.UseRouting();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Note System Management");
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Notes}/{action=Index}/{id?}");

app.Run();
