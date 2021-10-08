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
///   Class:          BatteryCtrlNumber
///   Description:    
///   Author:         AlBura
///   Date: 		  10/08/21 
///-----------------------------------------------------------------


namespace WpfBatteryCtrl
{
    /// <summary>
    /// Interaction logic for BatteryCtrlNumber.xaml
    /// </summary>
    public partial class BatteryCtrlNumber :  BaseBatteryCtrl
    {

        private TextBlock _textBlock = new TextBlock();

        public BatteryCtrlNumber()
        {
            InitializeComponent();

            this.FontSize = 20;
            this.FontFamily = new FontFamily("Century Gothic");

            DrawBatteryBody();
            DrawGridTick();
            UpdateValue();
        }

        public override void DrawBatteryBody()
        {
            DrawBatteryBody(MainStackPanel);
        }
        public override void UpdateValue()
        {
            int val = Convert.ToInt32(Math.Ceiling(Value));

            _textBlock.Text = val + "%";

            SolidColorBrush color = HighThresholdColor;
            if (val < LowThreshold)
                color = LowThresholdColor;
            else if (val < WarningThreshold)
                color = WarningThresholdColor;

            _textBlock.Foreground = color;
            _borderTop.BorderBrush = color;
            _borderTop.Background = color;
            _borderAround.BorderBrush = color;
        }
        public override void DrawGridTick()
        {
            _gridTicks.Children.Clear();
            _gridTicks.ColumnDefinitions.Clear();
            _gridTicks.ColumnDefinitions.Add(new ColumnDefinition());
            _gridTicks.RowDefinitions.Add(new RowDefinition());

            _textBlock.FontFamily = this.FontFamily;
            _textBlock.FontSize = this.FontSize;
            _textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            _textBlock.VerticalAlignment = VerticalAlignment.Center;
            _textBlock.TextAlignment = TextAlignment.Center;
            Grid.SetColumn(_textBlock, 0);
            Grid.SetRow(_textBlock,0);
            _gridTicks.Children.Add(_textBlock);

          

        }
    }
}
