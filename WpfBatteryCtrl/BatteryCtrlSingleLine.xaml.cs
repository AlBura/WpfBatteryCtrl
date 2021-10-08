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
///   Class:          BatteryCtrlSingleLine
///   Description:    
///   Author:         Alberto Buranello
///	  Date: 		  10/07/21 3:16:30 PM
///-----------------------------------------------------------------


namespace WpfBatteryCtrl
{
    /// <summary>
    /// Interaction logic for BatteryCtrlSingleLine.xaml
    /// </summary>
    public partial class BatteryCtrlSingleLine : UserControl
    {
       
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register("Value", typeof(double), typeof(BatteryCtrlSingleLine), new PropertyMetadata(0d,OnValueChanged));

        public static readonly DependencyProperty TickNumberProperty =
           DependencyProperty.Register("TickNumber", typeof(int), typeof(BatteryCtrlSingleLine), new PropertyMetadata(10, OnTickNumberChanged));

        public static readonly DependencyProperty LowThresholdProperty =
            DependencyProperty.Register("LowThresholdProperty", typeof(int), typeof(BatteryCtrlSingleLine), new PropertyMetadata(20, OnLimitChanged));

        public static readonly DependencyProperty WarningThresholdProperty =
           DependencyProperty.Register("WarningThresholdProperty", typeof(int), typeof(BatteryCtrlSingleLine), new PropertyMetadata(35, OnLimitChanged));

        public static readonly DependencyProperty LowThresholdColorProperty =
           DependencyProperty.Register("LowThresholdColorProperty", typeof(SolidColorBrush), typeof(BatteryCtrlSingleLine), new PropertyMetadata(new SolidColorBrush(Colors.Red), OnColorChanged));

        public static readonly DependencyProperty WarningThresholdColorProperty =
           DependencyProperty.Register("WarningThresholdColorProperty", typeof(SolidColorBrush), typeof(BatteryCtrlSingleLine), new PropertyMetadata(new SolidColorBrush(Colors.DarkOrange), OnColorChanged));

        public static readonly DependencyProperty HighThresholdColorProperty =
            DependencyProperty.Register("HighThresholdColorProperty", typeof(SolidColorBrush), typeof(BatteryCtrlSingleLine), new PropertyMetadata(new SolidColorBrush(Colors.Green), OnColorChanged));





        private const int MAXVALUE = 100;
        private List<Border> _listTicks = new List<Border>();

        public BatteryCtrlSingleLine()
        {
            InitializeComponent();
            DrawGridTick();
        }


        private void DrawGridTick()
        {
            tickGrid.Children.Clear();
            tickGrid.ColumnDefinitions.Clear();
            _listTicks.Clear();
            int ticks = TickNumber;
            for (int i =0; i<ticks; i++)
                tickGrid.ColumnDefinitions.Add(new ColumnDefinition());
      

            for (int i = 0; i < ticks; i++)
            {
                Border b = new Border();
                Grid.SetColumn(b, i);
                b.HorizontalAlignment = HorizontalAlignment.Stretch;
                b.VerticalAlignment = VerticalAlignment.Stretch;
                b.Background = new SolidColorBrush(Colors.DarkGreen);
                b.CornerRadius = new CornerRadius(1, 1, 1, 1);
                b.Margin = new Thickness(0, 0, 1, 0);
                b.Visibility = Visibility.Hidden;
                tickGrid.Children.Add(b);
                _listTicks.Add(b);
            }
        }


        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set {
                //wrap the value, the vlaue is in percentage 0-100
                var v = (double)value;
                if (v < 0.0) v = 0.0;
                if (v > MAXVALUE) v = MAXVALUE;
                SetValue(ValueProperty, v);     
                }
        }
        public int TickNumber
        {
            get => (int)GetValue(TickNumberProperty);
            set
            {
                //wrap the value, 1-100
                var v = (int)value;
                if (v < 2) v = 2;
                SetValue(TickNumberProperty, v);
            }
        }
        public int LowThreshold
        {
            get => (int)GetValue(LowThresholdProperty);
            set
            {
                var v = (int)value;
                if (v < 0.0) v = 0;
                if (v > MAXVALUE) v = MAXVALUE;
                SetValue(LowThresholdProperty, v);
            }
        }


        public int WarningThreshold
        {
            get => (int)GetValue(WarningThresholdProperty);
            set
            {
                var v = (int)value;
                if (v < 0.0) v = 0;
                if (v > MAXVALUE) v = MAXVALUE;
                SetValue(WarningThresholdProperty, v);
            }
        }

        public SolidColorBrush LowThresholdColor
        {
            get => (SolidColorBrush)GetValue(LowThresholdColorProperty);
            set => SetValue(LowThresholdColorProperty, value);
        }

        public SolidColorBrush WarningThresholdColor
        {
            get => (SolidColorBrush)GetValue(WarningThresholdColorProperty);
            set => SetValue(WarningThresholdColorProperty, value);
        }
        public SolidColorBrush HighThresholdColor
        {
            get => (SolidColorBrush)GetValue(HighThresholdColorProperty);
            set => SetValue(HighThresholdColorProperty, value);
        }
        private static void OnTickNumberChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BatteryCtrlSingleLine)o;
            bcsl.DrawGridTick();
        }


        private static void OnColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BatteryCtrlSingleLine)o;
            bcsl.SetValue(ValueProperty, bcsl.Value);
        }

        private static void OnLimitChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BatteryCtrlSingleLine)o;
            bcsl.SetValue(ValueProperty, bcsl.Value);
        }


        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BatteryCtrlSingleLine)o;
            var val =(int)((double)e.NewValue);

            int maxTicks = bcsl.TickNumber;
            int tickToShow = val * maxTicks / MAXVALUE;

            var list = bcsl._listTicks;
            var limit1 = bcsl.LowThreshold;
            var limit2 = bcsl.WarningThreshold;

            var color1 = bcsl.LowThresholdColor;
            var color2 = bcsl.WarningThresholdColor;
            var color3 = bcsl.HighThresholdColor;


            if (list.Count() < maxTicks) return;

            for (int i= 0; i < maxTicks; i++)
            {
                list[i].Visibility = (i < tickToShow) ? Visibility.Visible : Visibility.Hidden;
                SolidColorBrush color = color3;
                if (val < limit1)
                    color = color1;
                else if (val < limit2)
                    color = color2;

                bcsl.Border1.BorderBrush = color;
                bcsl.Border2.Background = color;
                list[i].Background = color;

            }
                
           












      /*      SolidColorBrush scb =  new SolidColorBrush( Colors.Red);
            li.Border1.BorderBrush = scb;
            li.Border2.Background = scb;
            li.Segment1.Background = scb;
            li.Segment2.Background = scb;
            li.Segment3.Background = scb;
            li.Segment4.Background = scb;
            li.Segment5.Background = scb;
            li.Segment6.Background = scb;
            li.Segment7.Background = scb;
            li.Segment8.Background = scb;
            li.Segment9.Background = scb;
            li.Segment10.Background = scb;
*/
        }
    }
}
