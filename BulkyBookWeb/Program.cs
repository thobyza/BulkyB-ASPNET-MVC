using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// [!] Add services here, to the container. also injection
builder.Services.AddControllersWithViews();

/* [!] Tell the app to use DbContext (inside ApplicationDBContext),
 * and use SQL Server using the connection string (inside appsetting.json)
 * 
 * GetConnectionString => special method, that will only look for the value in the parameters 
 * (here "DefaultConnection"), inside of ConnectionStrings block in appsettings.json
*/
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// [!] Adding razor runtime compilation, to update UI on save changes (is this still necessary?)
object value = builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request PIPELINE.
/* Pipeline ?? => pipeline specifies how ur app should respond to a web request
 * the request will go back and forth through the pipeline
 * 
 * pipline consist of middlewares: Auth, MVC, Statis files, etc.
 * the order in the pipeline is important
 */
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // => wwwroot folder

app.UseRouting(); 

app.UseAuthorization(); // auth middleware

/* Routing in MVC 
 * we have Controllers and Action
 */

app.MapControllerRoute(
    // this is the default route if there is no controller and action
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
