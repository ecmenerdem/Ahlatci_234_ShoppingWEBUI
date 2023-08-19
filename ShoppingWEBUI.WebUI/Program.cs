using ShoppingWEBUI.Helper;
using ShoppingWEBUI.WebUI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers()
builder.Services.AddControllersWithViews().AddJsonOptions(opt=>opt.JsonSerializerOptions.PropertyNamingPolicy=null);
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(opt=>opt.HeaderName="XSRF-TOKEN");

var app = builder.Build();
AppHttpContext.ServiceProvider = app.Services;

app.UseGlobalExceptionMiddleware();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}"
    );

app.UseSessionNullCheckMiddleware();



app.Run();
