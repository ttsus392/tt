using Microsoft.EntityFrameworkCore;
using MOQ1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOQTest1
{
    internal class MockLaptopContext<T>
    {
        public LaptopContext GetLaptopContext()
        {
            var options = new DbContextOptionsBuilder<LaptopContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            var context = new LaptopContext(options);

            // CustomerData => table name
            context.Laptops.Add(new Laptop { ID=121, Name="Acer", Price=90000, RAM="8GB" });

            context.SaveChanges();
            return context;
        }
    }
}
