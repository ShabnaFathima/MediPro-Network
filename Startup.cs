using MediProApi.BOL;
using Microsoft.OpenApi.Models;
using System.Text;

namespace MediProApi
{
   

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllers();
            services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Medipro",
                    Description = "API for Medipro application",
                    Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "xeno@gmail.com",
                        Name = "Xeno Erp Pvt Ltd",
                        Url = new Uri("http://www.xenoerp.com/")
                    }
                });
            });

            var AppConfigSection = Configuration.GetSection("App_Config");
            services.Configure<App_Config>(AppConfigSection);

            var app_config = AppConfigSection.Get<App_Config>();
            var secret = Encoding.ASCII.GetBytes(app_config.Secret);
            if (app_config != null)
            {
                Utility.LicenceName = app_config.BuildName + "API" ?? "";
                Utility.LicenceValue = app_config.BuildValue ?? "";
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            //New
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobTrackingAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "HMS v1"));

            app.UseRouting();

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyHeader();
                x.AllowAnyMethod();

            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConnectionBOL.ConStr = Configuration.GetConnectionString("ConStr");
        }

    }
}
