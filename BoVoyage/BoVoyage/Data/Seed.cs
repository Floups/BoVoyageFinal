using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public static class Seed
    {
        // Création des rôles et de l'utilisateur administrateur
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // On définit des rôles 
            string[] roleNames = { "Admin", "Manager", "Member" };

            // On les ajoute dans la base s'ils n'existent pas déjà
            foreach (string roleName in roleNames)
            {
                bool roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await RoleManager.CreateAsync(new IdentityRole(roleName));
            }

            // On crée l'utilisateur administrateur à partir des infos stockées
            // dans les paramètres de l'appli
            IConfigurationSection userSettings = Configuration.GetSection("UserSettings");
            var admin = new IdentityUser
            {
                UserName = userSettings["UserName"],
                Email = userSettings["UserEmail"],
                EmailConfirmed = true
            };
            string pwd = userSettings["UserPassword"];

            // Si l'utilisateur admin n'existe pas déjà dans la base, on l'ajoute
            // puis on lui affecte le rôle administrateur
            IdentityUser _user = await UserManager.FindByEmailAsync(userSettings["UserEmail"]);
            if (_user == null)
            {
                IdentityResult createAdmin = await UserManager.CreateAsync(admin, pwd);
                if (createAdmin.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
