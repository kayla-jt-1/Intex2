using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumRecords { get; set; }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }

        //How many pages we need 
        public int TotalPages => (int)Math.Ceiling((double)TotalNumRecords / RecordsPerPage);

        //How many pages can there be on one page
        public int LinksPerPage { get; set; }

        // Dynamicaly create links
        public string UrlParams { get; set; }
    }
}
