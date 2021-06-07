using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Common;

namespace WebServer.Server.Http
{
    public class HttpHeader
    {
        public HttpHeader(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; }  //init

        public string Value { get; set; }

        public override string ToString()
            => $"{this.Name}: {this.Value}";
    }

    // public record HttpHeader(string Name, string Value)
    
}
