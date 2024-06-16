using BaobabBackEndService.Repository.Users;
using BaobabBackEndService.Repository.Coupons;
using BaobabBackEndService.Repository.Categories;
using BaobabBackEndService.Repository.MassiveCoupons;
using BaobabBackEndService.Services.Users;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.Services.categories;
using BaobabBackEndService.Services.MassiveCoupons;
using BaobabBackEndService.ExternalServices.Jwt;

namespace BaobabBackEndService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMassiveCouponsRepository, MassiveCouponsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICouponsRepository, CouponsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMassiveCouponsServices, MassiveCouponsServices>();
            services.AddScoped<ICategoriesServices, CategoryServices>();
            services.AddScoped<ICouponsServices, CouponsServices>();
            services.AddScoped<IUsersServices, UsersServices>();
            services.AddScoped<JwtService>();
            return services;
        }
    }
}
