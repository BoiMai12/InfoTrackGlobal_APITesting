using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Support.TestData;

public abstract class TestDataHelper
{
    public static string ReadFile(string path)
    {
        return File.ReadAllText(GetFilePath(path));
    }
    public static string GetFilePath(string path)
    {
        return Path.Combine(".", "TestData", path);
    }
}

