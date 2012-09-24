using System.Collections.Generic;
using AutoMapper;
using DynamicLoopIoC.Domain.Entities;
using DynamicLoopIoC.Domain.Repositories;
using DynamicLoopIoC.Models.Mapping;
using DynamicLoopIoC.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLoopIoC.Tests
{
    [TestClass]
    public class MappingTests
    {
        [ClassInitialize]
        public static void CreateMapping(TestContext context)
        {
            AutoMapperConfiguration.Configure();
        }

        [TestMethod]
        public void Should_Map_An_Author_To_Its_Model()
        {
            var model = Mapper.Map<Author, AuthorModel>(new Author
                                                {
                                                    Id = 12,
                                                    FirstName = "Bruce",
                                                    LastName = "Wayne"
                                                });
            Assert.AreEqual(12, model.Id);
            Assert.AreEqual("Bruce", model.FirstName);
            Assert.AreEqual("Wayne", model.LastName);
            Assert.IsFalse(model.IsEditMode);
        }

        [TestMethod]
        public void Should_Map_An_Authors_List_To_Its_Model()
        {
            var model = Mapper.Map<IEnumerable<Author>, AuthorsListModel>(new List<Author>
            {
                new Author
                    {
                        Id = 12,
                        FirstName = "Bruce",
                        LastName = "Wayne",
                    },
                 new Author
                    {
                        Id = 13,
                        FirstName = "Dick",
                        LastName = "Grayson",
                    }
            });
            Assert.AreEqual(2, model.Authors.Count);
            Assert.AreEqual(12, model.Authors[0].Id);
            Assert.AreEqual("Bruce Wayne", model.Authors[0].FullName);
            Assert.AreEqual(13, model.Authors[1].Id);
            Assert.AreEqual("Dick Grayson", model.Authors[1].FullName);
        }

        [TestMethod]
        public void Should_Map_A_Model_To_An_Author()
        {
            var author = Mapper.Map<AuthorModel, Author>(new AuthorModel
            {
                Id = 12,
                FirstName = "Bruce",
                LastName = "Wayne",
                IsEditMode = true
            });
            Assert.AreEqual(12, author.Id);
            Assert.AreEqual("Bruce", author.FirstName);
            Assert.AreEqual("Wayne", author.LastName);
        }

        [TestMethod]
        public void Should_Map_A_Book_To_Its_Model()
        {
            var repository = new AuthorRepository();
            var bruce = repository.Insert(new Author
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });
            var author = repository.Insert(new Author
                                  {
                                      FirstName = "Cormac",
                                      LastName = "McCarthy"
                                  });

            var model = Mapper.Map<Book, BookModel>(new Book(repository)
            {
                Id = 5,
                Title = "The Road",
                ISBN = "1234567891234",
                AuthorId = author.Id
            });
            Assert.AreEqual(5, model.Id);
            Assert.AreEqual("The Road", model.Title);
            Assert.AreEqual("1234567891234", model.ISBN);
            Assert.AreEqual(author.Id, model.AuthorId);
            Assert.AreEqual(false, model.IsEditMode);
            Assert.AreEqual(2, model.Authors.Count);
            Assert.AreEqual(bruce.Id, model.Authors[0].Id);
            Assert.AreEqual("Bruce Wayne", model.Authors[0].FullName);
            Assert.AreEqual(author.Id, model.Authors[1].Id);
            Assert.AreEqual("Cormac McCarthy", model.Authors[1].FullName);
            Assert.IsFalse(model.IsEditMode);
        }

        [TestMethod]
        public void Should_Map_A_Books_List_To_Its_Model()
        {
            var repository = new AuthorRepository();
            var bruce = repository.Insert(new Author
            {
                FirstName = "Bruce",
                LastName = "Wayne"
            });
            var author = repository.Insert(new Author
            {
                FirstName = "Cormac",
                LastName = "McCarthy"
            });

            var model = Mapper.Map<IEnumerable<Book>, BooksListModel>(new List<Book>
            {
                new Book(repository)
                    {
                        Id = 5,
                        Title = "The Road",
                        ISBN = "1234567891234",
                        AuthorId = author.Id
                    },
                 new Book(repository)
                    {
                        Id = 6,
                        Title = "The Bat",
                        ISBN = "2345678912345",
                        AuthorId = bruce.Id
                    }
            });
            Assert.AreEqual(2, model.Books.Count);
            Assert.AreEqual(5, model.Books[0].Id);
            Assert.AreEqual("The Road", model.Books[0].Title);
            Assert.AreEqual("1234567891234", model.Books[0].ISBN);
            Assert.AreEqual("Cormac McCarthy", model.Books[0].AuthorFullName);
            Assert.AreEqual(6, model.Books[1].Id);
            Assert.AreEqual("The Bat", model.Books[1].Title);
            Assert.AreEqual("2345678912345", model.Books[1].ISBN);
            Assert.AreEqual("Bruce Wayne", model.Books[1].AuthorFullName);
        }

        [TestMethod]
        public void Should_Map_A_Model_To_A_Book()
        {
            var repository = new AuthorRepository();
            var author = repository.Insert(new Author
            {
                FirstName = "Cormac",
                LastName = "McCarthy"
            });

            var book = new BookRepository(repository).CreateNew();

            book = Mapper.Map<BookModel, Book>(new BookModel
            {
                Id = 5,
                Title = "The Road",
                ISBN = "1234567891234",
                AuthorId = author.Id,
                IsEditMode = false
            }, book);

            Assert.AreEqual(5, book.Id);
            Assert.AreEqual("The Road", book.Title);
            Assert.AreEqual("1234567891234", book.ISBN);
            Assert.AreEqual(author.Id, book.AuthorId);
        }
    }
}
