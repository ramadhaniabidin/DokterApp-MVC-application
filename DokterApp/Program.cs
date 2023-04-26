using DokterApp.Repositories;
using DokterApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace DokterApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IDoctorSpecializationRepository, DoctorSpecializationRepository>();
            builder.Services.AddScoped<IDoctorSpecializationService, DoctorSpecializationService>(); 
            builder.Services.AddScoped<IJadwalDokterRepository, JadwalDokterRepository>();
            builder.Services.AddScoped<IJadwalDokterService, JadwalDokterService>();    
            builder.Services.AddScoped<IPoliRepository, PoliRepository>();
            builder.Services.AddScoped<IPoliService, PoliService>();
            builder.Services.AddScoped<IDokterPoliRepository, DokterPoliRepository>();
            builder.Services.AddScoped<IDokterPoliService, DokterPoliService>();
            builder.Services.AddScoped<ICariRepository, CariRepository>();
            builder.Services.AddScoped<ICariService, CariService>();


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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}