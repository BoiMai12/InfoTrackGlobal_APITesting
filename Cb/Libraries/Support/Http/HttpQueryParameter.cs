
namespace Libraries.Support.Http;

    public class HttpQueryParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public HttpQueryParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

