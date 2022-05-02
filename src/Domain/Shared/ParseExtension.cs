
namespace Domain.Shared
{
    public static class ParseExtension
    {
        public static int ParseInt(string value)
        {
            if (!String.IsNullOrEmpty(value))
                return int.Parse(value);
            throw new Exception($"Cannot Parse {value}");
        }

        public static TimeSpan ParseHour(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DateTime dt = ConvertTimeFromUtc(new DateTime(1000, 1, 1,
                    Convert.ToInt32(value.Substring(0, 2)),
                    Convert.ToInt32(value.Substring(2, 2)),
                    Convert.ToInt32(value.Substring(4, 2))));
                return new TimeOnly(dt.Hour, dt.Minute, dt.Second).ToTimeSpan();
            }
            throw new Exception($"Cannot Parse {value}");
        }

        public static string ParseDate(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return new DateOnly(Convert.ToInt32(value.Substring(0, 4)),
                                   Convert.ToInt32(value.Substring(4, 2)),
                                   Convert.ToInt32(value.Substring(6, 2))).ToString();
            }
            throw new Exception($"Cannot Parse {value}");
        }

        public static decimal ParseToDecimal(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var result = decimal.Parse(value);
                return result;
            }
            throw new Exception($"Cannot Parse {value}");
        }

        public static DateTime ConvertTimeFromUtc(DateTime dt) => TimeZoneInfo.ConvertTimeFromUtc(
            dt, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
