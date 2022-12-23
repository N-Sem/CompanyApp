using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyApp.UI.Models.CustomAttributes
{
    public class DataRangeAttribute
    {
        public class DateRangeAttribute : RangeAttribute
        {
            public DateRangeAttribute(int minAge = int.MinValue, int maxAge = int.MaxValue)
                : base(typeof(DateTime),
                      DateTime.Now.AddYears(-maxAge).ToShortDateString(),
                      DateTime.Now.AddYears(-minAge).ToShortDateString())
            { }
        }
    }
}
