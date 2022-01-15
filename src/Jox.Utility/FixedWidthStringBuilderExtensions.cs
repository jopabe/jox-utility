using System.Globalization;

namespace Jox.Utility
{
    public static class FixedWidthStringBuilderExtensions
    {

        // text fields are left-aligned by default
        public static void AppendField(this StringBuilder sb, int length, string value = null, bool alignRight = false)
        {
            if (value is null)
            {
                value = string.Empty;
            }
            if (alignRight)
            {
                value = value.PadLeft(length);
            }
            sb.Append(value.PadRight(length).Substring(0, length));
        }

        // decimal fields are comma-based
        public static void AppendDecimalField(this StringBuilder sb, int bc, int ac, Decimal value)
        {
            var max = Convert.ToInt32(Math.Pow(10, bc));
            if (value >= max)
            {
                throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.InvariantCulture, "Amounts larger than {0} are not supported", max));
            }
            var format = new StringBuilder(bc + ac + 5);
            format.Append("{0:");
            format.Append('0', bc);
            format.Append('.');
            format.Append('0', ac);
            format.Append('}');
            sb.AppendFormat(CultureInfo.GetCultureInfo(0x813), format.ToString(), value);
        }

        // dates are formatted as YYYYMMDD
        public static void AppendDateField(this StringBuilder sb, DateTime value)
        {
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:yyyyMMdd}", value);
        }
    }
}
