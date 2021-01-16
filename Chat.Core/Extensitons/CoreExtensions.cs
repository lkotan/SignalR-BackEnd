namespace Chat.Core.Extensions
{
    public static class CoreExtensions
    {
        public static int ToInt(this object value)
        {
            value ??= "0";
            int.TryParse(value.ToString(), out var result);
            return result;
        }
        public static double ToDouble(this object value)
        {
            value ??= "0";
            double.TryParse(value.ToString().Replace(",", "").Replace(".", ","), out var result);
            return result;
        }

    }
}
