using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tuner.Dels;
using Tuner.Model;

namespace Tuner.WPFTuners
{
    /// <summary>
    /// Vue de l'Accordeur chromatique
    /// </summary>
    public class ChromaticTuner : PolytuneScreen
    {
        #region Static vars

        private static double DEFAULT_TUNER_VALUE = 0.0;

        #endregion

        #region Constructor

        public ChromaticTuner()
        {
            InitializeComponent();
            model = new VUMeterModel(NbDelsWidth, DEFAULT_THRESHOLD, -DEFAULT_THRESHOLD);
            DataContext = this;
            currentIndex = 0;
            updateVUMeters();
        }

        #endregion

        #region Implementation de la classe mère

        protected override void UpdateScreen()
        {
            model.Update(NbDelsWidth, Threshold, -Threshold);
            updateVUMeters();
        }

        protected override void UpdateThreshold()
        {
            model.Update(NbDelsWidth, Threshold, -Threshold);
        }

        #endregion

        #region Private vars

        private int currentIndex;

        #endregion

        #region Private methods

        private void updateVUMeters()
        {
            VUMeters.Clear();
            for (int i = 0; i < NbDelsWidth; i++)
            {
                VUMeter meter = new VUMeter();
                meter.NbDels = NbDelsHeight;
                //applyTransformationOnVUMeter(meter, i);
                VUMeters.Add(meter);
            }

            updateMiddle();
            updateValue();
        }

        private void updateMiddle()
        {
            int middleIndex = model.GetIndexForMiddleValue();
            VUMeters.ElementAtOrDefault(middleIndex).SetAllDels(true, 1);
        }

        private void updateValue()
        {
            if (VUMeters.Count != 0)
            {
                int newCurrentIndex = model.GetIndexForValue(TunerValue);
                int middleIndex = model.GetIndexForMiddleValue();

                // Effacer la précédente valeur
                VUMeters.ElementAtOrDefault(currentIndex).SetAllDels(false, 0);

                // Mettre la nouvelle valeur
                if (newCurrentIndex == middleIndex)
                {
                    // En vert car bonne valeur
                    VUMeters.ElementAtOrDefault(middleIndex).SetAllDels(true, 1);
                }
                else
                {
                    // En rouge sinon
                    VUMeters.ElementAtOrDefault(newCurrentIndex).SetAllDels(true, 0);
                }
                currentIndex = newCurrentIndex;
            }
        }

        #endregion

        #region Public vars

        public double TunerValue
        {
            get { return (double)GetValue(TunerValueProperty); }
            set { SetValue(TunerValueProperty, value); }
        }

        #endregion

        #region Dependency properties

        public static DependencyProperty TunerValueProperty =
            DependencyProperty.Register("TunerValue", typeof(double), typeof(ChromaticTuner), new PropertyMetadata(DEFAULT_TUNER_VALUE, OnTunerValueChanged));

        private static void OnTunerValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChromaticTuner tuner = ((d as UserControl).DataContext) as ChromaticTuner;
            tuner.updateValue();
            tuner.updateMiddle();
        }

        #endregion

    }
}
