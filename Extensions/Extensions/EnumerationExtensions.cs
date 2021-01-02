using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class EnumerationExtensions
    {
        public static bool IsSet<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        public static T Set<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }

            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format("Could not append value from enumerated type '{0}'.", typeof(T).Name), ex);
            }

        }

        public static T Clear<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format("Could not remove value from enumerated type '{0}'.", typeof(T).Name), ex);
            }
        }

        public static T Parse<T>(this System.Enum type, string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
