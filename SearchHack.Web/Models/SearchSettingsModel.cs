using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;

namespace SearchHack.Web.Models
{
    public class SearchTerm
    {
        [DataMember]
        public string Term { get; set; }
        [DataMember]
        public int Score { get; set; }
    }
    public class Search
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime CreatedOnUtc { get; set; }
        [DataMember]
        public List<SearchTerm> SearchTerms
        {
            get { return _searchTerms ?? (_searchTerms = new List<SearchTerm>()); }
            set { _searchTerms = value; }
        }

        private List<SearchTerm> _searchTerms;
    }

    public class SearchSettingsModel:Entity
    {
        [DataMember]
        public Guid UserId { get; set; }
        
        private List<Search> _searches;
        [DataMember]
        public List<Search> Searches
        {
            get { return _searches ?? (_searches = new List<Search>()); }
            set { _searches = value; }
        }
    }
}