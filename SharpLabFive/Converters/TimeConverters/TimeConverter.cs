namespace SharpLabFive.Converters.TimeConverters
{
    public static class TimeConverter
    {
        public static int ToMillisecondsFromSeconds(int seconds)
        {
            return seconds * 1000;
        }
    }
}