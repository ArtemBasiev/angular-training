using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace bizapps_test.MVC.Helpers
{
    public static class CommentValidator
    {
            public static MvcHtmlString CommentValidatorFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
            {
                TagBuilder containerDivBuilder = new TagBuilder("div");
                containerDivBuilder.AddCssClass("comment-validator-container");

                TagBuilder warningIcon = new TagBuilder("i");
                warningIcon.AddCssClass("glyphicon");
                warningIcon.AddCssClass("glyphicon-warning-sign");
                warningIcon.AddCssClass("valicon");

            //containerDivBuilder.InnerHtml += warningIcon.ToString(TagRenderMode.Normal);
                //MvcHtmlString newMes = MvcHtmlString.Create(helper.ValidationMessageFor(expression).ToString());

                containerDivBuilder.InnerHtml += helper.ValidationMessageFor(expression).ToString().Replace("</span>","")+ warningIcon.ToString(TagRenderMode.Normal) + "</span>";

                return MvcHtmlString.Create(containerDivBuilder.ToString(TagRenderMode.Normal));
            }
       
    }
}