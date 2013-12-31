using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tuner.FrameworkMVVM;
using Tuner.Model;

namespace Tuner.WPFTuners
{
    /// <summary>
    /// Logique d'interaction pour Polytune.xaml
    /// </summary>
    public partial class Polytune : BaseUserControl
    {

        #region Static vars

        private static bool DEFAULT_POLYPHONIC_VISIBILITY = false;
        private static bool DEFAULT_CHROMATIC_VISIBILITY = false;
        private static Converters.BooleanToVisibilityConverter visibilityConverter = new Converters.BooleanToVisibilityConverter();
        #endregion

        #region Constructor

        public Polytune()
        {
            DataContext = this;
            InitializeComponent();
            DisplayChromaticTuner = DEFAULT_CHROMATIC_VISIBILITY;
            DisplayPolyphonicTuner = DEFAULT_POLYPHONIC_VISIBILITY;
        }

        #endregion

        private bool displayChromaticTuner = DEFAULT_CHROMATIC_VISIBILITY;
        public bool DisplayChromaticTuner
        {
            get { return displayChromaticTuner; }
            set
            {
                chromatic.Visibility = (Visibility)visibilityConverter.Convert(value, null, null, null);
                NotifyPropertyChanged("DisplayChromaticTuner");
            }
        }

        private bool displayPolyphonicTuner = DEFAULT_POLYPHONIC_VISIBILITY;
        public bool DisplayPolyphonicTuner
        {
            get { return displayPolyphonicTuner; }
            set
            {
                polyphonic.Visibility = (Visibility)visibilityConverter.Convert(value, null, null, null);
                NotifyPropertyChanged("DisplayPolyphonicTuner");
            }
        }

        /*
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
           DependencyProperty.Register("DisplayChromaticTuner", typeof(bool), typeof(Polytune), new PropertyMetadata(DEFAULT_CHROMATIC_VISIBILITY, OnChromaticVisibilityChanged));

        public static DependencyProperty DisplayPolyphonicTunerProperty =
           DependencyProperty.Register("DisplayPolyphonicTuner", typeof(bool), typeof(Polytune), new PropertyMetadata(DEFAULT_POLYPHONIC_VISIBILITY, OnPolyphonicVisibilityChanged));

        private static void OnChromaticVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Polytune polytune = ((d as UserControl).DataContext) as Polytune;
            polytune.updateDisplay((bool)e.NewValue, polytune.DisplayPolyphonicTuner);
        }

        private static void OnPolyphonicVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Polytune polytune = ((d as UserControl).DataContext) as Polytune;
            polytune.updateDisplay(polytune.DisplayChromaticTuner, (bool)e.NewValue);
        }

        #endregion
        */

        public void displayValues(double[] errors)
        {
            DisplayChromaticTuner = false;
            polyphonic.TunerValues = new List<double>(errors);
            DisplayPolyphonicTuner = true;
        }

        public void displayValues(double error)
        {
            DisplayPolyphonicTuner = false;
            chromatic.TunerValue = error;
            DisplayChromaticTuner = true;
        }

        public void updateDisplay(bool displayChromatic, bool displayPolyphonic)
        {
            DisplayChromaticTuner = displayChromatic;
            DisplayPolyphonicTuner = displayPolyphonic;
        }

        private TunerModel mTuner = new TunerModel();

        public void displayErrors(List<GuitarString> playedStrings)
        {
            double[] errors = mTuner.GetErrorsForAllStrings();
            
            int nbPlayed = 0;
            for(int i = 0; i < playedStrings.Count; i++)
            {
                if(playedStrings.ElementAtOrDefault(i).IsPlayed)
                {
                    nbPlayed++;
                }
            }

            switch (nbPlayed)
            {
                case 0:
                    updateDisplay(false, false);
                    break;
                case 1:
                    updateDisplay(true, false);
                    updateChromatic(playedStrings, errors);
                    break;
                default:
                    updateDisplay(false, true);
                    updatePolyphonic(playedStrings, errors);
                    break;
            }
        }

        private void updateChromatic(List<GuitarString> playedStrings, double[] errors)
        {
            // Trouver l'unique corde jouée
            int i = 0;
            bool found = false;
            while( i < errors.Length && !found)
            {
                found = playedStrings.ElementAtOrDefault(i).IsPlayed;
                i++;
            }

            chromatic.TunerValue = errors[i];
        }

        private void updatePolyphonic(List<GuitarString> playedStrings, double[] errors)
        {
            polyphonic.TunerValues = new List<double>(errors);
        }
    }
}
