using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.NET5Project.Web.Interface;

namespace Zhaoxi.NET5Project.Web.Service
{
    public class ServiceB : IServiceB
    {
        public IServiceA serviceA { get; set; }
        public ServiceB()
        {
            Console.WriteLine("调用空的构造函数");
        }

        /*public ServiceB(IServiceA serviceA)
        {
            this.serviceA = serviceA;
        }*/
        public void CallServiceA()
        {
            serviceA.TestA();
        }
    }
}
