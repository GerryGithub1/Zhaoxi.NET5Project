using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.NET5Project.Models;

namespace Zhaoxi.NET5Project.DataRepositories
{
    public class MockStudentRepository : IStudentRepository
    {
        private ILogger<MockStudentRepository> _logger;
      
        private List<Student> _studentList;
        public MockStudentRepository()
        {
            Console.WriteLine("使用Autofac默认必须使用空的构造函数");
        }
        public MockStudentRepository(ILogger<MockStudentRepository> _logger)
        {
            this._logger = _logger;
            _studentList = new List<Student>()
            {
                new Student() { Id= 1, Name="张三",Major="Java" },
                new Student() { Id= 2, Name="李四",Major=".NET" },
                new Student() { Id= 3, Name="王五",Major="Python" }
            };
        }
        public Student GetStudent(int id)
        {
            _logger.LogInformation("执行GetStudent方法");
            return _studentList.FirstOrDefault(a => a.Id == id);
        }

        public List<Student> GetStudents()
        {
            return _studentList;
        }

        public void Save(Student student)
        {
            _logger.LogInformation("执行Save方法");
            _studentList.Add(student);
        }
    }
}
