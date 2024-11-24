using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.EF;
using RepositoryPatternWithUOW.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using RepositoryPatternWithUOW.Core.Models;

namespace Task_Xceed
{
    public class Program
    {

       
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            // Add services to the container.
            builder.Services.AddControllersWithViews();


            #region DB
            // Database
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Session

            // Sessions
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //  IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            #endregion

            #region UOW
            // Unit Of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #endregion

            
            var app = builder.Build();

            #region Configuring

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }



            // Session Middleware
            app.UseSession();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            #endregion

            app.Run();
            
        }
    }
}
