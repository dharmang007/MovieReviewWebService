using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace MovieReviewWebService.Controllers
{
    public class LoginController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext("Server=tcp:dharmangsolanki.database.windows.net,1433;Initial Catalog=MovieReview;Persist Security Info=False;User ID=dharmang;Password=JamesBond007;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

      
        [Route("login/get")]
        [HttpGet
            ]
        public bool login(string email,string pword )
        {
            try
            {
                var q = (from i in dc.UserInfos
                         where i.email == email
                         where i.Password == pword
                         select i).SingleOrDefault();
                if (q == null)
                {
                    return false;
                }
                else
                {
                    //return Request.CreateResponse(System.Net.HttpStatusCode.OK, user);
                    return true;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Route("Home")]
        [HttpGet]
        public IEnumerable<UserInfo> home()
        {
            return dc.UserInfos;
        }

    }

}

