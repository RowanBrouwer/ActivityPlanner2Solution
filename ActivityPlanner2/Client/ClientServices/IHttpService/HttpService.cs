using ActivityPlanner2.Client.Pages;
using ActivityPlanner2.Shared;
using Microsoft.Extensions.Logging;
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
        ILogger<HttpService> logger;
        public HttpService(HttpClient Http, ILogger<HttpService> logger)
        {
            this.Http = Http;
            this.logger = logger;
        }

        //Task<List<Person>> GetListOfPeople()
        //{
            
        //}
    }
}
