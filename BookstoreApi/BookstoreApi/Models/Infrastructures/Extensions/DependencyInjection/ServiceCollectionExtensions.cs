using System.Reflection;

namespace BookstoreApi.Models.Infrastructures.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServicesByConvention(this IServiceCollection services, string @namespace)
        {
            // 使用反射從目前執行的組件中抓出所有型別
            var types = Assembly.GetExecutingAssembly().GetTypes();

            // 篩選出目標介面
            var interfaces = types
                .Where(t => t.IsInterface && t.Namespace != null && t.Namespace.Contains(@namespace))
                .ToList();

            // 找出實作這個介面的類別
            foreach (var iface in interfaces)
            {
                var implementation = types
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && iface.IsAssignableFrom(t));

                // 若找到實作，進行 DI 註冊
                if (implementation != null)
                {
                    services.AddScoped(iface, implementation);
                }
            }
        }
    }
}
