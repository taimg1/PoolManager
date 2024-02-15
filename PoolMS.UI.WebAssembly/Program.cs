using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using PoolMS.UI.WebAssembly.Auth;
using Blazored.LocalStorage;
using PoolMS.UI.WebAssembly.Service;

namespace PoolMS.UI.WebAssembly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5208")});
            builder.Services.AddScoped<IService<PoolDto, PoolCreateDto, PoolUpdateDto>, PoolService>();
            builder.Services.AddScoped<IService<PoolSizeDto, PoolSizeCreateDto, PoolSizeUpdateDto>, PoolSizeService>();
            builder.Services.AddScoped<IService<ReservationDto, ReservationCreateDto, ReservationUpdateDto>, ReservationService>();
            builder.Services.AddScoped<IService<SubscriptionDto, SubscriptionCreateDto, SubscriptionUpdateDto>, SubscriptionService>();
            builder.Services.AddScoped<IService<VisitDto, VisitCreateDto, VisitUpdateDto>, VisitService>();
            builder.Services.AddScoped<IService<SubTypeDto, SubTypeCreateDto, SubTypeUpdateDto>, SubTypeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IService<RoleDto, RoleCreateDto, RoleUpdateDto>, RoleService>();
            builder.Services.AddScoped<IService<PaymentDto, PaymentCreateDto, PaymentUpdateDto>, PaymentService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AuthorizeAdmin>();
            builder.Services.AddScoped<SubscriptionService>();  
            builder.Services.AddAuthorizationCore();
           

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            await builder.Build().RunAsync();
        }
    }
}
