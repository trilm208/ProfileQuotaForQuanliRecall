namespace System
{
    public class ConvertSafe
    {
        public static Decimal ToDecimal(object value)
        {
            if (value == null)
                return 0;

            if (value == DBNull.Value)
                return 0;

            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
            }

            return 0;
        }

        public static int ToInt32(object value)
        {
            if (value == null)
                return 0;

            if (value == DBNull.Value)
                return 0;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
            }

            return 0;
        }

        public static bool ToBoolean(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
            }

            return false;
        }

        public static string ToString(object value)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch
            {
            }

            return string.Empty;
        }

        public static DateTime? ToDateTime(object value)
        {
            if (value == null)
                return null;

            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
            }

            return null;
        }

        public static TimeSpan ToTimeSpan(object value)
        {
            try
            {
                return TimeSpan.Parse(value.ToString());
            }
            catch
            {
            }

            return new TimeSpan();
        }
    }
}