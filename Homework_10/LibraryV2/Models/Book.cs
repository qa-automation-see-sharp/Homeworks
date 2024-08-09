using Newtonsoft.Json;

namespace LibraryV2.Models;

public sealed class Book
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int YearOfRelease { get; set; }
}