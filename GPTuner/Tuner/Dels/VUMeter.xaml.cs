using Tuner.FrameworkMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tuner.Dels;

namespace Tuner.Dels
{
    /// <summary>
    /// Logique d'interaction pour DelColumnView.xaml
    /// </summary>
    public partial class VUMeter : BaseUserControl
    {
        #region Dels

        public static int DEFAULT_NB_DELS = 5;
        public static double DEFAULT_MIN = -0.5;
        public static double DEFAULT_MAX = 0.5;
        public static double DEFAULT_VUMETER_VALUE = 0;

        private ObservableCollection<Del> dels;
        public ObservableCollection<Del> Dels
        {
            get { return dels; }
            set
            {
                NotifyPropertyChanged(ref dels, value);
            }
        }

        public void setDelOn(int index, bool on)
        {
            Del del = dels.ElementAtOrDefault(index);
            if (del != null)
            {
                del.On = on;
            }
        }

        public void setDelLevel(int index, int level)
        {
            Del del = dels.ElementAtOrDefault(index);
            if (del != null)
            {
                del.Level = level;
            }
        }

        public void addDel(Del del)
        {
            dels.Add(del);
        }

        public void updateMiddleDel()
        {
            if (Dels.Count != 0)
            {
                int middleDelIndex = (Dels.Count - 1) / 2;
                setDelOn(middleDelIndex, true);
                setDelLevel(middleDelIndex, 1);
                Console.WriteLine("Milieu : indice = " + middleDelIndex);
            }
        }

        public void setActiveDel(int index)
        {
            if (Dels.Count != 0)
            {
                // D'abord tout eteindre
                int middleDelIndex = (Dels.Count - 1) / 2;
                for (int i = 0; i < dels.Count; i++)
                {
                    dels.ElementAtOrDefault(i).On = false;
                }
                // Puis allumer la bonne
                Del del = dels.ElementAtOrDefault(index);
                if (del != null)
                {
                    del.On = true;
                    del.Level = 0;
                }
            }

        }

        public void updateActiveDel(double value)
        {
            if (Dels.Count != 0)
            {
                double closest = steps.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
                int index = steps.IndexOf(closest);
                setActiveDel(index);
                updateMiddleDel();
            }
            Console.WriteLine("min =" + MinValue);
            Console.WriteLine("max =" + MaxValue);
        }

        public void updateAllDels()
        {
            if (NbDels != 0)
            {
                // On vide tout
                Dels.Clear();
                for (int i = 0; i < NbDels; i++)
                {
                    Del del = new Del();
                    del.On = false;
                    del.Level = 0;
                    addDel(del);
                }

                // On recalcule tout en fonction du nouveau nombre de dels
                computeVUMeterValues();
                initializeVUMeter();
            }
        }

        #endregion

        #region Constructeur

        public VUMeter()
        {
            InitializeComponent();
            Dels = new ObservableCollection<Del>();
            steps = new List<double>();
            updateAllDels();
        }

        #endregion

        #region Private vars

        private double stepValue;
        private double range;
        private double middle;
        private List<double> steps;

        public double getStepValue() { return stepValue; }
        public double getRange() { return range; }
        public double getMiddle() { return middle; }
        public List<double> getSteps() { return steps; }

        #endregion

        #region Public vars

        public double VuMeterValue
        {
            get { return (double)GetValue(VuMeterValueProperty); }
            set { SetValue(VuMeterValueProperty, value); }
        }

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public int NbDels
        {
            get { return (int)GetValue(NbDelsProperty); }
            set { SetValue(NbDelsProperty, value); }
        }

        #endregion

        #region Dependecy properties

        public static DependencyProperty VuMeterValueProperty =
            DependencyProperty.Register("VuMeterValue", typeof(double), typeof(VUMeter), new PropertyMetadata(DEFAULT_VUMETER_VALUE, OnValueChanged));

        public static DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(VUMeter), new PropertyMetadata(DEFAULT_MAX, OnMaxValueChanged));

        public static DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(VUMeter), new PropertyMetadata(DEFAULT_MIN, OnMinValueChanged));

        public static DependencyProperty NbDelsProperty =
            DependencyProperty.Register("NbDels", typeof(int), typeof(VUMeter), new PropertyMetadata(DEFAULT_NB_DELS, OnNbDelsChanged));

        /// <summary>
        /// Quand la valeur max change
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO recalculer la position de la valeur zéro
            // TODO recalculer position de value
            VUMeter meter = ((d as UserControl).DataContext) as VUMeter;
            meter.MaxValue = (double)e.NewValue;
            meter.computeVUMeterValues();
            meter.updateActiveDel(meter.VuMeterValue);

        }

        /// <summary>
        /// Quand la valeur min change
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO recalculer la position de la valeur zéro
            // TODO recalculer position de value
            VUMeter meter = ((d as UserControl).DataContext) as VUMeter;
            meter.MinValue = (double)e.NewValue;
            meter.computeVUMeterValues();
            meter.updateActiveDel(meter.VuMeterValue);
        }

        /// <summary>
        /// Quand la valeur change
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VUMeter meter = ((d as UserControl).DataContext) as VUMeter;
            double v = (double)e.NewValue;
            meter.updateActiveDel(v);
        }

        /// <summary>
        /// Quand le nombre de dels change
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnNbDelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VUMeter meter = ((d as UserControl).DataContext) as VUMeter;
            meter.updateAllDels();
            meter.updateActiveDel(meter.VuMeterValue);
        }

        #endregion

        #region Private methods

        private void computeVUMeterValues()
        {
            if (Dels.Count != 0)
            {
                range = MaxValue - MinValue;
                middle = (MaxValue + MinValue) / 2;
                stepValue = range / (Dels.Count - 1);
                // On cree la liste de valeurs
                steps.Clear();
                for (int i = 0; i < Dels.Count; i++)
                {
                    double delStep = MaxValue - i * stepValue;
                    steps.Add(delStep);
                }
                for (int i = 0; i < steps.Count; i++)
                {
                    Console.WriteLine(" [ " + i + " ] = " + steps.ElementAt(i));

                }
                Console.WriteLine(" ");
            }
        }

        private void initializeVUMeter()
        {
            updateMiddleDel();
        }

        #endregion

    }
}
