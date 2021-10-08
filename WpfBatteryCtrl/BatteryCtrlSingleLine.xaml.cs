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
///	  Date: 		  10/07/21
///-----------------------------------------------------------------


namespace WpfBatteryCtrl
{
    /// <summary>
    /// Interaction logic for BatteryCtrlSingleLine.xaml
    /// </summary>
    public partial class BatteryCtrlSingleLine : BaseBatteryCtrl
    {
      

        private List<Border> _listTicks = new List<Border>();

        private Border _borderAround;
        private Border _borderTop;
        private Grid _gridTicks = new Grid();

        public BatteryCtrlSingleLine()
        {
            InitializeComponent();
            DrawBatteryBody();
            DrawGridTick();
        }
  
      
        public override void DrawBatteryBody()
        {
            Orientation align = this.BatteryOrientation;
            MainStackPanel.Children.Clear();

            if (_borderAround != null)
                _borderAround.Child = null;

            var color = this.HighThresholdColor;
            var thickness = new Thickness(2);
            var padding = new Thickness(2);

            var cornerRadiusAround = new CornerRadius(5);
            var widthAround = 100.0;
            var heightAround = 50.0;

            var cornerRadiusTop = new CornerRadius(0, 2, 2, 0);
            var widthTop = 7.0;
            var heightTop = 20.0;
            var marginTop = new Thickness(0, -2, 0, -2);

            var cornerRadiusTopVertical = new CornerRadius(2, 2, 0, 0);
            var marginTopVertical = new Thickness(-2, 0, -2, 0);

            switch (align)
            {
                case Orientation.Horizontal:
                    MainStackPanel.Orientation = Orientation.Horizontal;

                    _borderAround = new Border();
                    _borderAround.BorderBrush = color;
                    _borderAround.BorderThickness = thickness;
                    _borderAround.CornerRadius = cornerRadiusAround;
                    _borderAround.Padding = padding;
                    _borderAround.Width = widthAround;
                    _borderAround.Height = heightAround;
                    _borderAround.Child = _gridTicks;

                    _borderTop = new Border();
                    _borderTop.BorderBrush = color;
                    _borderTop.Background = color;
                    _borderTop.BorderThickness = thickness;
                    _borderTop.CornerRadius = cornerRadiusTop;
                    _borderTop.Padding = padding;
                    _borderTop.Width = widthTop;
                    _borderTop.Height = heightTop;
                    _borderTop.Margin = marginTop;

                    MainStackPanel.Children.Add(_borderAround);
                    MainStackPanel.Children.Add(_borderTop);
                    break;
                case Orientation.Vertical:
                    MainStackPanel.Orientation = Orientation.Vertical;
                    _borderAround = new Border();
                    _borderAround.BorderBrush = color;
                    _borderAround.BorderThickness = thickness;
                    _borderAround.CornerRadius = cornerRadiusAround;
                    _borderAround.Padding = padding;
                    _borderAround.Width = heightAround;//viceversa of horizontal
                    _borderAround.Height = widthAround;//viceversa of horizontal
                    _borderAround.Child = _gridTicks;

                    _borderTop = new Border();
                    _borderTop.BorderBrush = color;
                    _borderTop.Background = color;
                    _borderTop.BorderThickness = thickness;
                    _borderTop.CornerRadius = cornerRadiusTopVertical;
                    _borderTop.Padding = padding;
                    _borderTop.Width = heightTop;//viceversa of horizontal
                    _borderTop.Height = widthTop;//viceversa of horizontal
                    _borderTop.Margin = marginTopVertical;

                    MainStackPanel.Children.Add(_borderTop);
                    MainStackPanel.Children.Add(_borderAround);

                    break;
            }
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
                    Grid.SetRow(b, ticks - i);

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





        public override void UpdateValue()
        {
            int val = ((int)Value);
            int maxTicks = TickNumber;
            int tickToShow = val * maxTicks / MAXVALUE;

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
