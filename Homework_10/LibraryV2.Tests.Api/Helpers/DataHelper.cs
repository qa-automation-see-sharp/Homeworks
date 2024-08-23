using Bogus;
using LibraryV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV2.Tests.Api.Helpers
{
    public static class DataHelper
    {
        public static Book CreateBook()
        {
            var faker = new Faker();

            return new Book
            {
                Title = $"Pragmatic Programmer{faker.Random.AlphaNumeric(4)}",
                Author = "Andrew Hunt",
                YearOfRelease = 1999
            };
        }
    }
}
