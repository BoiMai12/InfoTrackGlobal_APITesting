using Libraries.Support.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.StepDefinitions.GlobalSteps.Http.Model;

public class HttpRequestHeaderStepModel
{
    public List<HttpQueryParameter>? QueryParameters { get; set; }
    public List<HttpRequestHeader>? QueryHeader { get; set; }
    public string? Content { get; set; }
    public string? ContentType { get; set; }
}
