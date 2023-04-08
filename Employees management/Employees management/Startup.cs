using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Serialization;

public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        //Enable cors
        services.AddCors(C =>
        {
            C.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        //Json Serialzer
        services.AddControllersWithViews().AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver=new DefaultContractResolver());
        services.AddControllers();
        services.AddRazorPages();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        //Enable CORS 
        app.UseCors(options =>options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}