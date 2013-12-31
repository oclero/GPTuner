using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tuner.Dels;
using Tuner.Model;

namespace Tuner.WPFTuners
{
    /// <summary>
    /// Vue de l'Accordeur polyphonique
    /// </summary>
    public class PolyphonicTuner : PolytuneScreen
    {

        #region Static vars

        private static int DEFAULT_NB_STRINGS = 6;

        #endregion

        #region Private vars

        // Pour savoir quels VUMeters correspondent à quelle corde (attention au nombre de VUMeters par corde)
        private List<int> activeVUMetersIndexes;

        // Pour savoir quels VUMeters sont inactifs
        private List<int> inactiveVUMetersIndexes;

        // Nombre de VUMeters par corde
        int nbVuMetersByString;

        #endregion

        #region Constructor

        public PolyphonicTuner()
            : base()
        {
            InitializeComponent();
            activeVUMetersIndexes = new List<int>();
            inactiveVUMetersIndexes = new List<int>();
            model = new VUMeterModel(NbDelsHeight, DEFAULT_THRESHOLD, -DEFAULT_THRESHOLD);
            DataContext = this;
            updateVUMeters();
        }

        #endregion

        #region Implémentation de la classe mère

        protected override void UpdateScreen()
        {
            model.Update(NbDelsHeight, Threshold, -Threshold);
            updateVUMeters();
        }

        protected override void UpdateThreshold()
        {
            model.Update(NbDelsHeight, Threshold, -Threshold);
        }

        #endregion

        #region Private methods

        private void updateVUMeters()
        {
            if (NbStrings != 0)
            {
                activeVUMetersIndexes.Clear();
                inactiveVUMetersIndexes.Clear();
                VUMeters.Clear();
                int nbEmptyVuMeters = NbDelsWidth % NbStrings;

                // Calcul des indices
                if (nbEmptyVuMeters == NbStrings - 1)
                {
                    // Cas ideal : nbEmptyVuMeters = NbStrings - 1
                    nbVuMetersByString = NbDelsWidth / NbStrings;
                    for (int i = 0; i < NbStrings; i++)
                    {
                        int currentIndex = i * (nbVuMetersByString + 1);
                        activeVUMetersIndexes.Add(currentIndex);
                        if (i != NbStrings - 1)
                        {
                            inactiveVUMetersIndexes.Add(nbVuMetersByString + currentIndex);
                        }
                    }
                }
                else
                {
                    // Sinon
                    nbVuMetersByString = 1;
                    int k = 0;
                    while (k < NbStrings)
                    {
                        activeVUMetersIndexes.Add(k);
                        k++;
                    }
                    while (k < NbDelsWidth)
                    {
                        inactiveVUMetersIndexes.Add(k);
                        k++;
                    }
                }

                // Ajout des VUMeters
                for (int i = 0; i < NbDelsWidth; i++)
                {
                    VUMeter meter = new VUMeter();
                    meter.NbDels = NbDelsHeight;
                    //applyTransformationOnVUMeter(meter, i);
                    VUMeters.Add(meter);
                }

                // Desactivation des inutiles
                foreach (int index in inactiveVUMetersIndexes)
                {
                    VUMeters.ElementAtOrDefault(index).SetAllDels(false);
                }

                // Activation des utiles
                updateMiddles();
                updateValues();
            }
        }

        private void updateMiddles()
        {
            foreach (int index in activeVUMetersIndexes)
            {
                updateDel(index, model.GetIndexForMiddleValue(), true, 1);
            }
        }

        private void updateValues()
        {
            if (VUMeters.Count != 0)
            {
                for (int i = 0; i < NbStrings; i++)
                {
                    updateValue(i, getValue(i));
                }
            }
        }

        private double getValue(int stringNumber)
        {
            return TunerValues.ElementAtOrDefault(stringNumber);
        }

        private void updateValue(int stringNumber, double value)
        {
            int delIndex = model.GetIndexForValue(value);
            int vuMeterIndex = activeVUMetersIndexes.ElementAtOrDefault(stringNumber);
            for (int i = 0; i < nbVuMetersByString; i++)
            {
                setActiveDel(vuMeterIndex + i, delIndex);
            }
        }

        private void updateDel(int VUMeterIndex, int delIndex, bool on, int level)
        {
            VUMeters.ElementAtOrDefault(VUMeterIndex).SetDel(delIndex, on, level);
        }

        private void setActiveDel(int VUMeterIndex, int delIndex)
        {
            int middleIndex = model.GetIndexForMiddleValue();
            for (int i = 0; i < NbDelsHeight; i++)
            {
                if (i != middleIndex)
                {
                    updateDel(VUMeterIndex, i, false, 0);
                }
            }
            int newLevel = 0;
            if (delIndex == middleIndex)
            {
                newLevel = 1;
            }
            updateDel(VUMeterIndex, delIndex, true, newLevel);
        }

        #endregion

        #region Public vars

        public List<double> TunerValues
        {
            get { return (List<double>)GetValue(TunerValuesProperty); }
            set { SetValue(TunerValuesProperty, value); }
        }

        public int NbStrings
        {
            get { return (int)GetValue(NbStringsProperty); }
            set { SetValue(NbStringsProperty, value); }
        }

        #endregion

        #region Dependency properties

        public static DependencyProperty TunerValuesProperty =
            DependencyProperty.Register("TunerValues", typeof(List<double>), typeof(PolyphonicTuner), new PropertyMetadata(new List<double>(), OnTunerValuesChanged));

        public static DependencyProperty NbStringsProperty =
            DependencyProperty.Register("NbStrings", typeof(int), typeof(PolyphonicTuner), new PropertyMetadata(DEFAULT_NB_STRINGS, OnNbStringsChanged));

        private static void OnNbStringsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolyphonicTuner tuner = ((d as UserControl).DataContext) as PolyphonicTuner;
            tuner.updateVUMeters();
        }

        private static void OnTunerValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolyphonicTuner tuner = ((d as UserControl).DataContext) as PolyphonicTuner;
            tuner.updateValues();
        }

        #endregion

    }
}
