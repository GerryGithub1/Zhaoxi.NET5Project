using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.NET5Project.Web.Interface;

namespace Zhaoxi.NET5Project.Web.Service
{
    public class ServiceA : IServiceA
    {
        public void TestA()
        {
            Console.WriteLine("我是ServiceA->TestA");
        }
    }
}
