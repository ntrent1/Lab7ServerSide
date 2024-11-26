using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Lab5.Data
{
    public class LibraryService
    {
        private readonly string usersFilePath;
        private readonly string booksFilePath;

        public LibraryService(string usersFilePath = "Data/Users.csv", string booksFilePath = "Data/Books.csv")
        {
            this.usersFilePath = usersFilePath;
            this.booksFilePath = booksFilePath;
        }

        private List<User> Users { get; set; } = new List<User>();
        private List<Book> Books { get; set; } = new List<Book>();
        private List<Book> BorrowedBooks { get; set; } = new List<Book>();

        public void ReadUsers()
        {
            Users = File.ReadAllLines(usersFilePath)
                .Select(line => line.Split(','))
                .Select(fields => new User { Id = int.Parse(fields[0]), Name = fields[1], Email = fields[2] })
                .ToList();
        }

        public void ReadBooks()
        {
            Books = File.ReadAllLines(booksFilePath)
                .Select(line => line.Split(','))
                .Select(fields => new Book { Id = int.Parse(fields[0]), Title = fields[1], Author = fields[2] })
                .ToList();
        }

        public void SaveUsers()
        {
            File.WriteAllLines(usersFilePath, Users.Select(u => $"{u.Id},{u.Name},{u.Email}"));
        }

        public void SaveBooks()
        {
            File.WriteAllLines(booksFilePath, Books.Select(b => $"{b.Id},{b.Title},{b.Author}"));
        }

        public void AddUser(User user)
        {
            Users.Add(user);
            SaveUsers();
        }

        public void EditUser(User user)
        {
            Users[Users.FindIndex(u => u.Id == user.Id)] = user;
            SaveUsers();
            
        }

        public void DeleteUser(int userId)
        {
            Users.RemoveAll(u => u.Id == userId);
            SaveUsers();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
            SaveBooks();
        }

        public void EditBook(Book book)
        {
            Books[Books.FindIndex(b => b.Id == book.Id)] = book;
            SaveBooks();
        }

        public void DeleteBook(int bookId)
        {
            Books.RemoveAll(b => b.Id == bookId);
            SaveBooks();
        }

        public void BorrowBook(Book book)
        {
            BorrowedBooks.Add(book);
        }

        public void ReturnBook(Book book)
        {
            BorrowedBooks.RemoveAll(b => b.Id == book.Id);
        }

        public List<User> GetAllUsers() => Users;
        public List<Book> GetAllBooks() => Books;
        public List<Book> GetBorrowedBooks() => BorrowedBooks;
    }
}
