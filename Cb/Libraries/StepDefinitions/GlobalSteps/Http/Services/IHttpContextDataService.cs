using Libraries.StepDefinitions.GlobalSteps.Http.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.StepDefinitions.GlobalSteps.Http.Services;

public interface IHttpContextDataService
{
    public void AddHttpBaseData(HttpBaseRequestContextData data);
    public void AddCurrentHttpData(HttpRequestContextData data);
    public void AddCurrentHttpResponse(RestResponse restResponse);
    public HttpBaseRequestContextData GetHttpBaseData();
    public HttpRequestContextData GetCurrentHttpData();
    public RestResponse GetCurrentHttpResponseData();

}

