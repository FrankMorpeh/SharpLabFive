
namespace SharpLabFour.Notification
{
    public class RecordNotChosen : INotification
    {
        public string Text { get; set; }

        public RecordNotChosen() { Text = "RECORD HASN'T BEEN CHOSEN!"; }
    }
    public class IncorrectNumberOfGoodsPerDay : INotification
    {
        public string Text { get; set; }

        public IncorrectNumberOfGoodsPerDay() { Text = "NUMBER OF GOODS MUST NOT BE LESS THAN ONE!"; }
    }
    public class IncorrectFormatOfNumberOfGoodsPerDay : INotification
    {
        public string Text { get; set; }

        public IncorrectFormatOfNumberOfGoodsPerDay() { Text = "NUMBER OF GOODS MUST BE AN INTEGER VALUE!"; }
    }
    public class None : INotification
    {
        public string Text { get; set; }

        public None() { Text = "NONE"; }
    }
}