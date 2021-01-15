using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.NET5Project.Web.Interface;

namespace Zhaoxi.NET5Project.Web.Service
{
    public class ServiceB : IServiceB
    {
        //public IServiceA ServiceA { get; set; }
        private IServiceA injectserviceA;
        private IServiceA serviceA;
        public ServiceB(IServiceA serviceA)
        {
            this.serviceA = serviceA;
            Console.WriteLine("调用空的构造函数");
        }

        public void SetDdd(IServiceA serviceA)
        {
            this.injectserviceA = serviceA;
        }

        /*public ServiceB(IServiceA serviceA)
        {
            this.serviceA = serviceA;
        }*/
        public void CallServiceA()
        {
            serviceA.TestA();
            //injectserviceA.TestA();
        }
    }
}
