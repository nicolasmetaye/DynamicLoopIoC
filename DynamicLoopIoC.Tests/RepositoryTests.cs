using System.Linq;
using DynamicLoopIoC.Domain.Entities;
using DynamicLoopIoC.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLoopIoC.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestCleanup]
        public void ClearContext()
        {
            new AuthorRepository().DeleteAll();
        }

        [TestMethod]
        public void Should_Add_An_Entity()
        {
            var repository = new AuthorRepository();
            var author = repository.Insert(new Author()
                                  {
                                      FirstName = "Bruce",
                                      LastName = "Wayne"
                                  });

            Assert.IsTrue(author.Id > 0);
            Assert.AreEqual("Bruce", author.FirstName);
            Assert.AreEqual("Wayne", author.LastName);

            var authorId = author.Id;

            author = repository.GetById(authorId);
            Assert.AreEqual(authorId, author.Id);
            Assert.AreEqual("Bruce", author.FirstName);
            Assert.AreEqual("Wayne", author.LastName);
        }

        [TestMethod]
        public void Should_Delete_An_Entity()
        {
            var repository = new AuthorRepository();
            var author = repository.Insert(new Author()
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });

            repository.Delete(author.Id);
            author = repository.GetById(author.Id);
            Assert.IsNull(author);
        }

        [TestMethod]
        public void Should_Save_An_Entity()
        {
            var repository = new AuthorRepository();
            var author = repository.Insert(new Author()
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });

            author.FirstName = "Dick";
            author.LastName = "Grayson";

            repository.Save(author);

            var authorId = author.Id;
            author = repository.GetById(authorId);

            Assert.AreEqual(authorId, author.Id);
            Assert.AreEqual("Dick", author.FirstName);
            Assert.AreEqual("Grayson", author.LastName);
        }

        [TestMethod]
        public void Should_Search_An_Entity()
        {
            var repository = new AuthorRepository();
            repository.Insert(new Author()
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });

            var authorToFind = repository.Insert(new Author()
            {
                FirstName = "Dick",
                LastName = "Grayson"
            });

            var authorsFound = repository.SearchFor(author => author.FirstName == "Dick" && author.LastName == "Grayson");

            Assert.AreEqual(1, authorsFound.Count());
            Assert.AreEqual(authorToFind.Id, authorsFound.First().Id);
        }

        [TestMethod]
        public void Should_Get_All_Entities()
        {
            var repository = new AuthorRepository();
            var bruce = repository.Insert(new Author()
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });

            var dick = repository.Insert(new Author()
            {
                FirstName = "Dick",
                LastName = "Grayson"
            });

            var authors = repository.GetAll();

            Assert.AreEqual(2, authors.Count());
            Assert.AreEqual(bruce.Id, authors.ToList()[0].Id);
            Assert.AreEqual(dick.Id, authors.ToList()[1].Id);
        }

        [TestMethod]
        public void Should_Find_An_Entity()
        {
            var repository = new AuthorRepository();
            repository.Insert(new Author()
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });

            repository.Insert(new Author()
            {
                FirstName = "Dick",
                LastName = "Grayson"
            });

            Assert.IsTrue(repository.Any(author => author.FirstName == "Dick" && author.LastName == "Grayson"));
        }

        [TestMethod]
        public void Should_Not_Find_An_Entity()
        {
            var repository = new AuthorRepository();
            repository.Insert(new Author()
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });

            Assert.IsFalse(repository.Any(author => author.FirstName == "Dick" && author.LastName == "Grayson"));
        }

        [TestMethod]
        public void Changing_An_Entity_Retrieved_By_The_Repository_Should_Not_Change_The_Stored_List()
        {
            var repository = new AuthorRepository();
            var author = repository.Insert(new Author()
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });

            var authorId = author.Id;

            author = repository.GetById(authorId);

            author.FirstName = "Dick";
            author.LastName = "Grayson";

            var authorStored = repository.GetById(authorId);
            Assert.AreEqual(authorId, authorStored.Id);
            Assert.AreEqual("Bruce", authorStored.FirstName);
            Assert.AreEqual("Wayne", authorStored.LastName);
        }
    }
}
