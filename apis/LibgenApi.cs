using HtmlAgilityPack;
using System.Linq;

namespace backend.Apis
{
    public static class LibgenApi
    {
        //  http://libgen.io/search.php?req=Mistborn
        public static RowInfo[] GetLibgen(string bookTitle)
        {
            //try
            var html = $@"http://libgen.io/search.php?req={bookTitle}";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var rowHtml = htmlDoc.DocumentNode.SelectNodes("/html/body/table[3]/tr[position()>1]");
            if (rowHtml == null || rowHtml.Count == 0)
            {
                return null;
            }
            var rowHtmlEpub = rowHtml.Where((node) => node.InnerHtml.Contains("epub"));
            var rowInfos = rowHtmlEpub.Select((node, index) =>
            {
                return new RowInfo(node, index);
            });
            return rowInfos.ToArray();

        }
        public class RowInfo
        {
            public string idNumber;
            public string authorName;
            public string bookTitle;
            public string[] mirrorLinks;
            public string fileSize;
            public int bestMatchNumber;
            public RowInfo(HtmlNode rowHtml, int bestMatchNumber)
            {
                this.idNumber = rowHtml.SelectSingleNode("./td[1]").InnerText;
                this.authorName = rowHtml.SelectSingleNode("./td[2]/a").InnerText;
                this.fileSize = rowHtml.SelectSingleNode("./td[8]").InnerText;
                this.bestMatchNumber = bestMatchNumber;
                var bookTitleTotal = rowHtml.SelectNodes("td[3]/a");
                foreach (var bt in bookTitleTotal)
                {
                    bookTitle = bookTitle + " " + bt.GetDirectInnerText();
                }
                this.bookTitle = bookTitle.TrimStart(' ');


                this.mirrorLinks = rowHtml.SelectNodes("td[10]/table//a").Select(node =>
                {
                    return node.GetAttributeValue("href", "");
                }).ToArray();
            }
        }

    }
}
