using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieStore.Controllers;
using MovieStore.Models;
using System.Linq;
using Moq;
using System.Data.Entity;


namespace MovieStore.Tests.Controllers {
    [TestClass]
    public class MovieStoreControllerTest {
        [TestMethod]
        public void MovieStore_Index_TestView_() {
            MoviesController controller = new MoviesController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_ListOfMovies() {
            MoviesController controller = new MoviesController();

            List<Movie> result = controller.ListOfMovies();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "King Pin", actual: result[0].Title);
            Assert.AreEqual(expected: "The Sound of Music", actual: result[1].Title);
            Assert.AreEqual(expected: "E.T. The Extra Terrestrial", actual: result[2].Title);
            Assert.AreEqual(expected: result.Count, actual: 3);
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_Success() {
            MoviesController controller = new MoviesController();

            RedirectToRouteResult result = controller.IndexRedirect(id: 1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Create", actual: result.RouteValues["action"]);
            Assert.AreEqual(expected: "HomeController", actual: result.RouteValues["controller"]);
            
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_BadRequest() {
            MoviesController controller = new MoviesController();

            HttpStatusCodeResult result = controller.IndexRedirect(id: 0) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void MovieStore_ListFromDb() {
            var list = new List<Movie> {
                new Movie{MovieId = 1, Title = "The Avengers", YearRelease = 2012},
                new Movie{MovieId = 2, Title = "Interview With A Vampire", YearRelease = 1999 }
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);

            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            ViewResult result = controller.ListFromDb() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_Details_Success() {
            var list = new List<Movie> {
                new Movie{MovieId = 1, Title = "The Avengers", YearRelease = 2012},
                new Movie{MovieId = 2, Title = "Interview With A Vampire", YearRelease = 1999 }
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(expression: m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            ViewResult result = controller.Details(id: 1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_Details_IdIsNull() {
            var list = new List<Movie> {
                new Movie{MovieId = 1, Title = "The Avengers", YearRelease = 2012},
                new Movie{MovieId = 2, Title = "Interview With A Vampire", YearRelease = 1999 }
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(expression: m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            ViewResult result = controller.Details(id: null) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MovieStore_Details_MovieIsNull() {
            var list = new List<Movie> {
                new Movie{MovieId = 1, Title = "The Avengers", YearRelease = 2012},
                new Movie{MovieId = 2, Title = "Interview With A Vampire", YearRelease = 1999 }
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);

            Movie movie = null;
            mockSet.Setup(expression: m => m.Find(It.IsAny<Object>())).Returns(movie);

            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Details(id: 3) as HttpStatusCodeResult;

            Assert.AreEqual(expected: HttpStatusCode.NotFound, actual: (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void MovieStore_Create_RedirectSuccess() {
            MoviesController controller = new MoviesController();

            RedirectToRouteResult result = controller.Create() as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Create", actual: result.RouteValues["action"]);
            Assert.AreEqual(expected: "HomeController", actual: result.RouteValues["controller"]);

        }

        [TestMethod]
        public void MovieStore_CreateNewMovie() {
            var list = new List<Movie> {
                new Movie{MovieId = 1, Title = "The Avengers", YearRelease = 2012},
                new Movie{MovieId = 2, Title = "Interview With A Vampire", YearRelease = 1999 }
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);

            Movie newMovie = new Movie {
                MovieId = 3,
                Title = "New Movie",
                YearRelease = 2019
            };

            mockSet.Setup(expression: m => m.Find(It.IsAny<Object>())).Returns(newMovie);

            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            RedirectToRouteResult result = controller.Create(newMovie) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_EditIsNull() {
            var list = new List<Movie> {
                new Movie{MovieId = 1, Title = "The Avengers", YearRelease = 2012},
                new Movie{MovieId = 2, Title = "Interview With A Vampire", YearRelease = 1999 }
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);

            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Edit(id: null) as HttpStatusCodeResult;

            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);
        }
    }
}
