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
///   Author:         AlBura
///	  Date: 		  10/07/21
///-----------------------------------------------------------------


namespace WpfBatteryCtrl
{
    /// <summary>
    /// Interaction logic for BatteryCtrlSingleLine.xaml
    /// </summary>
    public partial class BatteryCtrlSingleLine : BaseBatteryCtrl
    {

        public static readonly DependencyProperty TickNumberProperty =
                DependencyProperty.Register("TickNumber", typeof(int), typeof(BatteryCtrlSingleLine), new PropertyMetadata(10, OnTickNumberChanged));




        private List<Border> _listTicks = new List<Border>();
                    

        public BatteryCtrlSingleLine()
        {
            InitializeComponent();
            DrawBatteryBody();
            DrawGridTick();
            UpdateValue();

        }
  
      
        public override void DrawBatteryBody()
        {
            DrawBatteryBody(MainStackPanel);
        }


        private static void OnTickNumberChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BatteryCtrlSingleLine)o;
            bcsl.DrawGridTick();
        }

        public override void DrawGridTick()
        {
            _gridTicks.Children.Clear();
            _gridTicks.ColumnDefinitions.Clear();
            _listTicks.Clear();
            int ticks = TickNumber;
            bool isHorizontal = (this.BatteryOrientation == Orientation.Horizontal);
            if (isHorizontal)
            {
                for (int i = 0; i < ticks; i++)
                    _gridTicks.ColumnDefinitions.Add(new ColumnDefinition());
            }
            else
            {
                for (int i = 0; i < ticks; i++)
                    _gridTicks.RowDefinitions.Add(new RowDefinition());
            }



            for (int i = 0; i < ticks; i++)
            {
                Border b = new Border();
                if (isHorizontal)
                    Grid.SetColumn(b, i);
                else
                    Grid.SetRow(b, ticks-1 - i);

                b.HorizontalAlignment = HorizontalAlignment.Stretch;
                b.VerticalAlignment = VerticalAlignment.Stretch;
                b.Background = new SolidColorBrush(Colors.DarkGreen);
                b.CornerRadius = new CornerRadius(1, 1, 1, 1);
                b.Margin = new Thickness(0, 0, 1, 0);
                b.Visibility = Visibility.Hidden;
                _gridTicks.Children.Add(b);
                _listTicks.Add(b);
            }
        }


        public int TickNumber
        {
            get => (int)GetValue(TickNumberProperty);
            set
            {
                var v = (int)value;
                if (v < 2) v = 2;
                SetValue(TickNumberProperty, v);
            }
        }


        public override void UpdateValue()
        {
            double val = Value;
            double maxTicks = TickNumber;
            int tickToShow = Convert.ToInt32(Math.Ceiling(val * maxTicks / MAXVALUE));

            var list = _listTicks;
            var limit1 = LowThreshold;
            var limit2 = WarningThreshold;

            var color1 = LowThresholdColor;
            var color2 = WarningThresholdColor;
            var color3 = HighThresholdColor;

            if (list.Count() < maxTicks) return;

            for (int i = 0; i < maxTicks; i++)
            {
                list[i].Visibility = (i < tickToShow) ? Visibility.Visible : Visibility.Hidden;
                SolidColorBrush color = color3;
                if (val < limit1)
                    color = color1;
                else if (val < limit2)
                    color = color2;

                _borderTop.BorderBrush = color;
                _borderTop.Background = color;
                _borderAround.BorderBrush = color;
                list[i].Background = color;
            }
        }
    }
    
}
