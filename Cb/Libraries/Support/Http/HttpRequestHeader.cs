
namespace Libraries.Support.Http;

    public class HttpRequestHeader
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public HttpRequestHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

