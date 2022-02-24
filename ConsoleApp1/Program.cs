using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookService service = new BookService();
            var ret = service.GetAll();

            service.Dispose();
        }
    }
}
