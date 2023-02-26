using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SharpLabFive.Models.Workshops
{
    public class Workshop : INotifyPropertyChanged
    {
        private int itsNumberOfGoodsPerDay;
        private int itsNumberOfWorkers;
        private int itsNumberOfResources;

        public Workshop(int numberOfGoodsPerDay)
        {
            itsNumberOfGoodsPerDay = numberOfGoodsPerDay;
            SetNumberOfWorkers();
            itsNumberOfResources = itsNumberOfGoodsPerDay * 5;
        }
        private void SetNumberOfWorkers()
        {
            if (itsNumberOfGoodsPerDay < 2)
                NumberOfWorkers = 1;
            else
                NumberOfWorkers = itsNumberOfGoodsPerDay / 2;
        }

        public int NumberOfGoodsPerDay 
        { 
            get { return itsNumberOfGoodsPerDay; } 
            set 
            { 
                itsNumberOfGoodsPerDay = value;
                SetNumberOfWorkers();
                NumberOfResources = itsNumberOfGoodsPerDay * 5;
                OnPropertyChanged("NumberOfGoodsPerDay");
            } 
        }
        public int NumberOfWorkers 
        { 
            get { return itsNumberOfWorkers; } set { itsNumberOfWorkers = value; OnPropertyChanged("NumberOfWorkers"); } 
        }
        public int NumberOfResources 
        { 
            get { return itsNumberOfResources; } set { itsNumberOfResources = value; OnPropertyChanged("NumberOfResources"); } 
        }


        public int MakeGoods()
        {
            int numberOfGoodsMade = 0;
            int numberOfResourcesToDeduct = NumberOfResources / NumberOfGoodsPerDay;
            for (int i = 1; i <= itsNumberOfGoodsPerDay; i++)
            {
                Thread.Sleep(500);
                numberOfGoodsMade++;
                NumberOfResources -= numberOfResourcesToDeduct;
            }
            return numberOfGoodsMade;
        }

        public double UpdateResources(double priceForOneResource)
        {
            double amountOfMoneySpentOnResources = 0.0;
            int numberOfResourcesToAdd = NumberOfResources / NumberOfGoodsPerDay;
            for (int i = 1; i <= itsNumberOfGoodsPerDay; i++)
            {
                amountOfMoneySpentOnResources += priceForOneResource;
                NumberOfResources += numberOfResourcesToAdd;
            }
            return amountOfMoneySpentOnResources;
        }
        public double PaySalaries(double salaryForOneWorker)
        {
            double amountOfMoneySpentOnSalaries = 0.0;
            for (int i = 1; i <= itsNumberOfWorkers * 5; i++)
                amountOfMoneySpentOnSalaries += salaryForOneWorker;
            return amountOfMoneySpentOnSalaries;
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