using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.StepDefinitions.GlobalSteps.Http.Model;

public class HttpResponseContextData
{
    public int StatusCode { get; set; }
    public string? Body { get; set; }
    //public Dictionary<string, string>? Headers { get; set; }

}

