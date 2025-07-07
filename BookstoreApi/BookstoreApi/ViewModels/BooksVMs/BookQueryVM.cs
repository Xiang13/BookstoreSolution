namespace BookstoreApi.ViewModels.BooksVMs
{
    public class BookQueryVM
    {
        // 可為 null 表示全部
        public int? CategoryId { get; set; }
        // 書名關鍵字
        public string? Keyword { get; set; }
    }
}
