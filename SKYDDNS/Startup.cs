using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SKYDDNS.Cloudflare;
using SKYDDNS.Common;

namespace SKYDDNS
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddHangfire(options => options
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseRecommendedSerializerSettings()
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseMemoryStorage()
            //);
            //services.AddHangfireServer();

            //services.AddMvc();

            services.AddSingleton<IMainServices, MainServices>();
            services.AddHttpApi<ICloudflareApi>();
            services.AddHttpApi<ICommonApi>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles();

            //var api = app.ApplicationServices.GetRequiredService<ICloudflareApi>();
            //var res = api.GetDNSRecordsAsync("63edff574db2a5975e3e86c9746c0e15").GetAwaiter().GetResult();
            var main = app.ApplicationServices.GetRequiredService<IMainServices>();
            main.InitTaskAsnyc().GetAwaiter().GetResult();

            //app.UseRouting();



            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
