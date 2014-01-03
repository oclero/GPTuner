using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Tuner.Polytune
{
    /// <summary>
    /// Logique d'interaction pour DelView.xaml
    /// </summary>
    public partial class Del : UserControl
    {
        public const int LEVEL_0 = 0;
        public const int LEVEL_WRONG = 1;
        public const int LEVEL_RIGHT = 2;

        public Del()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On
        /// </summary>
        public bool On
        {
            get { return (bool)GetValue(OnProperty); }
            set { SetValue(OnProperty, value); }
        }

        /// <summary>
        /// Level
        /// </summary>
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        #region Dependency Properties

        public static readonly DependencyProperty OnProperty =
            DependencyProperty.Register("On", typeof(bool), typeof(Del), new PropertyMetadata(false, OnOnChanged));

        private static void OnOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Del del = d as Del;
            bool newValue = (bool)e.NewValue;
            if (newValue)
            {
                del.DelLight.Visibility = Visibility.Visible;
            }
            else
            {
                del.DelLight.Visibility = Visibility.Hidden;
            }
        }

        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(int), typeof(Del), new PropertyMetadata(0, OnLevelChanged));

        private static void OnLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Del del = d as Del;
            SolidColorBrush b;
            switch ((int)e.NewValue)
            {
                case 0:
                    b = (SolidColorBrush)Application.Current.FindResource("DELColor-Red");
                    break;
                case LEVEL_WRONG:
                    b = (SolidColorBrush)Application.Current.FindResource("DELColor-Red");
                    break;
                case LEVEL_RIGHT:
                    b = (SolidColorBrush)Application.Current.FindResource("DELColor-Green");
                    break;
                default:
                    b = (SolidColorBrush)Application.Current.FindResource("DELColor-Green");
                    break;
            }

            DropShadowEffect light = new DropShadowEffect
                {
                    Color = b.Color,
                    Direction = 320,
                    ShadowDepth = 0,
                    Opacity = 1
                };
            del.DelLight.Fill = b;
            del.Effect = light;
        }

        #endregion


    }
}