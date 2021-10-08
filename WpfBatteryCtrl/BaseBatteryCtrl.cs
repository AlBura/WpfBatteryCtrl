using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

///-----------------------------------------------------------------
///   Namespace:      WpfBatteryCtrl
///   Class:          BaseBatteryCtrl
///   Description:    
///   Author:         Alberto Buranello
///	  Date: 		  10/08/21 11:46:01 AM
///   Saipem spa
///-----------------------------------------------------------------


namespace WpfBatteryCtrl
{
    public abstract class BaseBatteryCtrl : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
           DependencyProperty.Register("Value", typeof(double), typeof(BaseBatteryCtrl), new PropertyMetadata(0d, OnValueChanged));

        public static readonly DependencyProperty TickNumberProperty =
           DependencyProperty.Register("TickNumber", typeof(int), typeof(BaseBatteryCtrl), new PropertyMetadata(10, OnTickNumberChanged));

        public static readonly DependencyProperty LowThresholdProperty =
            DependencyProperty.Register("LowThresholdProperty", typeof(int), typeof(BaseBatteryCtrl), new PropertyMetadata(20, OnLimitChanged));

        public static readonly DependencyProperty WarningThresholdProperty =
           DependencyProperty.Register("WarningThresholdProperty", typeof(int), typeof(BaseBatteryCtrl), new PropertyMetadata(35, OnLimitChanged));

        public static readonly DependencyProperty LowThresholdColorProperty =
           DependencyProperty.Register("LowThresholdColorProperty", typeof(SolidColorBrush), typeof(BaseBatteryCtrl), new PropertyMetadata(new SolidColorBrush(Colors.Red), OnColorChanged));

        public static readonly DependencyProperty WarningThresholdColorProperty =
           DependencyProperty.Register("WarningThresholdColorProperty", typeof(SolidColorBrush), typeof(BaseBatteryCtrl), new PropertyMetadata(new SolidColorBrush(Colors.DarkOrange), OnColorChanged));

        public static readonly DependencyProperty HighThresholdColorProperty =
            DependencyProperty.Register("HighThresholdColorProperty", typeof(SolidColorBrush), typeof(BaseBatteryCtrl), new PropertyMetadata(new SolidColorBrush(Colors.Green), OnColorChanged));

        public static readonly DependencyProperty BatteryOrientationProperty =
            DependencyProperty.Register("BatteryOrientationProperty", typeof(Orientation), typeof(BaseBatteryCtrl), new PropertyMetadata(Orientation.Horizontal, OnAlignementChanged));


        protected const int MAXVALUE = 100;


        public abstract void DrawBatteryBody();
        public abstract void DrawGridTick();
        public abstract void UpdateValue();

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
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
        private static void OnAlignementChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BatteryCtrlSingleLine)o;
            bcsl.DrawBatteryBody();
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
            bcsl.UpdateValue();
        }
        public Orientation BatteryOrientation
        {
            get => (Orientation)GetValue(BatteryOrientationProperty);
            set => SetValue(BatteryOrientationProperty, value);
        }

    }
}
