using MovieReviewWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieReviewWebService.Controllers
{
   
    public class MoviesController : ApiController
    {
        Movie m = new Movie();
        DataClasses1DataContext dc = new DataClasses1DataContext("Server=tcp:dharmangsolanki.database.windows.net,1433;Initial Catalog=MovieReview;Persist Security Info=False;User ID=dharmang;Password=JamesBond007;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        
        [HttpPost]
        public void AddMovie(Movie movie)
        {
            m.actors = movie.actors;
            m.director = movie.director;
            m.genre = movie.genre;
            m.music = movie.music;
            m.name = movie.name;
            m.rating=movie.rating;
            m.release_year = movie.release_year;
            dc.Movies.InsertOnSubmit(m);
            dc.SubmitChanges();
        }

    }
}
