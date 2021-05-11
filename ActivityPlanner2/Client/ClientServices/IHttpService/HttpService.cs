using ActivityPlanner2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.ClientServices
{ 
    public class HttpService : IHttpService
    {
        readonly HttpClient Http;
        public HttpService(HttpClient Http)
        {
            this.Http = Http;
        }

        //Task<List<Person>> GetListOfPeople()
        //{
            
        //}
    }
}
