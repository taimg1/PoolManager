
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PoolMS.API.Auth;
using PoolMS.API.OptionsSetup;
using PoolMS.Core;
using PoolMS.Core.Entities;
using PoolMS.Repository;
using PoolMS.Repository.Interface;
using PoolMS.Service.DataConvertor;
using PoolMS.Service.Interface;
using PoolMS.Service.JWT;

namespace PoolMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<Seed>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pool", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                        });
            });
            
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IRepository<Payment>, PaymentRepository>();
            builder.Services.AddScoped<IRepository<Pool>,PoolRepository>();
            builder.Services.AddScoped<IRepository<PoolSize>, PoolSizeRepository>();
            builder.Services.AddScoped<IRepository<Reservation>, ReservationRepository>();
            builder.Services.AddScoped<IRepository<Subscription>, SubscriptionRepository>();   
            builder.Services.AddScoped<IRepository<SubType>,SubTypeRepository>();    
            builder.Services.AddScoped<IRepository<Visit>,VisitRepository>();    
            builder.Services.AddScoped<IRepository<Role>,RoleRepository>();
            builder.Services.AddScoped<IJwtProvider,JwtProvider>();



            //builder.Services.AddScoped(typeof(ICsvDataConvertor<>), typeof(CSVDataConvertor<>));


            builder.Services.AddScoped<UserRepository>();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                                                             .AllowAnyHeader()
                                                             .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowOrigin");

            app.UseAuthMiddleware();
            app.UseGetUserMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
