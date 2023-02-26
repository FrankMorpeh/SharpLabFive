using SharpLabFive.Models.Workshops;
using System;

namespace SharpLabFive.Converters.WorkshopConverters
{
    public static class WorkshopConverter
    {
        public static Workshop ToWorkshop(string numberOfGoodsPerDay)
        {
            return new Workshop(Convert.ToInt32(numberOfGoodsPerDay));
        }
    }
}