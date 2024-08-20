using System.Security.Cryptography;
using LibraryV2.Models;

namespace LibraryV2.Tests.Api.TestHelpers
{
    public class DataHelper
    {
        public static class BookHelper
        {
            public static Book CreateRandomBook()
            {
                return new()
                {
                    Title = Guid.NewGuid().ToString(),
                    Author = Guid.NewGuid().ToString(),
                    YearOfRelease = new Random().Next(1800, 2024)
                };
            }

            public static Book CreateBook(string title, string author, int yearOfRealease)
            {
                return new()
                {
                    Title = title,
                    Author = author,
                    YearOfRelease = yearOfRealease
                };
            }
        }

        public static class UserHelper
        {
            public static User CreateRandomUser()
            {
                return new()
                {
                    NickName = Guid.NewGuid().ToString(),
                    FullName = Guid.NewGuid().ToString(),
                    Password = new Random().Next().ToString()
                };
            }

            public static User CreateUser(string nickName, string fullName, string password)
            {
                return new()
                {
                    NickName = nickName,
                    FullName = fullName,
                    Password = password
                };
            }

            private static void GeneratorPassword(){
                string CHAR_POOL = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{};:'\",./?`~";
                int defaultLenght = 14;
                //RandomNumberGenerator rng = new RandomNumberGenerator.;
            }
        }
    }
}