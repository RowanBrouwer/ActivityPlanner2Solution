using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Shared.ExtensionMethods
{
    public static class StringExtensions
    {
        public static DateTime? StringToNullableDateTime(this string str)
            => DateTime.TryParse(str, out var date) ? date : null;
    }
}
