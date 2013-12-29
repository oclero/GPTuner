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
using Tuner.FrameworkMVVM;
using Tuner.Model;

namespace Tuner.WPFTuners
{
    /// <summary>
    /// Logique d'interaction pour Polytune.xaml
    /// </summary>
    public partial class Polytune : BaseUserControl
    {

        public Polytune()
        {
            InitializeComponent();
        }

        #region public vars

        public bool DisplayChromaticTuner
        {
            get { return (bool)GetValue(DisplayChromaticTunerProperty); }
            set { SetValue(DisplayChromaticTunerProperty, value); }
        }

        public bool DisplayPolyphonicTuner
        {
            get { return (bool)GetValue(DisplayPolyphonicTunerProperty); }
            set { SetValue(DisplayPolyphonicTunerProperty, value); }
        }
        #endregion

        #region Dependency properties

        public static DependencyProperty DisplayChromaticTunerProperty =
           DependencyProperty.Register("DisplayChromaticTuner", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, OnDisplayChromaticTunerChanged));

        public static DependencyProperty DisplayPolyphonicTunerProperty =
           DependencyProperty.Register("DisplayPolyphonicTuner", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, OnDisplayPolyphonicTunerChanged));

        private static void OnDisplayChromaticTunerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Window window = ((d as Window).DataContext) as Window;
            // TODO
        }

        private static void OnDisplayPolyphonicTunerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Window window = ((d as UserControl).DataContext) as Window;
            // TODO
        }

        #endregion

        public void displayValues(double[] errors)
        {

            polyphonic.setVUMeterValues(errors);
        }

        private void getErrors()
        {
            TunerModel mTuner = new TunerModel();
            double[] errors = mTuner.GetErrorsForAllStrings();
            displayValues(errors);
        }
    }
}
