using SharpLabFive.Converters.TimeConverters;
using SharpLabFive.Models.Workshops;
using System.Collections.Generic;
using System.Threading;

namespace SharpLabFive.Controllers.WorkshopControllers
{
    public class Factory
    {
        private List<Workshop> itsWorkshops;
        private double itsMoney;
        private int itsNumberOfGoodsMade;
        private Thread itsMakeGoodsThread;
        private Thread itsSellGoodsThread;
        private Thread itsBuyResourcesAndPaySalariesThread;
        private static readonly object itsThreadLocker; // for synchronizing creation and selling of goods
        private Mutex itsMoneyMutex;
        private Mutex itsNumberOfGoodsMutex;

        public List<Workshop> Workshops { get { return itsWorkshops; } set { itsWorkshops = value; } }
        public double Money { get { return itsMoney; } set { itsMoney = value; } }
        public int NumberOfGoodsMade { get { return itsNumberOfGoodsMade; } set { itsNumberOfGoodsMade = value; } }

        public Factory() : this(new List<Workshop>()) { }
        public Factory(List<Workshop> workshops)
        {
            itsWorkshops = workshops;
            itsMoney = 0.0;
            itsNumberOfGoodsMade = 0;
            itsMakeGoodsThread = new Thread(MakeGoods) { IsBackground = true };
            itsSellGoodsThread = new Thread(SellGoods) { IsBackground = true };
            itsBuyResourcesAndPaySalariesThread = new Thread(BuyResourcesAndPaySalaries) { IsBackground = true };
            itsMoneyMutex = new Mutex();
            itsNumberOfGoodsMutex = new Mutex();
        }

        
        // Making goods
        public void MakeGoodsAsParallel()
        {
            itsMakeGoodsThread.Start();
        }
        private void MakeGoods()
        {
            lock (itsThreadLocker)
            {
                while (true)
                {
                    foreach (Workshop workshop in itsWorkshops)
                    {
                        itsNumberOfGoodsMutex.WaitOne();
                        itsNumberOfGoodsMade += workshop.MakeGoods();
                        itsNumberOfGoodsMutex.ReleaseMutex();
                    }
                    Monitor.Pulse(itsThreadLocker);
                    Monitor.Wait(itsThreadLocker); // wait until new resources are bought and salaries paid
                }
            }
        }

        // Selling goods
        public void SellGoodsAsParallel(int sellTimeInSeconds)
        {
            itsSellGoodsThread.Start(sellTimeInSeconds);
        }
        private void SellGoods(object sellTimeInSecondsUnpacked)
        {
            while (true)
            {
                int sellTimeInMilliseconds = TimeConverter.ToMillisecondsFromSeconds((int)sellTimeInSecondsUnpacked);
                int numberOfIterations = itsNumberOfGoodsMade;
                for (int i = 1; i <= numberOfIterations; i++)
                {
                    itsMoneyMutex.WaitOne();
                    itsMoney += 100;
                    itsMoneyMutex.ReleaseMutex();

                    itsNumberOfGoodsMutex.WaitOne();
                    itsNumberOfGoodsMade--;
                    itsNumberOfGoodsMutex.ReleaseMutex();

                    Thread.Sleep(sellTimeInMilliseconds);
                }
                itsSellGoodsThread.Suspend();
            }
        }

        // Updating factory
        public void BuyResourcesAndPaySalariesAsParallel()
        {
            itsBuyResourcesAndPaySalariesThread.Start();
        }
        private void BuyResourcesAndPaySalaries()
        {
            lock (itsThreadLocker)
            {
                while (true)
                {
                    BuyResources();
                    PaySalaries();
                    Monitor.Pulse(itsThreadLocker);
                    Monitor.Wait(itsThreadLocker);
                }
            }
        }
        private void BuyResources()
        {
            double priceForOneResource = GetPriceForOneResource();
            foreach (Workshop workshop in itsWorkshops)
            {
                itsMoneyMutex.WaitOne();
                itsMoney -= workshop.UpdateResources(priceForOneResource);
                itsMoneyMutex.ReleaseMutex();
            }
        }
        private double GetPriceForOneResource()
        {
            double numberOfResourcesNeeded = 0.0;
            foreach (Workshop workshop in itsWorkshops)
                numberOfResourcesNeeded = workshop.NumberOfGoodsPerDay * 5;
            return (itsMoney / 2) / numberOfResourcesNeeded;
        }
        private void PaySalaries()
        {
            double salaryForOneWorker = itsMoney / GetNumberOfWorkers(); // no need to divide money by two as resources have been bought before
            foreach (Workshop workshop in itsWorkshops)
            {
                itsMoneyMutex.WaitOne();
                itsMoney -= workshop.PaySalaries(salaryForOneWorker);
                itsMoneyMutex.ReleaseMutex();
            }
        }
        private int GetNumberOfWorkers()
        {
            int numberOfWorkers = 0;
            foreach (Workshop workshop in itsWorkshops)
                numberOfWorkers += workshop.NumberOfWorkers;
            return numberOfWorkers;
        }
    }
}