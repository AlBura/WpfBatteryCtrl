using System;
using System.Collections.Generic;
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


///-----------------------------------------------------------------
///   Namespace:      WpfBatteryCtrl
///   Class:          BatteryCtrlTwoLine
///   Description:    
///   Author:         Alberto Buranello
///	  Date: 		  10/08/21 
///-----------------------------------------------------------------


namespace WpfBatteryCtrl
{
    /// <summary>
    /// Interaction logic for BatteryCtrlTwoLine.xaml
    /// </summary>
    public partial class BatteryCtrlTwoLine : BaseBatteryCtrl
    {

        public static readonly DependencyProperty Value2Property =
             DependencyProperty.Register("Value2", typeof(double), typeof(BatteryCtrlTwoLine), new PropertyMetadata(0d, OnValue2Changed));


        private const double MAXLENGTH = 92.0;
        private const double MAXTHICK = 21.0;


        private Border _progress1 = new Border();
        private Border _progress2 = new Border();


        public BatteryCtrlTwoLine()
        {
            InitializeComponent();
            DrawBatteryBody();
            DrawGridTick();
            UpdateValue();
        }
        private static void OnValue2Changed(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BatteryCtrlTwoLine)o;
            bcsl.UpdateValue();
        }
        public double Value2
        {
            get => (double)GetValue(Value2Property);
            set
            {
                //wrap the value, the vlaue is in percentage 0-100
                var v = (double)value;
                if (v < 0.0) v = 0.0;
                if (v > MAXVALUE) v = MAXVALUE;
                SetValue(ValueProperty, v);
            }
        }


        public override void DrawBatteryBody()
        {       
            DrawBatteryBody(MainStackPanel);
        }




        public override void DrawGridTick()
        {
            _gridTicks.Children.Clear();
            _gridTicks.ColumnDefinitions.Clear();
            _gridTicks.RowDefinitions.Clear();
            bool isHorizontal = (this.BatteryOrientation == Orientation.Horizontal);
            if (isHorizontal)
            {
                _gridTicks.RowDefinitions.Add(new RowDefinition());
                _gridTicks.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(_progress1, 0);
                Grid.SetRow(_progress2, 1);
                _progress1.CornerRadius = new CornerRadius(0, 2, 2, 0);
                _progress2.CornerRadius = _progress1.CornerRadius;
                _progress1.HorizontalAlignment = HorizontalAlignment.Left;
                _progress2.HorizontalAlignment = _progress1.HorizontalAlignment;
            }
            else
            {
                _progress1.CornerRadius = new CornerRadius(2, 2, 0, 0);
                _progress2.CornerRadius = _progress1.CornerRadius;
                _gridTicks.ColumnDefinitions.Add(new ColumnDefinition());
                _gridTicks.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(_progress1, 0);
                Grid.SetColumn(_progress2, 1);
                _progress1.VerticalAlignment = VerticalAlignment.Bottom;
                _progress2.VerticalAlignment = _progress1.VerticalAlignment;

            }        
           
            _progress1.Padding = new Thickness(2);              
            _progress1.Width = MAXTHICK;
            _progress1.Height = MAXTHICK;
                     
            _progress2.Padding = new Thickness(2);
            _progress2.Width = MAXTHICK;
            _progress2.Height = MAXTHICK;

            _gridTicks.Children.Add(_progress1);
            _gridTicks.Children.Add(_progress2);
        }





        public override void UpdateValue()
        {

            var limit1 = LowThreshold;
            var limit2 = WarningThreshold;

            var colorL = LowThresholdColor;
            var colorW = WarningThresholdColor;
            var colorH = HighThresholdColor;
            //TODO

            //the progress goes from 1 to 290
            bool isHorizontal = (this.BatteryOrientation == Orientation.Horizontal);
            if (isHorizontal)
            {
                _progress1.Width = MAXLENGTH * Value / MAXVALUE;
                _progress2.Width = MAXLENGTH * Value2 / MAXVALUE;
            }
            else
            {
                _progress1.Height = MAXLENGTH * Value / MAXVALUE;
                _progress2.Height = MAXLENGTH * Value2 / MAXVALUE;
            }
          
            SolidColorBrush color1 = colorH;
            SolidColorBrush color2 = colorH;
            if (Value < limit1)
                color1 = colorL;
            else if (Value < limit2)
                color1 = colorW;
            if (Value2 < limit1)
                color2 = colorL;
            else if (Value2 < limit2)
                color2 = colorW;

            _progress1.Background = color1;
            _progress2.Background = color2;

            _borderAround.BorderBrush = (Value<Value2)?color1:color2;
            _borderTop.Background = _borderAround.BorderBrush;
            _borderTop.BorderBrush = _borderAround.BorderBrush;


        }
    }
}
