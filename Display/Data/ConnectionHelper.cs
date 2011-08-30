using System;
using System.Collections.Generic;
using System.Configuration;
using Display.Models;

namespace Display.Data
{
    public class ConnectionHelper<T>
    {
        readonly IDictionary<Type, string> _types = new Dictionary<Type,string>();

        public ConnectionHelper()
        {
            _types.Add(typeof(Post), "Posts");
            _types.Add(typeof(User), "Users");
            _types.Add(typeof(EmailModel), "Email");
        } 

        public string ConnectionString
        {
            get
            {
                var environment = ConfigurationManager.AppSettings["environment"];
                if(environment.Equals("dev"))
                {
                    return @"mongodb://localhost/" + _types[typeof(T)];
                }
                //return ConfigurationManager.AppSettings["MONGOHQ_URL"];
                return "mongodb://appharbor:2f9c73f4109d2b6e1b1f90110f51d711@staff.mongohq.com:10007/1423cfad-0b48-4f7d-8aed-c8fb8024b2a9"; 
            }
        }
    }
}