using SharpLabFive.Models.Workshops;
using SharpLabFive.XmlLoggers.FactoryLoggers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SharpLabFive.Controllers.WorkshopControllers
{
    public class Factory : INotifyPropertyChanged
    {
        private ObservableCollection<Workshop> itsWorkshops;
        private double itsMoney;
        private int itsNumberOfGoodsMade;
        private int itsNumberOfResources;
        private Thread itsMakeGoodsThread;
        private Thread itsSellGoodsThread;
        private Thread itsBuyResourcesAndPaySalariesThread;
        private Mutex itsMoneyMutex;
        private Mutex itsNumberOfGoodsMutex;
        private Mutex itsNumberOfResourcesMutex;
        private int itsSellingTime;
        private bool itsSellGoodsThreadWorking;
        private bool itsBuyResourcesThreadWorking;
        private bool itsPauseMakingGoods;
        private bool itsPauseSellingGoods;
        private bool itsStopMakingGoods;
        private bool itsStopSellingGoods;
        private bool itsStopBuyingResources;
        private System.Timers.Timer itsLoggingTimer; 

        public ObservableCollection<Workshop> Workshops 
        { 
            get { return itsWorkshops; } set { itsWorkshops = value; OnPropertyChanged("Workshops"); } 
        }
        public double Money { get { return itsMoney; } set { itsMoney = value; OnPropertyChanged("Money"); } }
        public int NumberOfGoodsMade 
        { 
            get { return itsNumberOfGoodsMade; } set { itsNumberOfGoodsMade = value; OnPropertyChanged("NumberOfGoodsMade"); } 
        }
        public int NumberOfResources 
        { 
            get { return itsNumberOfResources; }
            set { itsNumberOfResources = value; OnPropertyChanged("NumberOfResources"); }
        }
        public int SellingTime { get { return itsSellingTime; } set { itsSellingTime = value; } }

        public Factory() : this(new ObservableCollection<Workshop>()) { }
        public Factory(ObservableCollection<Workshop> workshops)
        {
            Workshops = workshops;
            Money = 0;
            NumberOfGoodsMade = 0;
            NumberOfResources = 0;
            itsMakeGoodsThread = new Thread(MakeGoods) { IsBackground = true };
            itsSellGoodsThread = new Thread(SellGoods) { IsBackground = true };
            itsBuyResourcesAndPaySalariesThread = new Thread(BuyResourcesAndPaySalaries) { IsBackground = true };
            itsMoneyMutex = new Mutex();
            itsNumberOfGoodsMutex = new Mutex();
            itsNumberOfResourcesMutex = new Mutex();
            itsSellingTime = 1000;
            itsSellGoodsThreadWorking = false;
            itsBuyResourcesThreadWorking = false;
            itsPauseMakingGoods = false;
            itsPauseSellingGoods = false;
            itsStopMakingGoods = false;
            itsStopSellingGoods = false;
            itsStopBuyingResources = false;
            itsLoggingTimer = new System.Timers.Timer(30000); itsLoggingTimer.Elapsed += LoggingTimer_Elapsed;
        }
        private int GetCommonNumberOfResources()
        {
            int commonNumberOfResources = 0;
            foreach (Workshop workshop in itsWorkshops)
                commonNumberOfResources += workshop.NumberOfResources;
            return commonNumberOfResources;
        }

        // Data handling
        public void AddWorkshop(Workshop workshop)
        {
            itsWorkshops.Add(workshop);
        }
        public void RemoveWorkshop(Workshop workshop)
        {
            itsWorkshops.Remove(workshop);
        }

        
        // Making goods
        public void MakeGoodsAsParallel()
        {
            itsLoggingTimer.Start();
            NumberOfResources = GetCommonNumberOfResources();
            itsMakeGoodsThread.Start();
        }
        private void MakeGoods()
        {
            while (!itsStopMakingGoods)
            {
                if (!itsPauseMakingGoods)
                {
                    foreach (Workshop workshop in itsWorkshops)
                    {
                        while (NumberOfResources >= workshop.NumberOfGoodsPerDay * 5)
                        {
                            itsNumberOfGoodsMutex.WaitOne();
                            NumberOfGoodsMade += workshop.MakeGoods();
                            itsNumberOfGoodsMutex.ReleaseMutex();

                            itsNumberOfResourcesMutex.WaitOne();
                            NumberOfResources -= workshop.NumberOfGoodsPerDay * 5;
                            itsNumberOfResourcesMutex.ReleaseMutex();

                            if (!itsSellGoodsThreadWorking)
                                SellGoodsAsParallel();
                        }
                    }
                    if (!itsBuyResourcesThreadWorking)
                        BuyResourcesAndPaySalariesAsParallel();
                }
            }
        }
        public void ContinueMakingGoods()
        {
            itsPauseMakingGoods = false;
        }
        public void PauseMakingGoods()
        {
            itsPauseMakingGoods = true;
        }
        public void StopMakingGoodsAsParallel()
        {
            itsStopMakingGoods = true;
        }

        // Selling goods
        private void SellGoodsAsParallel()
        {
            itsSellGoodsThreadWorking = true;
            itsSellGoodsThread.Start();
        }
        private void SellGoods()
        {
            while (!itsStopSellingGoods)
            {
                if (!itsPauseSellingGoods)
                {
                    if (NumberOfGoodsMade > 0)
                    {
                        itsMoneyMutex.WaitOne();
                        Money += 100;
                        itsMoneyMutex.ReleaseMutex();

                        itsNumberOfGoodsMutex.WaitOne();
                        NumberOfGoodsMade -= 1;
                        itsNumberOfGoodsMutex.ReleaseMutex();

                        Thread.Sleep(itsSellingTime);
                    }
                }
            }
        }
        public void ContinueSellingGoods()
        {
            itsPauseSellingGoods = false;
        }
        public void PauseSellingGoods()
        {
            itsPauseSellingGoods = true;
        }
        public void StopSellingGoodsAsParallel()
        {
            itsStopSellingGoods = true;
        }

        // Updating factory
        private void BuyResourcesAndPaySalariesAsParallel()
        {
            itsBuyResourcesThreadWorking = true;
            itsBuyResourcesAndPaySalariesThread.Start();
        }
        private void BuyResourcesAndPaySalaries()
        {
            while (!itsStopBuyingResources)
            {
                BuyResources();
                PaySalaries();
            }
        }
        private void BuyResources()
        {
            foreach (Workshop workshop in itsWorkshops)
            {
                while (true)
                {
                    if (itsMoney >= workshop.NumberOfGoodsPerDay * 50)
                    {
                        itsMoneyMutex.WaitOne();
                        Money -= workshop.UpdateResources(50);
                        itsMoneyMutex.ReleaseMutex();

                        itsNumberOfResourcesMutex.WaitOne();
                        NumberOfResources += workshop.NumberOfGoodsPerDay * 5;
                        itsNumberOfResourcesMutex.ReleaseMutex();

                        break;
                    }
                }
                Thread.Sleep(300);
            }
        }
        private void PaySalaries()
        {
            foreach (Workshop workshop in itsWorkshops)
            {
                while (true)
                {
                    if (itsMoney >= workshop.NumberOfWorkers * 50)
                    {
                        itsMoneyMutex.WaitOne();
                        Money -= workshop.PaySalaries(50);
                        itsMoneyMutex.ReleaseMutex();

                        break;
                    }
                }
                Thread.Sleep(200);
            }
        }
        public void StopBuyingResourcesAndPaySalariesAsParallel()
        {
            itsStopBuyingResources = true;
        }


        // Timer event handlers
        private void LoggingTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            XmlFactoryLogger.LogFactory(this);
        }


        // MVVM events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}