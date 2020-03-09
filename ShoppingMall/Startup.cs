using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Caching.Memory;
using ShoppingMall.API.Middleware;

namespace ShoppingMall
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region  添加SwaggerUI

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "ShoppingMall API接口文档",
                    Version = "v1.0",
                    Description = "ASP.NET CORE WebApi for ShoppingMall",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "liangyp", Email = "liangyps@163.com", Url = "" }
                });
                //options.IgnoreObsoleteActions();
                //options.DocInclusionPredicate((docName, description) => true);
                //options.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Dinner.WebApi.xml"));
                //options.DescribeAllEnumsAsStrings();
                //options.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
                //添加读取注释服务
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var apiXmlPath = Path.Combine(basePath, "ShoppingMall.API.xml");
                options.IncludeXmlComments(apiXmlPath, true);
                var modelXmlPath = Path.Combine(basePath, "ShoppingMall.Model.xml");
                options.IncludeXmlComments(modelXmlPath);

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                options.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
            });

            #endregion

            //注册MemoryCache缓存
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            //添加JWT认证身份
            services.AddAuthorization(options =>
            {
                options.AddPolicy("System", policy => policy.RequireClaim("SystemType").Build());
                options.AddPolicy("Client", policy => policy.RequireClaim("ClientType").Build());
                options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());
            });

            #region AutoFac

            var builder = new ContainerBuilder();
            var assemblysServices = Assembly.Load("ShoppingMall.Services");
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();
            ////将services中的服务填充到Autofac中.
            builder.Populate(services);

            var assemblyRepository = Assembly.Load("ShoppingMall.Repository");
            builder.RegisterAssemblyTypes(assemblyRepository).AsImplementedInterfaces();

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //使用JWT身份认证中间件
            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseMvc();

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });

            #endregion
        }
    }
}
