using System;

namespace SamplesApp.Converters
{
    public class Converter
    {
        public T ConvertTo<T>(object objectToConvert)
        {
            var defaultValue = default(T);
            if (objectToConvert == null || objectToConvert == DBNull.Value)
                return defaultValue;

            try
            {
                return (T)objectToConvert;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}