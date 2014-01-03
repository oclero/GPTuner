using Tuner.Polytune.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Tuner.Polytune
{
    /// <summary>
    /// Logique d'interaction pour DelGrid.xaml
    /// </summary>
    public partial class DelGrid : UserControl
    {
        public DelGrid()
        {
            InitializeComponent();
        }

        public ObservableCollection<List<DelModel>> Dels
        {
            get { return (ObservableCollection<List<DelModel>>)GetValue(DelsProperty); }
            set { SetValue(DelsProperty, value); }
        }

        #region Dependency properties

        public static DependencyProperty DelsProperty = DependencyProperty.Register("Dels", typeof(ObservableCollection<List<DelModel>>), typeof(DelGrid), new PropertyMetadata(OnDelsChanged));

        private static void OnDelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DelGrid delGrid = d as DelGrid;
            List<List<DelModel>> lsts = (List<List<DelModel>>)e.NewValue;
            delGrid.grid.ItemsSource = lsts;
        }

        #endregion

        
    }
}
