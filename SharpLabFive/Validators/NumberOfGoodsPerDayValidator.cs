using SharpLabFour.Notification;
using System;

namespace SharpLabFive.Validators
{
    public static class NumberOfGoodsPerDayValidator
    {
        public static INotification CheckNumberOfGoodsPerDay(string numberOfGoodsPerDayStr)
        {
            var conversionResult = ConvertNumberOfGoodsPerDay(numberOfGoodsPerDayStr);
            if (conversionResult.Item1 == false)
                return new IncorrectFormatOfNumberOfGoodsPerDay();
            else
            {
                if (conversionResult.Item2 < 1)
                    return new IncorrectNumberOfGoodsPerDay();
                else
                    return new None();
            }
        }
        private static Tuple<bool, int> ConvertNumberOfGoodsPerDay(string numberOfGoodsPerDayStr)
        {
            int numberOfGoodsPerDay = -1;
            bool parsingIsSuccessful = int.TryParse(numberOfGoodsPerDayStr, out numberOfGoodsPerDay);
            return new Tuple<bool, int>(parsingIsSuccessful, numberOfGoodsPerDay);
        }
    }
}