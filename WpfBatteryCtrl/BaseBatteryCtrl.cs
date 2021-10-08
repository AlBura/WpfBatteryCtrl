using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

///-----------------------------------------------------------------
///   Namespace:      WpfBatteryCtrl
///   Class:          BaseBatteryCtrl
///   Description:    
///   Author:         AlBura
///	  Date: 		  10/08/21 
///-----------------------------------------------------------------


namespace WpfBatteryCtrl
{
    public abstract class BaseBatteryCtrl : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
           DependencyProperty.Register("Value", typeof(double), typeof(BaseBatteryCtrl), new PropertyMetadata(0d, OnValueChanged));

    
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
            DependencyProperty.Register("BatteryOrientationProperty", typeof(Orientation), typeof(BaseBatteryCtrl), new PropertyMetadata(Orientation.Horizontal, OnOrientationChanged));


        protected const double MAXVALUE = 100.0;

        protected Border _borderAround;
        protected Border _borderTop;
        protected Grid _gridTicks = new Grid();
        public abstract void DrawBatteryBody();
        public abstract void DrawGridTick();
        public abstract void UpdateValue();


        protected void DrawBatteryBody(StackPanel mainStackPanel)
        { 
            Orientation align = this.BatteryOrientation;
            mainStackPanel.Children.Clear();

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
                    mainStackPanel.Orientation = Orientation.Horizontal;

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

                    mainStackPanel.Children.Add(_borderAround);
                    mainStackPanel.Children.Add(_borderTop);
                    break;
                case Orientation.Vertical:
                    mainStackPanel.Orientation = Orientation.Vertical;
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

                    mainStackPanel.Children.Add(_borderTop);
                    mainStackPanel.Children.Add(_borderAround);

                    break;
            }
    }






     
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
      
        public int LowThreshold
        {
            get => (int)GetValue(LowThresholdProperty);
            set
            {
                var v = (int)value;
                if (v < 0.0) v = 0;
                if (v > MAXVALUE) v = Convert.ToInt32(MAXVALUE);
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
                if (v > MAXVALUE) v = Convert.ToInt32(MAXVALUE);
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
      
        private static void OnOrientationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BaseBatteryCtrl)o;
            bcsl.DrawBatteryBody();
            bcsl.DrawGridTick();
            bcsl.UpdateValue();
        }


        private static void OnColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BaseBatteryCtrl)o;
            bcsl.SetValue(ValueProperty, bcsl.Value);
        }

        private static void OnLimitChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BaseBatteryCtrl)o;
            bcsl.SetValue(ValueProperty, bcsl.Value);
        }
        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bcsl = (BaseBatteryCtrl)o;
            bcsl.UpdateValue();
        }
        public Orientation BatteryOrientation
        {
            get => (Orientation)GetValue(BatteryOrientationProperty);
            set => SetValue(BatteryOrientationProperty, value);
        }

    }
}
