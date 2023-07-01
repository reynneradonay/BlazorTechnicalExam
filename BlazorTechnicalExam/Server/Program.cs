using BlazorTechnicalExam.Server.Data;
using BlazorTechnicalExam.Server.Interfaces;
using BlazorTechnicalExam.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<BlazorTechnicalExamDbContext>(opt =>
    opt.UseInMemoryDatabase("BlazorTechnicalExam"));
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IImageLibraryService, ImageLibraryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BlazorTechnicalExamDbContext>();

    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
