using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieReviewWebService.Models
{
    public class Movies
    {
        public string name { get; set; }
        public int release_year { get; set; }
        public string genre {get;set;}
        public string director { get; set; }
        public string actors { get; set; }
        public string music { get; set; }
        public int rating { get; set; }

    }
}