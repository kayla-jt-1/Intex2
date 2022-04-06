using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Intex2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create the page links
        private IUrlHelperFactory uhf;
        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; } //public string PageClass

        //bootstrap things
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public string PageClassLabel { get; set; }
        public string PageClassLinks { get; set; }

        //Override the template
        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");

            int startPage;
            int endPage;

            // If there is more than one page link to display
            if (PageModel.TotalPages > 1)
            {
                // If the total number of pages is less than the allowed number of page links
                // Then the start page is 1 and the end page is equal to the total # of pages
                if (PageModel.TotalPages <= PageModel.LinksPerPage)
                {
                    startPage = 1;
                    endPage = PageModel.TotalPages;
                }

                // assuming we have more pagelinks than can fit on the page, 
                else
                {
                    // if the current page, plus the # of allowed links 
                    if (PageModel.CurrentPage + PageModel.LinksPerPage - 1 > PageModel.TotalPages)
                    {
                        startPage = PageModel.CurrentPage - ((PageModel.CurrentPage + PageModel.LinksPerPage - 1)
                            - PageModel.TotalPages);
                        endPage = (PageModel.CurrentPage + PageModel.LinksPerPage - 1) -
                            ((PageModel.CurrentPage + PageModel.LinksPerPage - 1) - PageModel.TotalPages);
                    }
                    else
                    {
                        if (PageModel.LinksPerPage != 2)
                        {
                            startPage = PageModel.CurrentPage - (PageModel.LinksPerPage / 2);
                            if (startPage < 1)
                            {
                                startPage = 1;
                            }
                            endPage = startPage + PageModel.LinksPerPage - 1;
                        }
                        else
                        {
                            startPage = PageModel.CurrentPage;
                            endPage = PageModel.CurrentPage + PageModel.LinksPerPage - 1;
                        }

                    }
                }
                TagBuilder labelDiv;
                labelDiv = new TagBuilder("div");
                labelDiv.AddCssClass(PageClassLabel);
                labelDiv.InnerHtml.Append($"Showing {PageModel.CurrentPage} of { PageModel.TotalPages}");
                final.InnerHtml.AppendHtml(labelDiv);

                TagBuilder kms = new TagBuilder("div");
                string url = uh.Action(PageAction, new { pageNum = 1 });
                kms.InnerHtml.AppendHtml(GeneratePageLinks("First", 1, url));
                for (int i = startPage; i <= endPage; i++)
                {
                    url = uh.Action(PageAction, new { pageNum = i });
                    kms.InnerHtml.AppendHtml(GeneratePageLinks(i.ToString(), i, url));          
                }
                tho.Content.AppendHtml(final.InnerHtml);

                url = uh.Action(PageAction, new { pageNum = PageModel.TotalPages });
                kms.InnerHtml.AppendHtml(GeneratePageLinks("Last", PageModel.TotalPages, url));
                kms.AddCssClass(PageClassLinks);
                final.InnerHtml.AppendHtml(kms);
                tho.Content.AppendHtml(final.InnerHtml);
            }
        }
        private TagBuilder GeneratePageLinks(string iterator, int pageNumber, string url)
        {
            TagBuilder tb = new TagBuilder("a");
            tb.Attributes["href"] = url;
            tb.AddCssClass(PageClass);
            if (iterator != "First" && iterator != "Last")
            {
                tb.AddCssClass(Convert.ToInt32(iterator) == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            }
            else
            {
                tb.AddCssClass(pageNumber == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            }
            tb.InnerHtml.Append(iterator.ToString());
            return tb;
        }

            //    for (int i = 1; i <= PageModel.TotalPages; i++)
            //    {
            //        TagBuilder tb = new TagBuilder("a");

            //        tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

            //        //bootstrap
            //        if (PageClassesEnabled)
            //        {
            //            tb.AddCssClass(PageClass);
            //            tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            //        }

            //        tb.InnerHtml.Append(i.ToString());

            //        final.InnerHtml.AppendHtml(tb);

            //    }

            //    tho.Content.AppendHtml(final.InnerHtml);

        
    }
}
