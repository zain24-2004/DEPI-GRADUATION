using Online_Courses_2024.service.implementaion;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Courses_2024.Data;
using Online_Courses_2024.service.interfaces;
using Online_Courses_2024.ViewModel.Utilities;
using static Online_Courses_2024.ViewModel.Utilities.AutoMapperExtension;

namespace Online_Courses_2024
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var ConnectionString = builder.Configuration.GetConnectionString("con");
            builder.Services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer(ConnectionString));

            builder.Services.AddTransient<IcourseService, courseService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IInstructorService, InstructorService>();
            builder.Services.AddTransient<ILessonService, LessonService>();
            builder.Services.AddTransient<IAssignmentService, AssignmentService>();
            builder.Services.AddTransient<IProgressService, ProgressService>();
            builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            //builder.Services.AddAutoMapper(typeof(AssembleType));


            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

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
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
            app.Run();
        }
    }
}
