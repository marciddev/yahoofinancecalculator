using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.Windows.Xps.Serialization;

namespace stock_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _lowerValue;
        public int LowerValue
        {
            get => _lowerValue;
            set => SetField(ref _lowerValue, value);
        }

        private int _higherValue = 1;
        public int HigherValue
        {
            get => _higherValue;
            set => SetField(ref _higherValue, value);
        }
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private async void compareValues_Click(object sender, RoutedEventArgs e)
        {
            holidayClass holidayData = await grabHolidayData.getHolidayData();
            double CalculateChange(double previous, double current)
            {
                if (previous == 0)
                    throw new InvalidOperationException();

                var change = current - previous;
                return (double)change / previous;
            }
            string DoubleToPercentageString(double d)
            {
                return "" + (d * 100).ToString("#.##") + "%";
            }
            DateTime current = DateTime.Now;
            DateTime comparingToIVE = DateTime.Now;
            if(LowerValue != 0)
            {
                comparingToIVE = comparingToIVE.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIVE = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider

            //NEW YEARS
                if(comparingToIVE.ToString("MM-dd") == "01-01")
                {
                comparingToIVE = comparingToIVE.AddDays(-1);
                }
                if(comparingFromIVE.ToString("MM-dd") == "01-01")
                {
                comparingFromIVE = comparingFromIVE.AddDays(-1);
                }



                //MLKJ
            if(comparingToIVE.ToString("MM") == "01" && comparingToIVE.DayOfWeek == DayOfWeek.Monday)
            {
                if(Int32.Parse(comparingToIVE.ToString("dd")) > 14 && Int32.Parse(comparingToIVE.ToString("dd")) > 22)
                {
                    comparingToIVE.AddDays(-1);
                }
            }
            if (comparingFromIVE.ToString("MM") == "01" && comparingFromIVE.DayOfWeek == DayOfWeek.Monday)
            {
                if (Int32.Parse(comparingFromIVE.ToString("dd")) > 14 && Int32.Parse(comparingFromIVE.ToString("dd")) > 22)
                {
                    MessageBox.Show("yes");
                    comparingFromIVE.AddDays(-1);
                }
            }




            if (comparingToIVE.DayOfWeek == DayOfWeek.Saturday)
                {
                    comparingToIVE = comparingToIVE.AddDays(-1);
                }
                if (comparingToIVE.DayOfWeek == DayOfWeek.Sunday)
                {
                    comparingToIVE = comparingToIVE.AddDays(-2);
                }
                if (comparingFromIVE.DayOfWeek == DayOfWeek.Saturday)
                {
                    comparingFromIVE = comparingFromIVE.AddDays(-1);
                }
                if (comparingFromIVE.DayOfWeek == DayOfWeek.Sunday)
                {
                    comparingFromIVE = comparingFromIVE.AddDays(-2);
                }
                
            
            

            apiStuff IVE = await OldMain.grabData("IVE");

            double comparingToCloseIVE = 0.00;
            double comparingFromCloseIVE = 0.00;


                for (var i = IVE.historical.Count - 1; i >= 0; i--)
                {
                    if (IVE.historical[i].date == comparingToIVE.ToString("yyyy-MM-dd"))
                    {
                        comparingToCloseIVE = double.Parse(IVE.historical[i].close);
                    }
                    if (IVE.historical[i].date == comparingFromIVE.ToString("yyyy-MM-dd"))
                    {
                        comparingFromCloseIVE = double.Parse(IVE.historical[i].close);
                    }
                }


            double differenceIVE = CalculateChange(comparingFromCloseIVE, comparingToCloseIVE);
            String percentageChangeIVE = DoubleToPercentageString(differenceIVE);
            this.IVE.Content = percentageChangeIVE;





            
            DateTime comparingToIVV = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIVV = comparingToIVV.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIVV = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIVV.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIVV = comparingToIVV.AddDays(-1);
            }
            if (comparingToIVV.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIVV = comparingToIVV.AddDays(-2);
            }
            if (comparingFromIVV.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIVV = comparingFromIVV.AddDays(-1);
            }
            if (comparingFromIVV.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIVV = comparingFromIVV.AddDays(-2);
            }

            apiStuff IVV = await OldMain.grabData("IVV");
            double comparingToCloseIVV = 0.00;
            double comparingFromCloseIVV = 0.00;
            for (var i = IVV.historical.Count - 1; i > 0; i--)
            {
                if (IVV.historical[i].date == comparingToIVV.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIVV = double.Parse(IVV.historical[i].close);
                }
                if (IVV.historical[i].date == comparingFromIVV.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIVV = double.Parse(IVV.historical[i].close);
                }
            }

            double differenceIVV = CalculateChange(comparingFromCloseIVV, comparingToCloseIVV);
            String percentageChangeIVV = DoubleToPercentageString(differenceIVV);
            this.IVV.Content = percentageChangeIVV;









            DateTime comparingToIVW = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIVW = comparingToIVW.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIVW = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIVW.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIVW = comparingToIVW.AddDays(-1);
            }
            if (comparingToIVW.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIVW = comparingToIVW.AddDays(-2);
            }
            if (comparingFromIVW.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIVW = comparingFromIVW.AddDays(-1);
            }
            if (comparingFromIVW.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIVW = comparingFromIVW.AddDays(-2);
            }

            apiStuff IVW = await OldMain.grabData("IVW");
            double comparingToCloseIVW = 0.00;
            double comparingFromCloseIVW = 0.00;
            for (var i = IVW.historical.Count - 1; i > 0; i--)
            {
                if (IVW.historical[i].date == comparingToIVW.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIVW = double.Parse(IVW.historical[i].close);
                }
                if (IVW.historical[i].date == comparingFromIVW.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIVW = double.Parse(IVW.historical[i].close);
                }
            }

            double differenceIVW = CalculateChange(comparingFromCloseIVW, comparingToCloseIVW);
            String percentageChangeIVW = DoubleToPercentageString(differenceIVW);
            this.IVW.Content = percentageChangeIVW;




            DateTime comparingToIJJ = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIJJ = comparingToIJJ.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIJJ = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIJJ.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIJJ = comparingToIJJ.AddDays(-1);
            }
            if (comparingToIJJ.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIJJ = comparingToIJJ.AddDays(-2);
            }
            if (comparingFromIJJ.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIJJ = comparingFromIJJ.AddDays(-1);
            }
            if (comparingFromIJJ.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIJJ = comparingFromIJJ.AddDays(-2);
            }

            apiStuff IJJ = await OldMain.grabData("IJJ");
            double comparingToCloseIJJ = 0.00;
            double comparingFromCloseIJJ = 0.00;
            for (var i = IJJ.historical.Count - 1; i > 0; i--)
            {
                if (IJJ.historical[i].date == comparingToIJJ.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIJJ = double.Parse(IJJ.historical[i].close);
                }
                if (IJJ.historical[i].date == comparingFromIJJ.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIJJ = double.Parse(IJJ.historical[i].close);
                }
            }

            double differenceIJJ = CalculateChange(comparingFromCloseIJJ, comparingToCloseIJJ);
            String percentageChangeIJJ = DoubleToPercentageString(differenceIJJ);
            this.IJJ.Content = percentageChangeIJJ;





            DateTime comparingToIJH = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIJH = comparingToIJH.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIJH = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIJH.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIJH = comparingToIJH.AddDays(-1);
            }
            if (comparingToIJH.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIJH = comparingToIJH.AddDays(-2);
            }
            if (comparingFromIJH.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIJH = comparingFromIJH.AddDays(-1);
            }
            if (comparingFromIJH.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIJH = comparingFromIJH.AddDays(-2);
            }

            apiStuff IJH = await OldMain.grabData("IJH");
            double comparingToCloseIJH = 0.00;
            double comparingFromCloseIJH = 0.00;
            for (var i = IJH.historical.Count - 1; i > 0; i--)
            {
                if (IJH.historical[i].date == comparingToIJH.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIJH = double.Parse(IJH.historical[i].close);
                }
                if (IJH.historical[i].date == comparingFromIJH.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIJH = double.Parse(IJH.historical[i].close);
                }
            }

            double differenceIJH = CalculateChange(comparingFromCloseIJH, comparingToCloseIJH);
            String percentageChangeIJH = DoubleToPercentageString(differenceIJH);
            this.IJH.Content = percentageChangeIJH;





            DateTime comparingToIJK = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIJK = comparingToIJK.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIJK = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIJK.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIJK = comparingToIJK.AddDays(-1);
            }
            if (comparingToIJK.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIJK = comparingToIJK.AddDays(-2);
            }
            if (comparingFromIJK.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIJK = comparingFromIJK.AddDays(-1);
            }
            if (comparingFromIJK.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIJK = comparingFromIJK.AddDays(-2);
            }

            apiStuff IJK = await OldMain.grabData("IJK");
            double comparingToCloseIJK = 0.00;
            double comparingFromCloseIJK = 0.00;
            for (var i = IJK.historical.Count - 1; i > 0; i--)
            {
                if (IJK.historical[i].date == comparingToIJK.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIJK = double.Parse(IJK.historical[i].close);
                }
                if (IJK.historical[i].date == comparingFromIJK.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIJK = double.Parse(IJK.historical[i].close);
                }
            }

            double differenceIJK = CalculateChange(comparingFromCloseIJK, comparingToCloseIJK);
            String percentageChangeIJK = DoubleToPercentageString(differenceIJK);
            this.IJK.Content = percentageChangeIJK;





            DateTime comparingToIWN = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIWN = comparingToIWN.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIWN = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIWN.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIWN = comparingToIWN.AddDays(-1);
            }
            if (comparingToIWN.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIWN = comparingToIWN.AddDays(-2);
            }
            if (comparingFromIWN.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIWN = comparingFromIWN.AddDays(-1);
            }
            if (comparingFromIWN.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIWN = comparingFromIWN.AddDays(-2);
            }

            apiStuff IWN = await OldMain.grabData("IWN");
            double comparingToCloseIWN = 0.00;
            double comparingFromCloseIWN = 0.00;
            for (var i = IWN.historical.Count - 1; i > 0; i--)
            {
                if (IWN.historical[i].date == comparingToIWN.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIWN = double.Parse(IWN.historical[i].close);
                }
                if (IWN.historical[i].date == comparingFromIWN.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIWN = double.Parse(IWN.historical[i].close);
                }
            }

            double differenceIWN = CalculateChange(comparingFromCloseIWN, comparingToCloseIWN);
            String percentageChangeIWN = DoubleToPercentageString(differenceIWN);
            this.IWN.Content = percentageChangeIWN;





            DateTime comparingToIWM = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIWM = comparingToIWM.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIWM = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIWM.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIWM = comparingToIWM.AddDays(-1);
            }
            if (comparingToIWM.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIWM = comparingToIWM.AddDays(-2);
            }
            if (comparingFromIWM.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIWM = comparingFromIWM.AddDays(-1);
            }
            if (comparingFromIWM.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIWM = comparingFromIWM.AddDays(-2);
            }

            apiStuff IWM = await OldMain.grabData("IWM");
            double comparingToCloseIWM = 0.00;
            double comparingFromCloseIWM = 0.00;
            for (var i = IWM.historical.Count - 1; i > 0; i--)
            {
                if (IWM.historical[i].date == comparingToIWM.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIWM = double.Parse(IWM.historical[i].close);
                }
                if (IWM.historical[i].date == comparingFromIWM.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIWM = double.Parse(IWM.historical[i].close);
                }
            }

            double differenceIWM = CalculateChange(comparingFromCloseIWM, comparingToCloseIWM);
            String percentageChangeIWM = DoubleToPercentageString(differenceIWM);
            this.IWM.Content = percentageChangeIWM;





            DateTime comparingToIWO = DateTime.Now;
            if (LowerValue != 0)
            {
                comparingToIWO = comparingToIWO.AddDays(LowerValue * -1);
            }
            DateTime comparingFromIWO = current.AddDays(HigherValue * -1);

            //checking if weekend
            //comparing to = left slider
            if (comparingToIWO.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingToIWO = comparingToIWO.AddDays(-1);
            }
            if (comparingToIWO.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingToIWO = comparingToIWO.AddDays(-2);
            }
            if (comparingFromIWO.DayOfWeek == DayOfWeek.Saturday)
            {
                comparingFromIWO = comparingFromIWO.AddDays(-1);
            }
            if (comparingFromIWO.DayOfWeek == DayOfWeek.Sunday)
            {
                comparingFromIWO = comparingFromIWO.AddDays(-2);
            }

            apiStuff IWO = await OldMain.grabData("IWO");
            double comparingToCloseIWO = 0.00;
            double comparingFromCloseIWO = 0.00;
            for (var i = IWO.historical.Count - 1; i > 0; i--)
            {
                if (IWO.historical[i].date == comparingToIWO.ToString("yyyy-MM-dd"))
                {
                    comparingToCloseIWO = double.Parse(IWO.historical[i].close);
                }
                if (IWO.historical[i].date == comparingFromIWO.ToString("yyyy-MM-dd"))
                {
                    comparingFromCloseIWO = double.Parse(IWO.historical[i].close);
                }
            }

            double differenceIWO = CalculateChange(comparingFromCloseIWO, comparingToCloseIWO);
            String percentageChangeIWO = DoubleToPercentageString(differenceIWO);
            this.IWO.Content = percentageChangeIWO;

        }


        Boolean locked = false;
        int lowerVal = 0;
        int higherVal = 0;
        int between = 0;
        private void lockValues_Click(object sender, RoutedEventArgs e)
        {
            if (locked == false)
            {
                this.lockValues.Content = "Locked!";
                locked = true;
                lowerVal = this.LowerValue;
                higherVal = this.HigherValue;
                between = higherVal - lowerVal;
                this.moveSlider.Visibility = Visibility.Visible;
                this.mainSlider.IsEnabled = false;
            } else
            {
                this.lockValues.Content = "Lock-In";
                locked = false;
                this.moveSlider.Visibility = Visibility.Hidden;
                this.mainSlider.IsEnabled = true;
            }
        }

        private void mainSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            if(locked == true)
            {
                
            }
        }

        private void moveSlider_Click(object sender, RoutedEventArgs e)
        {
            this.mainSlider.HigherValue = this.mainSlider.HigherValue + between;
        }
    }
}
