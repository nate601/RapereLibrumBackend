namespace backend.Apis
{
    static class GoogleApi
    {
        public class GoogleBookInfo
        {
            public string AuthorName;
            public string BookTitle;
            public string Isbn;

            public GoogleBookInfo(string _AuthorName, string _BookTitle, string _Isbn)
            {
                this.AuthorName = _AuthorName;
                this.BookTitle = _BookTitle;
                this.Isbn = _Isbn;
            }
        }
    }
}