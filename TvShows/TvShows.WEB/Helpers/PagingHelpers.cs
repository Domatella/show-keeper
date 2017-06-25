using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TvShows.WEB.Models;

namespace TvShows.WEB.Helpers
{
    public static class PagingHelpers
    {
        private const int MAX_BUTTONS_QUANTITY = 5;
        private const int CENTER_BUTTON_POSITION = 3;

        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(1));
            tag.InnerHtml = "1";
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());

            tag = new TagBuilder("a");
            if (pageInfo.PageNumber != 1)
            {
                tag.MergeAttribute("href", pageUrl(pageInfo.PageNumber - 1));
            }
            else
            {
                tag.MergeAttribute("href", pageUrl(1));
            }

            tag.InnerHtml = "<";
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());


            int i;
            if ((pageInfo.PageNumber > CENTER_BUTTON_POSITION) &&
                (pageInfo.TotalPages > MAX_BUTTONS_QUANTITY))
            {
                i = pageInfo.PageNumber - (CENTER_BUTTON_POSITION - 1);
                if (pageInfo.PageNumber + (CENTER_BUTTON_POSITION - 1) > pageInfo.TotalPages)
                {
                    i -= pageInfo.PageNumber + (CENTER_BUTTON_POSITION - 1) - pageInfo.TotalPages;
                }
            }
            else
            {
                i = 1;
            }

            int lastButton;
            if ((pageInfo.PageNumber + (CENTER_BUTTON_POSITION - 1) < pageInfo.TotalPages) &&
                (pageInfo.TotalPages > MAX_BUTTONS_QUANTITY))
            {
                lastButton = pageInfo.PageNumber + (CENTER_BUTTON_POSITION - 1);
                if (pageInfo.PageNumber < CENTER_BUTTON_POSITION)
                {
                    lastButton += CENTER_BUTTON_POSITION - pageInfo.PageNumber;
                }
            }
            else
            {
                lastButton = pageInfo.TotalPages;
            }

            for (; i <= lastButton; i++)
            {
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }

                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            tag = new TagBuilder("a");
            if (pageInfo.PageNumber != pageInfo.TotalPages)
            {
                tag.MergeAttribute("href", pageUrl(pageInfo.PageNumber + 1));
            }
            else
            {
                tag.MergeAttribute("href", pageUrl(pageInfo.TotalPages));
            }

            tag.InnerHtml = ">";
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());

            tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(pageInfo.TotalPages));
            tag.InnerHtml = pageInfo.TotalPages.ToString();
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}