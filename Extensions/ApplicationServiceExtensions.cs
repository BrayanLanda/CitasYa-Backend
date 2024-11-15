using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CordiSimple.Errors.Global;
using CordiSimple.Interfaces;
using CordiSimple.Models;
using CordiSimple.Services;
using Microsoft.AspNetCore.Identity;

namespace CordiSimple.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Configurar el comportamiento del serializador JSON para manejar ciclos de referencia
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddCors();

            // Registrar los repositorios genéricos
            services.AddScoped<ITokenRepository, TokenServices>();
            services.AddScoped<IAuthRepository, AuthService>();
            services.AddScoped<IUserRespository, UserService>();
            // services.AddScoped<IRoomRepository, RoomService>();
            // services.AddScoped<IRoomTypeRepository, RoomTypeService>();
            // services.AddScoped<IBookingRepository, BookingService>();
            // services.AddScoped<IGuestRepository, GuestService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            // services.AddScoped<DataSeeder>();

            // Registrar el GlobalExceptionFilter
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            return services;
        }
    }
}