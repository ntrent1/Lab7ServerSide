namespace LibraryService.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Lab5.Data;
    using Xunit;

    public class LibraryServiceTests
    {
        private string testDirectory;

        public LibraryServiceTests()
        {
            testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestData");
            Directory.CreateDirectory(testDirectory);
            File.WriteAllText(Path.Combine(testDirectory, "Users.csv"), string.Empty);
            File.WriteAllText(Path.Combine(testDirectory, "Books.csv"), string.Empty);
        }

        private string GetTestFilePath(string fileName) => Path.Combine(testDirectory, fileName);

        [Fact]
        public void AddUser_ShouldAddUserToList()
        {
            // Arrange
            var service = new LibraryService(GetTestFilePath("Users.csv"), GetTestFilePath("Books.csv"));
            var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };

            // Act
            service.AddUser(user);

            // Assert
            var users = service.GetAllUsers();
            Assert.Single(users);
            Assert.Equal(user, users[0]);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUserFromList()
        {
            // Arrange
            var service = new LibraryService(GetTestFilePath("Users.csv"), GetTestFilePath("Books.csv"));
            var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            service.AddUser(user);

            // Act
            service.DeleteUser(user.Id);

            // Assert
            var users = service.GetAllUsers();
            Assert.Empty(users);
        }

        [Fact]
        public void AddBook_ShouldAddBookToList()
        {
            // Arrange
            var service = new LibraryService(GetTestFilePath("Users.csv"), GetTestFilePath("Books.csv"));
            var book = new Book { Id = 1, Title = "Book Title", Author = "Author Name" };

            // Act
            service.AddBook(book);

            // Assert
            var books = service.GetAllBooks();
            Assert.Single(books);
            Assert.Equal(book, books[0]);
        }

        [Fact]
        public void DeleteBook_ShouldRemoveBookFromList()
        {
            // Arrange
            var service = new LibraryService(GetTestFilePath("Users.csv"), GetTestFilePath("Books.csv"));
            var book = new Book { Id = 1, Title = "Book Title", Author = "Author Name" };
            service.AddBook(book);

            // Act
            service.DeleteBook(book.Id);

            // Assert
            var books = service.GetAllBooks();
            Assert.Empty(books);
        }

        [Fact]
        public void EditUser_ShouldUpdateUserDetails()
        {
            // Arrange
            var service = new LibraryService(GetTestFilePath("Users.csv"), GetTestFilePath("Books.csv"));
            var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            service.AddUser(user);

            // Act
            user.Name = "Jane Doe";
            service.EditUser(user);

            // Assert
            var updatedUser = service.GetAllUsers().Find(u => u.Id == user.Id);
            Assert.Equal("Jane Doe", updatedUser.Name);
        }

        [Fact]
        public void EditBook_ShouldUpdateBookDetails()
        {
            // Arrange
            var service = new LibraryService(GetTestFilePath("Users.csv"), GetTestFilePath("Books.csv"));
            var book = new Book { Id = 1, Title = "Book Title", Author = "Author Name" };
            service.AddBook(book);

            // Act
            book.Title = "New Book Title";
            service.EditBook(book);

            // Assert
            var updatedBook = service.GetAllBooks().Find(b => b.Id == book.Id);
            Assert.Equal("New Book Title", updatedBook.Title);
        }
    }


}