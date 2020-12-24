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
            #region ע�����ݷ���
            services.AddTransient<IStudentRepository, MockStudentRepository>();
            #endregion
            #region ����޸���ͼ���ݣ��������󷽿���Ч������
            //1.Nuget��װ��Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            //2.����ʹ��services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            #endregion
            #region ���Autofac
            /*{
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<MockStudentRepository>().As<IStudentRepository>();
                IContainer container = containerBuilder.Build();
                IStudentRepository studentRepository = container.Resolve<IStudentRepository>();
                Student student = studentRepository.GetStudent(1);
            }*/
            #endregion
            #region ���캯��ע��
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
            #region ����ע��
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
            // ����̬�ļ�
            app.UseStaticFiles();
            /*app.Use(async (context,next) =>
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                // await context.Response.WriteAsync("hello world");
                throw new Exception("�������쳣");
            });*/
            //app.Use(async (context,next) => {
            //    // ��ֹ����
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
            //    logger.LogInformation("��������������Ӧ");
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
