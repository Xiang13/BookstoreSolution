namespace BookstoreApi.ViewModels.HomeVMs
{
    public class BookSearchVM
    {
        public int? CategoryId { get; set; }   // 可為 null 表示全部
        public string? Keyword { get; set; }   // 書名關鍵字
    }
}
