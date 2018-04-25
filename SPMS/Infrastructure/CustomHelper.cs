using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPMS.Infrastructure
{
    public static class CustomHelper
    {
        public static MvcHtmlString HelperImage(this HtmlHelper helper, string ImageUrl, string altname)
        {
            TagBuilder tag1 = new TagBuilder("ahref");
            {
                TagBuilder tag = new TagBuilder("img");
                tag.MergeAttribute("src", ImageUrl);
                tag.MergeAttribute("alt", altname);
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
            }
        }
        public static MvcHtmlString HelperConcat(this HtmlHelper helper,string FirstName,string MiddleName,string LastName)
        {
            if (MiddleName == null)
                return MvcHtmlString.Create( FirstName + " " + LastName);
            else
                return MvcHtmlString.Create( FirstName + " " +MiddleName+" "+ LastName);

        }


    }
}