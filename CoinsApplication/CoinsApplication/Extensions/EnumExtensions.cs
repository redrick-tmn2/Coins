using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsApplication.Extensions
{
    public static class EnumExtensions
    {
        public static T Next<T>(T value) 
            where T : struct, IConvertible
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var values = (T[])Enum.GetValues(type);

            var nextIndex = Array.IndexOf<T>(values, value) + 1;


            return (values.Length == nextIndex) ? values[0] : values[nextIndex];
        }
    }
}
