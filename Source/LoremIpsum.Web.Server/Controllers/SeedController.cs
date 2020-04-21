using LoremIpsum.Core;
using LoremIpsum.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LoremIpsum.Core.GuidHelper;

namespace LoremIpsum.Web.Server
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SeedController : BaseController
    {
        public SeedController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager) :
            base(context, userManager, signInManager, roleManager)
        {
        }

        /// <summary>
        /// A route that will create database and tables and will seed initial data
        /// </summary>
        /// <returns></returns>
        ///
        [Route(ApiRoutes.Seed)]
        public async Task<IActionResult> SeedAsync()
        {
            // Make sure we have the database
            mContext.Database.EnsureCreated();

            //Seed Roles
            await SeedRolesAsync();

            //Seed Super Admin
            await AddSuperAdminUserAsync();

            await SeedEnterpriseSettingAsync();

            //return the Views/Seed/Seed.cshtml
            return View();
        }


        /// <summary>
        /// Seed Roles
        /// </summary>
        /// <returns></returns>
        private async Task SeedRolesAsync()
        {

            if (mContext.Roles.Any())
                return;

            var li = new List<IdentityRole> {
                new IdentityRole{Id= "80b1f8c8-0ec0-41f4-b5f6-d928f4c81395" , Name = AspNetRolesDefaults.SuperAdmin, NormalizedName = AspNetRolesDefaults.SuperAdmin.NormalizedUpper() },
                new IdentityRole{Id= "4bf02ec1-9dc5-4d69-bfcd-ac696c97dfc2",Name = AspNetRolesDefaults.Admin, NormalizedName = AspNetRolesDefaults.Admin.NormalizedUpper() },
                new IdentityRole{Id = "2aeef950-cdff-435a-bbd8-d486c39bd807",Name = AspNetRolesDefaults.Manager, NormalizedName = AspNetRolesDefaults.Manager.NormalizedUpper() },
                new IdentityRole{Id ="ca7eb731-73b7-4850-bbb9-1c2177d4bee2",Name = AspNetRolesDefaults.Employee, NormalizedName = AspNetRolesDefaults.Employee.NormalizedUpper() },
            };

            await mContext.Roles.AddRangeAsync(li);
            await mContext.SaveChangesAsync();
        }

        /// <summary>
        /// Add Super Admin User
        /// </summary>
        /// <returns></returns>
        private async Task AddSuperAdminUserAsync()
        {
            var adminID = GenerateNewGuid();

            //Check if existing
            if (mContext.Users.Any(x => x.UserName == "admin"))
                return;

            using var transaction = await mContext.Database.BeginTransactionAsync();

            //TODO: Eliminate try catch: Use BaseTaskManager
            try
            {
                var userId = GenerateNewGuid();
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "nelson.villacruz@ygroup.ph",
                    Id = userId,
                    EmailConfirmed = true,
                    PhoneNumber = "09509595882",
                    Employee = new Employee
                    {
                        Id = adminID,
                        FirstName = "Nelson",
                        LastName = "Villacruz",
                        BirthDate = new DateTimeOffset(1994, 10, 22, 0, 0, 0, 0, TimeSpan.FromSeconds(0)),
                        MiddleName = "Sequiña",
                        Gender = Gender.Male,
                        CreatedBy = userId,
                        ModifiedBy = userId
                    },
                };

                var success = await mUserManager.CreateAsync(user, "Password1234");

                if (success.Succeeded)
                    await mUserManager.AddToRolesAsync(user, new[] { AspNetRolesDefaults.SuperAdmin, AspNetRolesDefaults.Employee });
                else
                    throw new Exception(success.Errors.AggregateErrors());

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }


        }

        private async Task SeedEnterpriseSettingAsync()
        {
            if (mContext.EnterpriseSettings.Any())
                return;

            await mContext.EnterpriseSettings.AddAsync(new EnterpriseSetting
            {
                Id = GenerateNewGuid(),
                CompanyName = ""
            });

            await mContext.SaveChangesAsync();
        }
    }
}
