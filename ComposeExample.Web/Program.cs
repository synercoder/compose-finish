using Serilog;
using ComposeExample.Extensions;
using ComposeExample.MoviesClient;
using System.ComponentModel;

namespace ComposeExample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lets add serilog
            builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration).Enrich.WithProperty("Application", "ComposeExample.Web"));

            builder.Services.AddSingleton< IConfigurationRoot>(builder.Configuration);

            // Add services to the container.
            builder.Services.AddRazorPages()
                .AddJsonOptions(o => o.AddDateOnlySupport());

            TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));

            builder.Services.AddMoviesApiClient(builder.Configuration.GetSection("KnownUrls")["Api"]);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}