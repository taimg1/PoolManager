using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PoolMS.Core.Entities;
using PoolMS.Repository.DTO;
using PoolMS.Service.DTO;
using PoolMS.Service.Interface;
using PoolMS.Service.JWT;
using PoolMS.UI.Components;
using PoolMS.UI.Interfaces;
using PoolMS.UI.Service;
using System.Text;

namespace PoolMS.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAddress"))});
            builder.Services.AddScoped<IService<PoolDto,PoolCreateDto,PoolUpdateDto>, PoolService>();
            builder.Services.AddScoped<IService<PoolSizeDto, PoolSizeCreateDto, PoolSizeUpdateDto>, PoolSizeService>();
            builder.Services.AddScoped<IService<ReservationDto, ReservationCreateDto, ReservationUpdateDto>, ReservationService>();
            builder.Services.AddScoped<IService<SubscriptionDto, SubscriptionCreateDto, SubscriptionUpdateDto>, SubscriptionService>();
            builder.Services.AddScoped<IService<VisitDto, VisitCreateDto, VisitUpdateDto>, VisitService>();
            builder.Services.AddScoped<IService<SubTypeDto, SubTypeCreateDto, SubTypeUpdateDto>, SubTypeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IService<RoleDto,RoleCreateDto, RoleUpdateDto>, RoleService>();
            builder.Services.AddScoped<IService<PaymentDto, PaymentCreateDto, Payment>, PaymentService>();




            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            app.UseStaticFiles();
            app.UseAntiforgery();



            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
