using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SearchHack.Web.Models;
using MongoRepository;
using SearchHack.Web.Services;

namespace SearchHack.Web.Api
{
    public class SearchUrlsController : ApiController
    {
        MongoRepository<SearchUrlsModel> repo = new MongoRepository<SearchUrlsModel>();
        private UserService userService = new UserService();
        // GET api/<controller>
        public SearchUrlsModel Get()
        {
            SearchUrlsModel result = null;
            var user = userService.GetApplicationUser(User);
            var uid = Guid.Parse(user.Id);
            result = repo.SingleOrDefault(x => x.UserId == uid);
            if (result == null)
                result = new SearchUrlsModel {UserId = uid};
            return result;

            
        }

        // GET api/<controller>/5
        //public SearchUrlsModel Get(string id)
        //{
        //    //SearchUrlsModel result = null;
        //    //var user = userService.GetApplicationUser(User);
        //    //var uid = Guid.Parse(user.Id);
        //    //result = repo.SingleOrDefault(x => x.UserId == uid && x.SearchType == id);
        //    //if (result == null)
        //    //    result = new SearchUrlsModel { UserId = uid, SearchType = id };
        //    //return result;
        //}

        // POST api/<controller>
        public SearchUrlsModel Post(SearchUrlsModel model)
        {
            SearchUrlsModel result = null;
            var user = userService.GetApplicationUser(User);
            var uid = Guid.Parse(user.Id);
            result = repo.SingleOrDefault(x => x.UserId == uid);
            if (result == null)
            {
                result = model;
                repo.Add(result);
            }
            else
            {
                result.Urls = model.Urls;
                repo.Update(result);
            }
            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}