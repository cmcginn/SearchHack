using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SearchHack.Web.Models;
using SearchHack.Web.Services;
using MongoRepository;

namespace SearchHack.Web.Api
{
    public class SearchController : ApiController
    {
        private UserService userService = new UserService();
        MongoRepository<SearchSettingsModel> repo = new MongoRepository<SearchSettingsModel>();
       // 
        // GET api/<controller>
        public SearchSettingsModel Get()
        {
            SearchSettingsModel result;
            //SearchSettings settings = null;
            var user = userService.GetApplicationUser(User);
            var uid = Guid.Parse(user.Id);
            result = repo.SingleOrDefault(x => x.UserId == uid);
            if (result == null)
                result = new SearchSettingsModel {UserId = uid};
            

            return result;
            
        }

      
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public SearchSettingsModel Post(SearchSettingsModel model)
        {
            SearchSettingsModel result;
            var user = userService.GetApplicationUser(User);
            var uid = Guid.Parse(user.Id);
            result = repo.SingleOrDefault(x => x.UserId == uid);
            if (result == null)
            {
                model.Searches.Single().CreatedOnUtc = System.DateTime.UtcNow;
                result = repo.Add(model);

            }
            else
            {
                result.Searches = model.Searches;
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