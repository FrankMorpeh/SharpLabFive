namespace SharpLabFive.Converters.TimeConverters
{
    public static class TimeConverter
    {
        public static int ToMillisecondsFromSeconds(double? seconds)
        {
            return (int)seconds * 1000;
        }
    }
}