using System;
using Xunit;
using MOQ1;
using MOQ1.Controllers;
using Microsoft.EntityFrameworkCore;
using MOQ1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MOQTest1
{
    public class UnitTest1
    {        [Fact]
        public void Test1()
        {
            ValuesController valuesController;

            MockLaptopContext<LaptopContext> mock=new MockLaptopContext<LaptopContext>();

            valuesController = new ValuesController(mock.GetLaptopContext());

            var response = valuesController.Get() as ObjectResult;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public void Test12()
        {
            ValuesController valuesController;

            MockLaptopContext<LaptopContext> mock = new MockLaptopContext<LaptopContext>();

            valuesController = new ValuesController(mock.GetLaptopContext());

            var response = valuesController.Post(new Laptop { ID = 1221, Name = "Acer", Price = 90000, RAM = "8GB" }) as ObjectResult;

            Assert.Equal(200, response.StatusCode);
        }
    }
}
