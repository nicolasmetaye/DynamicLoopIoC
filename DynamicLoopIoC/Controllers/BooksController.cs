using System.Web.Mvc;
using AutoMapper;
using DynamicLoopIoC.Domain.Entities;
using DynamicLoopIoC.Domain.Repositories;
using DynamicLoopIoC.Models.Models;

namespace DynamicLoopIoC.Controllers
{
    public class BooksController : Controller
    {
        IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ActionResult Add()
        {
            var model = Mapper.Map<Book, BookModel>(_bookRepository.CreateNew());
            model.IsEditMode = false;
            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
                return RedirectToAction("Index", "Home");

            var model = Mapper.Map<Book, BookModel>(book);
            model.IsEditMode = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var book = _bookRepository.CreateNew();
                book = Mapper.Map(model, book);
                _bookRepository.Insert(book);
                return RedirectToAction("Index", "Home", new { message = (int)BooksListSuccessMessage.BookAddedSuccesfully });
            }
            return Add();
        }

        [HttpPost]
        public ActionResult Edit(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var book = _bookRepository.CreateNew();
                book = Mapper.Map(model, book);
                _bookRepository.Save(book);
                return RedirectToAction("Index", "Home", new { message = (int)BooksListSuccessMessage.BookEditedSuccesfully });
            }
            return Edit(model.Id);
        }

        public ActionResult Delete(int id)
        {
            _bookRepository.Delete(id);
            return RedirectToAction("Index", "Home", new { message = (int)BooksListSuccessMessage.BookDeletedSuccesfully });
        }
    }
}
