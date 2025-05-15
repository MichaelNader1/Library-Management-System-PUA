
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _unitOfWork.Repository<Book>()
                .GetAllAsync(include: q => q
                    .Include(b => b.Languages)
                    .Include(b => b.BookSubheadings)
                    .Include(b => b.Publishers)
                    .Include(b => b.Book_Colleges).ThenInclude(bc => bc.College)
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Supply_Method)
                );

            var result = books.Select(book => new
            {
                book.BookID,
                book.Classification_Number,
                book.ISBN,
                book.Title,
                book.Subtitle,
                book.Publish_Year,
                book.IsMissing,
                book.IsDamage,
                book.Notes,
                book.CoverImage,
                book.Attachment,
                book.Place_of_publication,
                book.Locator,
                book.IsLocked,
                book.Price,
                book.Donor_Name,
                book.Reciving_Date,
                Author = book.Author != null ? new { book.Author.Author_ID, book.Author.Author_Name } : null,
                Category = book.Category != null ? new { book.Category.Category_Id, book.Category.Category_Name } : null,
                Supply_Method = book.Supply_Method != null ? new { book.Supply_Method.Supply_Method_Id, book.Supply_Method.Supply_Method_Name } : null,
                Languages = book.Languages.Select(l => new { l.Language_Id, l.Language_Name }).ToList(),
                Subheadings = book.BookSubheadings.Select(s => new { s.Subheading_Id, s.Subheading_Name }).ToList(),
                Publishers = book.Publishers.Select(p => new { p.Publisher_ID, p.Publisher_Name }).ToList(),
                Colleges = book.Book_Colleges.Select(bc => new { bc.College.College_ID, bc.College.College_Name }).ToList()
            });

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _unitOfWork.Repository<Book>()
                .FindAsync(b => b.BookID == id, include: q => q
                    .Include(b => b.Languages)
                    .Include(b => b.BookSubheadings)
                    .Include(b => b.Publishers)
                    .Include(b => b.Book_Colleges).ThenInclude(bc => bc.College)
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .Include(b => b.Supply_Method)
                );

            if (book == null)
                return NotFound("Book not found");

            var result = new
            {
                book.BookID,
                book.Classification_Number,
                book.ISBN,
                book.Title,
                book.Subtitle,
                book.Publish_Year,
                book.IsMissing,
                book.IsDamage,
                book.Notes,
                book.CoverImage,
                book.Attachment,
                book.Place_of_publication,
                book.Locator,
                book.IsLocked,
                book.Price,
                book.Donor_Name,
                book.Reciving_Date,
                Authors = book.Author != null ? new { book.Author.Author_ID, book.Author.Author_Name } : null,
                Category = book.Category != null ? new { book.Category.Category_Id, book.Category.Category_Name } : null,
                Supply_Method = book.Supply_Method != null ? new { book.Supply_Method.Supply_Method_Id, book.Supply_Method.Supply_Method_Name } : null,
                Languages = book.Languages.Select(l => new { l.Language_Id, l.Language_Name }).ToList(),
                Subheadings = book.BookSubheadings.Select(s => new { s.Subheading_Id, s.Subheading_Name }).ToList(),
                Publishers = book.Publishers.Select(p => new { p.Publisher_ID, p.Publisher_Name }).ToList(),
                Colleges = book.Book_Colleges.Select(bc => new { bc.College.College_ID, bc.College.College_Name }).ToList()
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Invalid book data");

            await SetBookRelationships(book);
            await _unitOfWork.Repository<Book>().AddAsync(book);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.BookID }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null || id != book.BookID)
                return BadRequest("Invalid request");

            var existingBook = await _unitOfWork.Repository<Book>()
                .FindAsync(b => b.BookID == id, include: q => q
                    .Include(b => b.Languages)
                    .Include(b => b.BookSubheadings)
                    .Include(b => b.Publishers)
                    .Include(b => b.Book_Colleges)
                    .Include(b => b.Category)
                );

            if (existingBook == null)
                return NotFound("Book not found");

            existingBook.Classification_Number = book.Classification_Number;
            existingBook.ISBN = book.ISBN;
            existingBook.Title = book.Title;
            existingBook.Subtitle = book.Subtitle;
            existingBook.Publish_Year = book.Publish_Year;
            existingBook.IsMissing = book.IsMissing;
            existingBook.IsDamage = book.IsDamage;
            existingBook.Notes = book.Notes;
            existingBook.CoverImage = book.CoverImage;
            existingBook.Attachment = book.Attachment;
            existingBook.Place_of_publication = book.Place_of_publication;
            existingBook.Locator = book.Locator;
            existingBook.IsLocked = book.IsLocked;
            existingBook.Price = book.Price;
            existingBook.Donor_Name = book.Donor_Name;
            existingBook.Author_Id = book.Author_Id;
            existingBook.Category_Id = book.Category_Id;
            existingBook.Supply_Method_Id = book.Supply_Method_Id;

             existingBook.Languages.Clear();
            existingBook.BookSubheadings.Clear();
            existingBook.Publishers.Clear();
            existingBook.Book_Colleges.Clear();

            existingBook.Languages = (await _unitOfWork.Repository<Language>().GetAllAsync())
                .Where(l => book.LanguageIds.Contains(l.Language_Id)).ToList();

            existingBook.BookSubheadings = (await _unitOfWork.Repository<Subheading>().GetAllAsync())
                .Where(s => book.SubheadingIds.Contains(s.Subheading_Id)).ToList();

            existingBook.Publishers = (await _unitOfWork.Repository<Publisher>().GetAllAsync())
                .Where(p => book.PublisherIds.Contains(p.Publisher_ID)).ToList();

            var colleges = await _unitOfWork.Repository<College>()
                .GetAllAsync(q => q.Where(c => book.CollegeIds.Contains(c.College_ID)));

            existingBook.Book_Colleges.Clear();
            foreach (var college in colleges)
            {
                existingBook.Book_Colleges.Add(new Book_College
                {
                    Book_ID = existingBook.BookID,
                    College_ID = college.College_ID
                });
            }

            _unitOfWork.Repository<Book>().Update(existingBook);
            await _unitOfWork.SaveAsync();

            return Ok("Book Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _unitOfWork.Repository<Book>()
                .FindAsync(b => b.BookID == id, include: q => q.Include(b => b.Book_Colleges));

            if (book == null)
                return NotFound("Book not found");

            _unitOfWork.Repository<Book_College>().DeleteRange(book.Book_Colleges);
            _unitOfWork.Repository<Book>().Delete(book);

            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        private async Task SetBookRelationships(Book book)
        {
            book.Languages = (await _unitOfWork.Repository<Language>().GetAllAsync())
                .Where(l => book.LanguageIds.Contains(l.Language_Id)).ToList();

            book.BookSubheadings = (await _unitOfWork.Repository<Subheading>().GetAllAsync())
                .Where(s => book.SubheadingIds.Contains(s.Subheading_Id)).ToList();

            book.Publishers = (await _unitOfWork.Repository<Publisher>().GetAllAsync())
                .Where(p => book.PublisherIds.Contains(p.Publisher_ID)).ToList();

            var colleges = await _unitOfWork.Repository<College>()
                .GetAllAsync(q => q.Where(c => book.CollegeIds.Contains(c.College_ID)));

            book.Book_Colleges.Clear();
            foreach (var college in colleges)
            {
                book.Book_Colleges.Add(new Book_College
                {
                    Book_ID = book.BookID,
                    College_ID = college.College_ID
                });
            }
        }
    }
}
