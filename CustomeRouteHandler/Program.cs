using CustomeRouteHandler.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//app.UseEndpoints(endpoints => 
//    endpoints.Map("example-route", async c =>
//{
//    //https://localhost:5001/example-route endpoint'e gelen herhangi bir istek Controller'dan ziyade direkt olarak buradaki fonksiyon tarafindan karsilanacaktir.
//}));


app.UseEndpoints(endpoints => endpoints.Map("example-route", new ExampleHandler().Handler()));

app.UseEndpoints(endpoints => endpoints.Map("image/{imageName}", new ImageHandler().Handler(app.Environment.WebRootPath)));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
