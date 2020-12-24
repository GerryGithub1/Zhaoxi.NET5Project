using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.NET5Project.DataRepositories;
using Zhaoxi.NET5Project.Models;
using Zhaoxi.NET5Project.Web.Interface;
using Zhaoxi.NET5Project.Web.Service;

namespace Zhaoxi.NET5Project
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
            services.AddSession();
            services.AddControllersWithViews();
            #region 注册数据服务
            services.AddTransient<IStudentRepository, MockStudentRepository>();
            #endregion
            #region 解决修改视图内容，必须编译后方可生效的问题
            //1.Nuget安装：Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            //2.配置使用services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            #endregion
            #region 添加Autofac
            /*{
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<MockStudentRepository>().As<IStudentRepository>();
                IContainer container = containerBuilder.Build();
                IStudentRepository studentRepository = container.Resolve<IStudentRepository>();
                Student student = studentRepository.GetStudent(1);
            }*/
            #endregion
            #region 构造函数注入
            /*{
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<ServiceA>().As<IServiceA>();
                containerBuilder.RegisterType<ServiceB>().As<IServiceB>();
                IContainer container = containerBuilder.Build();
                IServiceB serviceB = container.Resolve<IServiceB>();
                serviceB.CallServiceA();
                Console.WriteLine("ddd");
            }*/
            #endregion
            #region 属性注入
            {
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<ServiceA>().As<IServiceA>();
                containerBuilder.RegisterType<ServiceB>().As<IServiceB>().PropertiesAutowired();
                IContainer container = containerBuilder.Build();
                IServiceB serviceB = container.Resolve<IServiceB>();
                serviceB.CallServiceA();
                Console.WriteLine("ddd");
            }
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // 处理静态文件
            app.UseStaticFiles();
            /*app.Use(async (context,next) =>
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                // await context.Response.WriteAsync("hello world");
                throw new Exception("发生了异常");
            });*/
            //app.Use(async (context,next) => {
            //    // 防止乱码
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    // SecretsKey
            //    await context.Response.WriteAsync($"{Configuration["MyKey"]}_{Configuration["SecretsKey"]}");
            //    await next();
            //});

            //app.Use(async(context, next) => {
            //    await context.Response.WriteAsync("Hello NET5444=====");
            //    await next();
            //});
            //app.Run(async (context) => {
            //    await context.Response.WriteAsync("Hello NET5=====");
            //    logger.LogInformation("处理请求并生成响应");
            //});
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
