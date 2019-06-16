using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
namespace backend.Apis
{
    public static class GoogleApi
    {
        public static GoogleBookInfo GetBookInfo(int Isbn)
        {
            var client = new RestClient(@"https://www.googleapis.com/books/v1");
            var searchRequest = new RestRequest("volumes?=searchTerm");
            searchRequest.AddUrlSegment("searchTerms", $"isbn:{Isbn.ToString()}");
            IRestResponse response = client.Execute(searchRequest, Method.GET);
            JObject content = JObject.Parse(response.Content);
            JToken authorNameToken = content.SelectToken("$.items[0].volumeInfo.title");


            return null; //TODO:Remove
        }
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