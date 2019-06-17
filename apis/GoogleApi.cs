using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
namespace backend.Apis
{
    public static class GoogleApi
    {
        public static GoogleBookInfo GetBookInfo(string Isbn)
        {
            try
            {

                var client = new RestClient(@"https://www.googleapis.com");
                var searchRequest = new RestRequest("/books/v1/volumes?q=isbn:" + Isbn);
                IRestResponse response = client.Execute(searchRequest, Method.GET);
                if (!response.IsSuccessful || response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                JObject content = JObject.Parse(response.Content);
                JToken authorNameToken = content.SelectToken("$.items[0].volumeInfo.authors[0]");
                JToken bookTitle = content.SelectToken("$.items[0].volumeInfo.title");
                JToken urlToThumbnail = content.SelectToken("$.items[0].volumeInfo.imageLinks.thumbnail");
                JToken averageRating = content.SelectToken("$.items[0].volumeInfo.averageRating");
                JToken numberOfRatings = content.SelectToken("$.items[0].volumeInfo.ratingsCount");
                return new GoogleBookInfo(authorNameToken.Value<String>(), bookTitle.Value<String>(), Isbn.ToString(), urlToThumbnail.Value<String>(), averageRating.Value<String>(), numberOfRatings.Value<String>());

            }
            catch (Exception)
            {
                return null;
            }
        }
        public class GoogleBookInfo
        {
            public string AuthorName;
            public string BookTitle;
            public string Isbn;
            public string UrlToThumbnail;
            public string AverageRating;
            public string NumberOfRatings;

            public GoogleBookInfo(string _AuthorName, string _BookTitle, string _Isbn, string _UrlToThumbnail, string _averageRating, string _numberOfRatings)
            {
                this.AuthorName = _AuthorName;
                this.BookTitle = _BookTitle;
                this.Isbn = _Isbn;
                this.UrlToThumbnail = _UrlToThumbnail;
                this.AverageRating = _averageRating;
                this.NumberOfRatings = _numberOfRatings;
            }
        }
    }
}