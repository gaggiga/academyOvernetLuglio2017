using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.WebApi.Controllers;
using Yoox.WebApi.Models;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Yoox.WebApi.Tests
{
    [TestClass]
    public class PersoneControllerTest
    {

        [TestMethod]
        public void GetPersone_CheckResult()
        {
            var data = new List<Persona>
            {
                new Persona { Id = 1, Nome = "Alex", Cognome = "Polevoi" },
                new Persona { Id = 2, Nome = "Giovanni", Cognome = "Improta" },
                new Persona { Id = 3, Nome = "Mauro", Cognome = "Sanna" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Persona>>();
            mockSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(c => c.Persone).Returns(mockSet.Object);

            var mdbs = new MyDbService(mockContext.Object);
            var controller = new PersoneController(mdbs);
            var persone = controller.GetPersone();

            foreach(var p in data)
            {
                Assert.IsTrue(persone.Contains(p));
            }
        }

        [TestMethod]
        public void GetPersona_CheckResult()
        {
            var data = new List<Persona>
            {
                new Persona { Id = 1, Nome = "Alex", Cognome = "Polevoi" },
                new Persona { Id = 2, Nome = "Giovanni", Cognome = "Improta" },
                new Persona { Id = 3, Nome = "Mauro", Cognome = "Sanna" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Persona>>();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
                   .Returns<object[]>(ids => data.FirstOrDefault(p => p.Id == (int)ids[0]));

            mockSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(c => c.Persone).Returns(mockSet.Object);

            var mdbs = new MyDbService(mockContext.Object);
            var controller = new PersoneController(mdbs);
            IHttpActionResult actionResult = controller.GetPersona(2);
            var contentResult = actionResult as OkNegotiatedContentResult<Persona>;

            Assert.AreEqual(data.First(p => p.Id == 2), contentResult.Content);
        }

        [TestMethod]
        public void GetPersona_NotFound()
        {
            var data = new List<Persona>().AsQueryable();

            var mockSet = new Mock<DbSet<Persona>>();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => null);

            mockSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(c => c.Persone).Returns(mockSet.Object);

            var mdbs = new MyDbService(mockContext.Object);
            var controller = new PersoneController(mdbs);
            IHttpActionResult actionResult = controller.GetPersona(2);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

    }
}
