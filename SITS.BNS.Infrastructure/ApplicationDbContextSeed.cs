using SITS.BNS.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITS.BNS.Infrastructure
{
    public static class ApplicationDbContextSeed
    {
        public static async Task<Task> SeedSampleDataAsync(ApplicationDbContext context)
        {

            if (!context.AppUsers.Any())
            {
                var appusers = new List<AppUser>
                {
                    new AppUser
                    {
                        FullName = "System Admin",
                        FirstName = "System", 
                        LastName = "Admin", 
                        Organization = "Kekirawa Division", 
                        OrganizationId = 0, 
                        Mobile = "077 203-8566",
                        IsActive = true,
                        Password = BCrypt.Net.BCrypt.HashPassword("123"),
                        UserEmail = "chathura.ishan9@gmail.com",
                        CreatedBy = "SYS",
                        CreatedByName = "SYS",
                        CreatedDate = DateTime.Now.ToUniversalTime(),
                        UpdatedBy = "SYS",
                        UpdatedByName = "SYS",
                        UpdatedDate = DateTime.Now.ToUniversalTime()
                    }

                };

                context.AppUsers.AddRange(appusers);
                await context.SaveChangesAsync();
            }

            if (!context.AppRoles.Any())
            {
                var approles = new List<AppRole>
                {
                    new AppRole
                    {
                        Name = "Admin",
                        Description = "Administrator",
                        IsActive = true,
                        CreatedBy = "SYS",
                        CreatedByName = "SYS",
                        CreatedDate = DateTime.Now.ToUniversalTime(),
                        UpdatedBy = "SYS",
                        UpdatedByName = "SYS",
                        UpdatedDate = DateTime.Now.ToUniversalTime()
                    },
                    new AppRole
                    {
                        Name = "User",
                        Description = "Normal User",
                        IsActive = true,
                        CreatedBy = "SYS",
                        CreatedByName = "SYS",
                        CreatedDate = DateTime.Now.ToUniversalTime(),
                        UpdatedBy = "SYS",
                        UpdatedByName = "SYS",
                        UpdatedDate = DateTime.Now.ToUniversalTime()
                    }

                };

                context.AppRoles.AddRange(approles);
                await context.SaveChangesAsync();
            }

            if (!context.AppUserRoles.Any())
            {
                var appuserroles = new List<AppUserRole>
                {
                    new AppUserRole
                    {
                        AppRoleId = 1,
                        AppUserId = 1,
                        IsActive = true,
                        CreatedBy = "SYS",
                        CreatedByName = "SYS",
                        CreatedDate = DateTime.Now.ToUniversalTime(),
                        UpdatedBy = "SYS",
                        UpdatedByName = "SYS",
                        UpdatedDate = DateTime.Now.ToUniversalTime()
                    }

                };

                context.AppUserRoles.AddRange(appuserroles);
                await context.SaveChangesAsync();
            }

            if (!context.GramaOffice.Any())
            {
                var companies = new List<GramaOffice>
                {
                    new GramaOffice { Name = "Nawakkulama", LocationCode = "605", Longitude = "8.195431", Latitude = "80.678950", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Moragoda", LocationCode = "606", Longitude = "8.167377", Latitude = "80.673488", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Keeriyagaswewa", LocationCode = "607", Longitude = "8.134366", Latitude = "80.675572", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Mahadivulwewa", LocationCode = "608", Longitude = "8.156015", Latitude = "80.713169", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Heenukkiriyawa", LocationCode = "609", Longitude = "8.095621", Latitude = "80.654252", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Ganewalpola", LocationCode = "610", Longitude = "8.089736", Latitude = "80.625701", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Mainiya Rambewa", LocationCode = "611", Longitude = "8.087413", Latitude = "80.614184", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kollankuttigama", LocationCode = "612", Longitude = "8.092433", Latitude = "80.592052", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Mamineeyawa", LocationCode = "613", Longitude = "8.113187", Latitude = "80.606648", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Thoruwewa", LocationCode = "614", Longitude = "8.114416", Latitude = "80.587066", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kele Puliyankulama", LocationCode = "615", Longitude = "8.146625", Latitude = "80.582115", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Ihala Puliyankulama", LocationCode = "616", Longitude = "8.148031", Latitude = "80.562195", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Maradankadawala", LocationCode = "617", Longitude = "8.123764", Latitude = "80.562697", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Olukaranda", LocationCode = "618", Longitude = "8.074625", Latitude = "80.593576", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Mudaperumagama", LocationCode = "619", Longitude = "8.066235", Latitude = "80.544311", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Dumriya Nagaraya", LocationCode = "620", Longitude = "8.061318", Latitude = "80.587749", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Ihalagama", LocationCode = "621", Longitude = "8.061707", Latitude = "80.553961", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Shasthrawelliya", LocationCode = "622", Longitude = "8.053641", Latitude = "80.565476", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Karukkankulama", LocationCode = "623", Longitude = "8.042358", Latitude = "80.550945", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Mailagaswewa", LocationCode = "624", Longitude = "8.037075", Latitude = "80.568282", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Neekiniyawa", LocationCode = "625", Longitude = "8.031040", Latitude = "80.578379", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Malawa", LocationCode = "626", Longitude = "8.037925", Latitude = "80.593624", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Maradankadawala Road", LocationCode = "627", Longitude = "8.051608", Latitude = "80.595794", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kekirawa Town", LocationCode = "628", Longitude = "8.037092", Latitude = "80.600320", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kuda Kekirawa", LocationCode = "629", Longitude = "8.043091", Latitude = "80.605445", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Mankadawala", LocationCode = "630", Longitude = "8.059089", Latitude = "80.607128", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Maldenipura", LocationCode = "631", Longitude = "8.053435", Latitude = "80.605175", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Embulgaswewa", LocationCode = "632", Longitude = "8.067425", Latitude = "80.632372", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Medawewa", LocationCode = "633", Longitude = "8.045317", Latitude = "80.647526", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Pothanegama", LocationCode = "634", Longitude = "8.037800", Latitude = "80.621024", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kumbukwewa", LocationCode = "635", Longitude = "8.022269", Latitude = "80.632178", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Rathmalkanda", LocationCode = "636", Longitude = "8.021787", Latitude = "80.655556", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Maha Kekirawa", LocationCode = "637", Longitude = "8.026248", Latitude = "80.608102", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Olombewa", LocationCode = "638", Longitude = "8.015452", Latitude = "80.586055", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Korasagalla", LocationCode = "639", Longitude = "8.008227", Latitude = "80.617981", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Medagama", LocationCode = "640", Longitude = "8.001267", Latitude = "80.650479", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Maha Elagamuwa", LocationCode = "641", Longitude = "7.992177", Latitude = "80.617490", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Pallehingura", LocationCode = "642", Longitude = "7.982379", Latitude = "80.644942", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Unagollewa", LocationCode = "643", Longitude = "7.996087", Latitude = "80.601685", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Horapola", LocationCode = "644", Longitude = "7.995957", Latitude = "80.584304", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Nidigama", LocationCode = "645", Longitude = "7.974100", Latitude = "80.607784", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Barawila", LocationCode = "646", Longitude = "7.963149", Latitude = "80.614848", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Murungahiti Kanda", LocationCode = "647", Longitude = "7.972967", Latitude = "80.629475", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kotagala", LocationCode = "648", Longitude = "7.955369", Latitude = "80.638303", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Nelbegama", LocationCode = "649", Longitude = "7.965384", Latitude = "80.634915", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Madatugama", LocationCode = "650", Longitude = "7.943831", Latitude = "80.627356", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kandalama East", LocationCode = "651", Longitude = "7.923334", Latitude = "80.649450", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kithulhitiyawa", LocationCode = "652", Longitude = "7.919331", Latitude = "80.636683", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Kandalama West", LocationCode = "653", Longitude = "7.918315", Latitude = "80.630607", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Dunumandalawa", LocationCode = "654", Longitude = "7.937547", Latitude = "80.621137", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Bandarapothana", LocationCode = "655", Longitude = "7.943316", Latitude = "80.604443", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Undurawa", LocationCode = "656", Longitude = "7.959731", Latitude = "80.572339", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow },
                    new GramaOffice { Name = "Dambewatana", LocationCode = "657", Longitude = "7.989463", Latitude = "80.565697", IsAvailable = true, IsActive = true, CreatedBy = "SYS", CreatedByName = "SYS", CreatedDate = DateTime.UtcNow, UpdatedBy = "SYS", UpdatedByName = "SYS", UpdatedDate = DateTime.UtcNow }
                };

                context.GramaOffice.AddRange(companies);
                await context.SaveChangesAsync();
            }

            //if (!context.Product.Any())
            //{
            //    var products = new List<Product>
            //    {

            //        new Product
            //        {
            //            ProductName = "Creadit Cards (Corporate)",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-CCC",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Credit Cards (Personal)",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-CCP",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Current Accounts",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-CAS",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Foreign Currency",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-FCY",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Foreign Remitance ",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-FRE",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Leasing",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-LEG",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Loans",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-LOS",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Mobile Banking",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-MBG",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Online Banking",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-OBG",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Pawning and Gold Loan",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-PGL",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Safe Deposit Lockers",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-SDL",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Savings Accounts",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-SAS",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Term Disposits/ Fixed Deposits",
            //            CompanyId = 1,
            //            ProductCode = "SAMP-TFD",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },

            //        new Product
            //        {
            //            ProductName = "Factoring",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-FAG",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Fixed Deposits",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-FDS",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Gold Finance",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-GFE",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Key Facts Documents",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-KFD",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Leasing",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-LEG",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Loans",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-LOS",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Payement Facility Method",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-PFM",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Savings",
            //            CompanyId = 2,
            //            ProductCode = "SIYA-SAS",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },

            //        new Product
            //        {
            //            ProductName = "Pawn and Gold Loan System",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSPG",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Meeting Management Solution",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSMM",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Fuel Card Solution",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSFC",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "ERP Solutions",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSER",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "PMS (Property Management System)",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSPM",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "RPA Solutions",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSRS",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Creadit Approval System",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSCA",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Learning Management Solution",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSLM",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Web Development",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-SSWD",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Email Solutions",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-ESES",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Cloud Solutions (Asure, AWS, GCP, Huawei)",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-ESCS",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Infrastructure Solutions",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-ESIS",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Network and Security Solutions",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-ESNS",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "SOC2 Practice",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-ESSP",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Smart Security and CCTV",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-MSCC",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "End User Computers (Laptop/ Desktop)",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-MSED",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Service Desk as a Service",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-MSSD",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "Smart Board Room",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-MSSB",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },
            //        new Product
            //        {
            //            ProductName = "AMC - Annual Maintenance Contract",
            //            CompanyId = 3,
            //            ProductCode = "SAMI-MSAM",
            //            Measurement = "Quantity",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        },

            //        new Product
            //        {
            //            ProductName = "Online Trading",
            //            CompanyId = 4,
            //            ProductCode = "SCSE-SPOT",
            //            Measurement = "Amount",
            //            IsActive = true,
            //            CreatedBy = "SYS",
            //            CreatedByName = "SYS",
            //            CreatedDate = DateTime.Now.ToUniversalTime(),
            //            UpdatedBy = "SYS",
            //            UpdatedByName = "SYS",
            //            UpdatedDate = DateTime.Now.ToUniversalTime()
            //        }
            //    };

            //    context.Product.AddRange(products);
            //    await context.SaveChangesAsync();
            //}


            if (!context.AppRoles.Any())
            {

            }
            return Task.CompletedTask;
        }
    }
}
