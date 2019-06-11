using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Uniswap.Data.Indexes;
using Uniswap.Statistics.Api.Composition;
using Uniswap.Statistics.Api.Swagger;

namespace Uniswap.Statistics.Api
{
    public class Startup
    {
        public Startup(Options.Options options)
        {
            Options = options;
        }

        public Options.Options Options { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Uniswap Statistics API", Version = "v1"});
                c.DescribeAllEnumsAsStrings();
                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                c.DocumentFilter<LowercaseDocumentFilter>();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MongoModule(Options.Db));

            builder.RegisterModule<StatsModule>();

            builder.RegisterModule<ExchangeModule>();

            builder.RegisterModule<PriceModule>();
            
            builder.RegisterModule<ChartModule>();
            
            builder.RegisterModule<HistoryModule>();
            
            builder.RegisterModule<TickerModule>();
            
            builder.RegisterModule<UserModule>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                app.ApplicationServices.GetService<IIndexInitializer>().Initialize().GetAwaiter().GetResult();
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.EnableDeepLinking();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Uniswap Statistics API v1");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}