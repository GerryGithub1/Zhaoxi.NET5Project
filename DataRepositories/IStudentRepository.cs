using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.NET5Project.Models;

namespace Zhaoxi.NET5Project.DataRepositories
{
    public  interface IStudentRepository
    {
        Student GetStudent(int id);
        void Save(Student student);
        List<Student> GetStudents();
    }
}
