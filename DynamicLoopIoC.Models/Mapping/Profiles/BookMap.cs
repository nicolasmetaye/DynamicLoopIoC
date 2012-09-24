using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DynamicLoopIoC.Common.Helpers;
using DynamicLoopIoC.Domain.Entities;
using DynamicLoopIoC.Domain.Repositories;
using DynamicLoopIoC.Models.Models;

namespace DynamicLoopIoC.Models.Mapping.Profiles
{
    public class BookMap : Profile
    {
        public BookMap()
        {
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Book, BookModel>()
                .ForMember(model => model.Authors, expression => expression.ResolveUsing(book => GetAuthorsList()))
                .ForMember(model => model.IsEditMode, expression => expression.Ignore());
            Mapper.CreateMap<Book, BookListItemModel>()
                .ForMember(model => model.AuthorFullName, expression => expression.ResolveUsing(book => book.Author.FirstName + " " + book.Author.LastName));
            Mapper.CreateMap<IEnumerable<Book>, BooksListModel>()
                .ForMember(model => model.Books, expression => expression.ResolveUsing(books => books))
                .ForMember(model => model.SuccessMessage, expression => expression.Ignore());
            Mapper.CreateMap<BookModel, Book>()
                .ForMember(book => book.ISBN, expression => expression.ResolveUsing(model => ISBNFilter.Filter(model.ISBN)));
        }

        private List<AuthorListItemModel> GetAuthorsList()
        {
            return
                new AuthorRepository()
                .GetAll()
                .Select(author => new AuthorListItemModel { Id = author.Id, FullName = author.FirstName + " " + author.LastName })
                .ToList();
        }
    }
}