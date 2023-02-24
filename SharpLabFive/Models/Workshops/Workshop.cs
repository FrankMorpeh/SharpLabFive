using System.Threading;

namespace SharpLabFive.Models.Workshops
{
    public class Workshop
    {
        private int itsNumberOfGoodsPerDay;
        private int itsNumberOfWorkers;
        private int itsNumberOfResources;

        public Workshop(int numberOfGoodsPerDay)
        {
            itsNumberOfGoodsPerDay = numberOfGoodsPerDay;
            if (itsNumberOfGoodsPerDay < 2)
                itsNumberOfWorkers = 1;
            else
                itsNumberOfWorkers = itsNumberOfGoodsPerDay / 2;
            itsNumberOfResources = itsNumberOfGoodsPerDay * 5;
        }

        public int NumberOfGoodsPerDay { get { return itsNumberOfGoodsPerDay; } set { itsNumberOfGoodsPerDay = value; } }
        public int NumberOfWorkers { get { return itsNumberOfWorkers; } set { itsNumberOfWorkers = value; } }
        public int NumberOfResources { get { return itsNumberOfResources; } set { itsNumberOfResources = value; } }


        public int MakeGoods()
        {
            int numberOfGoodsMade = 0;
            for (int i = 1; i <= itsNumberOfGoodsPerDay; i++)
            {
                Thread.Sleep(100);
                numberOfGoodsMade++;
            }
            return numberOfGoodsMade;
        }

        public double UpdateResources(double priceForOneResource)
        {
            double amountOfMoneySpentOnResources = 0.0;
            for (int i = 1; i <= itsNumberOfGoodsPerDay; i++)
                amountOfMoneySpentOnResources += priceForOneResource;
            return amountOfMoneySpentOnResources;
        }
        public double PaySalaries(double salaryForOneWorker)
        {
            double amountOfMoneySpentOnSalaries = 0.0;
            for (int i = 1; i <= itsNumberOfWorkers * 5; i++)
                amountOfMoneySpentOnSalaries += salaryForOneWorker;
            return amountOfMoneySpentOnSalaries;
        }
    }
}