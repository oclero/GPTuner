using System.Windows;
using System.Windows.Controls;

namespace Tuner.Dels
{
    /// <summary>
    /// Logique d'interaction pour DelView.xaml
    /// </summary>
    public partial class Del : UserControl
    {
        public Del()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        /// <summary>
        /// On
        /// </summary>
        public bool On
        {
            get { return (bool)GetValue(OnProperty); }
            set { SetValue(OnProperty, value); }
        }
        public static readonly DependencyProperty OnProperty =
            DependencyProperty.Register("On", typeof(bool), typeof(Del), new PropertyMetadata(false));

        /// <summary>
        /// Level
        /// </summary>
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }
        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(int), typeof(Del), new PropertyMetadata(1));

        #endregion
    }
}