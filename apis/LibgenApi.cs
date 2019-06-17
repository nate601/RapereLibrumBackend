using System;
using System.Collections.Generic;
using ScrapySharp;
using ScrapySharp.Network;
using ScrapySharp.Extensions;
using HtmlAgilityPack;
using System.Linq;
using ScrapySharp.Html;

namespace backend.Apis
{
    public static class LibgenApi
    {
        //  http://libgen.io/search.php?req=Mistborn
        public static RowInfo[] GetLibgen(string bookTitle)
        {
            try
            {
                ScrapingBrowser browser = new ScrapingBrowser();
                var page = browser.NavigateToPage(new Uri($"http://libgen.io/search.php?req={bookTitle}"));
                var rowHtml = page.Html.CssSelect("body > table.c > tbody > tr:nth-child(n+2)");
                var rowHtmlEpub = rowHtml.Where((node) => node.ToString().Contains("epub"));
                var rowInfos = rowHtmlEpub.Select((node) =>
                {
                    return new RowInfo(node);
                });
                return rowInfos.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public class RowInfo
        {
            string idNumber;
            string authorName;
            string bookTitle;
            string[] mirrorLinks;
            string fileSize;

            public RowInfo(HtmlNode rowHtml)
            {
                this.idNumber = rowHtml.ChildNodes[0].InnerText;
                this.authorName = rowHtml.ChildNodes[1].FirstChild.InnerText;
                this.bookTitle = rowHtml.CssSelect(".title").First().InnerText;
                this.fileSize = rowHtml.CssSelect("td:nth-last-child(4)").First().InnerText;
                this.mirrorLinks = rowHtml.CssSelect("tbody > tr").CssSelect("a[href]").Select((node) => { return node.GetAttributeValue("href"); }).ToArray();
            }
        }

    }
}
