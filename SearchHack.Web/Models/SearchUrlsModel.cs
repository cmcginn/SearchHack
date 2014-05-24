using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MongoRepository;

namespace SearchHack.Web.Models
{
    public class SearchUrl
    {
        [DataMember]
        public string SearchType { get; set; }

        [DataMember]
        public string Url { get; set; }
    }
    public class SearchUrlsModel:Entity
    {
        [DataMember]
        public Guid UserId { get; set; }

        private List<SearchUrl> _urls;
        [DataMember]
        public List<SearchUrl> Urls
        {
            get { return _urls ?? (_urls = new List<SearchUrl>()); }
            set { _urls = value; }
        }

       
    }
}