using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Utils
{
    public static class StringValidator
    {
        public static bool isValid(string param)
        {
            if (string.IsNullOrEmpty(param))
                return false;
            else if (string.IsNullOrWhiteSpace(param))
                return false;
            else if (param == string.Empty)
                return false;
            return true;
        }
    }
}
