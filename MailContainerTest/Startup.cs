using MailContainerTest.Data.Repository;
using MailContainerTest.ExceptionMiddleware;
using MailContainerTest.Services.IServices;
using MailContainerTest.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static MailContainerTest.Models.Enums;

namespace MailContainerTest
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
          
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            DataStore dataStore = (DataStore)Enum.Parse(typeof(DataStore), Configuration.GetValue<string>("DataStore"),true);
            switch (dataStore)
            {
                case DataStore.Backup:
                    services.AddTransient<IMailDataStoreContainerService>(s =>  new MailDataStoreContainerService(new BackupMailContainerDataStoreRepository()));
                    break;
                default:
                    services.AddTransient<IMailDataStoreContainerService>(s => new MailDataStoreContainerService(new MailContainerDataStoreRepository()));
                    break;
            }
            
            services.AddTransient<LargeLetterMailTransferService>();
            services.AddTransient<SmallParcelMailTransferService>();
            services.AddTransient<StandardLetterParcelService>();

            services.AddTransient<Func<MailType, IMailTransferService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case MailType.LargeLetter:
                        return serviceProvider.GetService<LargeLetterMailTransferService>();
                    case MailType.SmallParcel:
                        return serviceProvider.GetService<SmallParcelMailTransferService>();
                    case MailType.StandardLetter:
                        return serviceProvider.GetService<StandardLetterParcelService>();
                    default:
                        throw new KeyNotFoundException();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
