
using BookMyTicket.ViewModel;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace BookMyTicket.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        AdityaEntities4 db = new AdityaEntities4();

        

        //get movieList

        public List<Movie> getMovieList()
        {
            List<Movie> movie = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50301/api/");
                //HTTP GET
                var responseTask = client.GetAsync("MovieWebApi");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Movie>>();
                    readTask.Wait();

                    movie = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    movie = null;        //Enumerable.Empty<StudentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                return movie;
            }
        }

        //get showList

        public List<Show> getShowList()
        {
            List<Show> showlist = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50301/api/");
                //HTTP GET
                var responseTask = client.GetAsync("ShowWebApi");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Show>>();
                    readTask.Wait();

                    showlist = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    showlist = null;        //Enumerable.Empty<StudentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                return showlist;
            }
        }



        //get genreList

        public List<Genre> getGenreList()
        {
            List<Genre> genreList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50301/api/");
                //HTTP GET
                var responseTask = client.GetAsync("GenreWebApi");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Genre>>();
                    readTask.Wait();

                    genreList = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    genreList = null;        //Enumerable.Empty<StudentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                return genreList;
            }
        }


        //get genreList

        public List<Language> getLanguageList()
        {
            List<Language> languageList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50301/api/");
                //HTTP GET
                var responseTask = client.GetAsync("LanguageWepApi"); //LanguageWepApi
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Language>>();
                    readTask.Wait();

                    languageList = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    languageList = null;        //Enumerable.Empty<StudentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                return languageList;
            }
        }



        // access to all
        public ActionResult Test2(int? page) // used  by user
        {
            //    return View(db.Movies.ToList().OrderByDescending(temp => temp.DateRelease).ToPagedList(page ?? 1, 8));

            return View(getMovieList().ToList().OrderByDescending(temp => temp.DateRelease).ToPagedList(page ?? 1, 8));
        }

        //public ActionResult NowShowing(int? page) // used  by user
        //{
        //    var today = DateTime.Now;

        //    var  onemonthbefore = today.AddMonths(-1);

        //    return View(db.Movies.Where(temp=>temp.DateRelease >= onemonthbefore).ToList().OrderByDescending(temp => temp.DateRelease).ToPagedList(page ?? 1, 8));
        //}

        //access to all
        public ActionResult cardButtonPress(int movieid)
        {
            //  var testvariable = db.Shows.Where(temp => temp.Movie.MovieId == movieid).ToList();

            var testvariable = getShowList().Where(temp => temp.Movie.MovieId == movieid).ToList();
            return View(testvariable);

        }


        // access to all
        public ActionResult MovieDetails(int movieid)
        {
            //  var singleMovieInDb = db.Movies.Single(temp => temp.MovieId == movieid);

            var singleMovieInDb = getMovieList().Single(temp => temp.MovieId == movieid);
            return View(singleMovieInDb);
        }




        [Authorize(Roles = "AdminProfile")]
        public string CheckDetails(string param1, string param2)
        {
            var currentdate = DateTime.Now;

            var onemonthback = currentdate.AddMonths(-1);


            var movieindb = (from k in db.Movies.Where(temp => temp.DateRelease >= onemonthback)
                             select new
                             {
                                 MovieId = k.MovieId,
                                 MovieName = k.MovieName,
                                 DateRelease = k.DateRelease,
                                 DateEnd = k.DateEnd,
                                 LanguageId = k.LanguageId,
                                 GenreId = k.GenreId,
                                 Description = k.Description,
                                 CreatedDate = k.CreatedDate,
                                 ModifiedDate = k.ModifiedDate,
                                 CreatedBy = k.CreatedBy,
                                 ModifiedBy = k.ModifiedBy,
                                 MoviePic = k.MoviePic,
                                 MovieWallpaper = k.MovieWallpaper
                             }).ToList();

           

            List<Movie> movielist = new List<Movie>();

            var chk1 = new Movie
            {
                MovieName = "Movie1 " + param1,
                Description = param2 + " Description1"
            };
            var chk2 = new Movie
            {
                MovieName = "Movie2 " + param1,
                Description = param2 + " Description2"
            };
            var chk3 = new Movie
            {
                MovieName = "Movie3 " + param1,
                Description = param2 + " Description3"
            };

            movielist.Add(chk1);
            movielist.Add(chk2);
            movielist.Add(chk3);



            return JsonConvert.SerializeObject(movieindb);
        }



        // access to all
        [HttpGet]
        public JsonResult getMovies() // this is used for autocomplete
        {
          //  return Json(db.Movies.Select(temp => temp.MovieName).ToList(), JsonRequestBehavior.AllowGet);

            return Json(getMovieList(). Select(temp => temp.MovieName).ToList(), JsonRequestBehavior.AllowGet);


        }


        // access to all

        [ValidateAntiForgeryToken]
        public ActionResult getSingleMovie(string search) // this is used for singlemoviesearch
        {
            try
            {
                // var singlemovieindb = db.Movies.Single(temp => temp.MovieName == search);

                //  var var1 = db.Movies.Single(temp => temp.MovieName == search).ToPagedList(1, 1);

             //   var singlemovieindb = db.Movies.Where(temp => temp.MovieName == search).ToList().ToPagedList(1, 1);

                var singlemovieindb =  getMovieList().Where(temp => temp.MovieName == search).ToList().ToPagedList(1, 1);
                

                if (singlemovieindb.Count == 0)
                {
                    throw new Exception();
                }

                if (singlemovieindb != null)
                {
                    // IEnumerable<Movie> singlemovie = new Movie[] { singlemovieindb };

                    // var var1 = singlemovie;
                    // PagedList.IPagedList`1[BookMyTicket.Movie]'

                    //     IPagedList<Movie> = new Movie[] { singlemovieindb };

                    ViewBag.message = "successful";
                    return View("Test2", singlemovieindb);
                }
            }
            catch/*(Exception e)*/
            {
                ViewBag.message = "Movie not found please provide correct name";
              //  return View("Test2", db.Movies.ToList().OrderByDescending(temp => temp.DateRelease).ToPagedList(1, 8));

                return View("Test2", getMovieList().ToList().OrderByDescending(temp => temp.DateRelease).ToPagedList(1, 8));
            }
            return null;

        }


        // access to all
        // GET: Movie
        public ActionResult Index(int? page)
        {
            if (User.IsInRole("AdminProfile"))
            {
                // return View("AdminMovieList", db.Movies.ToList());

                return View("AdminMovieList", getMovieList().ToList());

            }
            else
            {
                var today = DateTime.Now;

                var onemonthbefore = today.AddMonths(-1);

                //   return View("NowShowing", db.Movies.Where(temp => temp.DateRelease >= onemonthbefore).ToList().OrderByDescending(temp => temp.DateRelease).ToPagedList(page ?? 1, 8));

                return View("NowShowing", getMovieList().Where(temp => temp.DateRelease >= onemonthbefore).ToList().OrderByDescending(temp => temp.DateRelease).ToPagedList(page ?? 1, 8));

            }

        }

        [Authorize(Roles = "AdminProfile")]
        public ActionResult MovieForm()
         {

            MovieViewModel obj = new MovieViewModel()
            {
                //genrelist = db.Genres.ToList(),
                genrelist = getGenreList().ToList(),  
                //languagelist = db.Languages.ToList()       
                languagelist = getLanguageList().ToList()
            };
            return View(obj);
        }



        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminProfile")]
        [HttpPost]
        public ActionResult CreateMovie(MovieViewModel obj)
        {

            if (obj.movie.MovieId == 0)
            {
              //  db.Movies.Add(obj.movie);
               // db.SaveChanges();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50301/api/");


                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Movie>("MovieWebApi", obj.movie);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("Index");
                        return RedirectToAction("Index", "Movie");
                    }
                    else
                    {
                        return Content("Some error occured please contact administrator");
                    }
                }
            }
            
            return Content("Please provide correct information and try again later");
        }


        //edit





        [Authorize(Roles = "AdminProfile")]
        public ActionResult EditMovieForm(int id)
        {
            MovieViewModel obj = new MovieViewModel()
            {
                //genrelist = db.Genres.ToList()
                genrelist = getGenreList(),
                //languagelist = db.Languages.ToList()
                languagelist = getLanguageList(),
                //movie = db.Movies.SingleOrDefault(temp => temp.MovieId == id)
                movie = getMovieList().SingleOrDefault(temp=>temp.MovieId == id)
            };
            return View(obj);
        }



        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminProfile")]
        public ActionResult EditMovie(MovieViewModel mvm)
        {
            if (!ModelState.IsValid)
            {
                return Content("Some Error occured");
            }

            else
            {
                // var movieindb = db.Movies.SingleOrDefault(temp => temp.MovieId == mvm.movie.MovieId);
                var movieindb = getMovieList().SingleOrDefault(temp => temp.MovieId == mvm.movie.MovieId);




                if (movieindb == null)
                {
                    return Content("Movie Does not Exist");
                }
                else
                {
                    //movieindb.MovieName = mvm.movie.MovieName;
                    //movieindb.DateRelease = mvm.movie.DateRelease;
                    //movieindb.DateEnd = mvm.movie.DateEnd;
                    //movieindb.LanguageId = mvm.movie.LanguageId;
                    //movieindb.GenreId = mvm.movie.GenreId;
                    //movieindb.Description = mvm.movie.Description;
                    //db.SaveChanges();




                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50301/api/");

                        //HTTP POST
                        var putTask = client.PutAsJsonAsync<Movie>("MovieWebApi", mvm.movie);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                            return RedirectToAction("Index", "Movie");
                        }
                        else
                        {
                            return Content("Error occured movie not updated contact administrator");
                        }

                    }


                }
            }
            
        }



        //delete



        //public ActionResult DeleteMovie(int id)
        //{       

        //        var movieindb = db.Movies.SingleOrDefault(temp => temp.MovieId == id);

        //        if (movieindb == null)
        //        {
        //            return Content("Movie Does not Exist");
        //        }
        //        else
        //        {
        //            db.Movies.Remove(movieindb);
        //            db.SaveChanges();
        //        }


        //    return RedirectToAction("Index", "Movie");
        //}

        [Authorize(Roles = "AdminProfile")]
        [HttpGet]
        public JsonResult DeleteMovie(int id)
        {

           // var movieindb = db.Movies.SingleOrDefault(temp => temp.MovieId == id);

            var movieindb = getMovieList().SingleOrDefault(temp => temp.MovieId == id);

            if (movieindb == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);   //Content("Movie Does not Exist");
            }
            else
            {
                //db.Movies.Remove(movieindb);
                //db.SaveChanges();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50301/api/");

                    //HTTP DELETE
                    var deleteTask = client.DeleteAsync("MovieWebApi/"+movieindb.MovieId);
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            
        }

    }
}