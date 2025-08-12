using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.StepDefinitions.GlobalSteps.Http.Model;

public class HttpQueryParameterStepModel
{
    public HttpQueryParameterStepModel(string name, string value)
    {
        Name = name;
        Value = value;
    }
    public string Name { get; set; }
    public string Value { get; set; }

}
