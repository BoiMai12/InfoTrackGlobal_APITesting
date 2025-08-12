using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Support.Constrains;

public abstract class ContextDataKeys
{
    public const string HttpBaseRequest = "__HTTP_BASE_REQUEST";
    public const string HttpCurrentRequest = "__HTTP_CURRENT_REQUEST";
    public const string HttpCurrentResponse = "__HTTP_CURRENT_RESPONSE";
    public const string VariablePrefix = "__VARIABLE:";
}

