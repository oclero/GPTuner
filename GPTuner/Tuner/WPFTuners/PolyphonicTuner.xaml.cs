using Tuner.Dels;
using Tuner.FrameworkMVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tuner.WPFTuners
{
    /// <summary>
    /// Logique d'interaction pour PolyphonicTuner.xaml
    /// </summary>
    public partial class PolyphonicTuner : BaseUserControl
    {

        private BindingList<VUMeter> vuMeters;
        public BindingList<VUMeter> VUMeters
        {
            get { return vuMeters; }
            set
            {
                NotifyPropertyChanged(ref vuMeters, value);
            }
        }

        public static int DEFAULT_NB_LEVELS = 5;
        public static double DEFAULT_THRESHOLD = 0.5;
        public static int DEFAULT_NB_STRINGS = 6;
        public static int DEFAULT_NB_VUMETERS_BY_STRING = 2;
        public static double[] DEFAULT_VALUES = null;

        public PolyphonicTuner()
        {
            InitializeComponent();
            VUMeters = new BindingList<VUMeter>();
            ReDrawVUMeters();
        }

        #region Private methods

        private void DrawVUMeters()
        {
            // On boucle pour former le range de l'angle

            for (int i = 0; i < Strings; i++)
            {
                // On ajoute un nouveau VUMeter dans la liste
                VUMeter meter = new VUMeter();
                meter.NbDels = Levels;
                //applyTransformationOnVUMeter(meter, i);
                vuMeters.Add(meter);
            }
        }

        private void applyTransformationOnVUMeter(VUMeter meter, int index)
        {
            // TODO Corriger translation

            //Calculer la position de chaque colonne de Dels
            double StartAngle = -20;
            double Endangle = 40;
            double AngleTotal = Endangle - StartAngle;
            double AngleBetweenEach = AngleTotal / Strings;
            double CircleRadius = 10; // TODO

            // On lui applique les transformations nécessaires
            // Preparer le point pivot
            Point p = new Point(0.5, 0.5);
            meter.RenderTransformOrigin = p;
            meter.HorizontalAlignment = HorizontalAlignment.Center;
            meter.VerticalAlignment = VerticalAlignment.Center;

            // Preparer la transformation
            TransformGroup transformation = new TransformGroup();

            // 1 . Calcul de l'angle de rotation
            RotateTransform rotation = new RotateTransform();
            double angle = StartAngle + index * AngleBetweenEach;
            double angle_radians = angle * (Math.PI / 180);
            rotation.Angle = angle;
            transformation.Children.Add(rotation);

            // 2 . Calcul de la translation (dépendant de l'angle)
            TranslateTransform translation = new TranslateTransform();
            translation.X = (int)((CircleRadius) * Math.Cos(angle_radians));
            translation.Y = (int)((CircleRadius) * Math.Sin(angle_radians));
            transformation.Children.Add(translation);

            // Appliquer la transformation
            meter.RenderTransform = transformation;
        }

        private void ReDrawVUMeters()
        {
            vuMeters.Clear();
            DrawVUMeters();
        }

        #endregion

        #region Public methods

        public void setVUMeterValue(int index, double value)
        {
            VUMeter meter = VUMeters.ElementAtOrDefault(index);
            if (meter != null)
            {
                meter.VuMeterValue = value;
            }
        }

        public void setVUMeterValues(double[] values)
        {
            if (values.Length == Strings)
            {
                for (int i = 0; i < Strings; i++)
                {
                    setVUMeterValue(i, values[i]);
                }
            }
        }

        #endregion

        #region public vars
        public double Threshold
        {
            get { return (double)GetValue(ThresholdProperty); }
            set { SetValue(ThresholdProperty, value); }
        }

        public int Levels
        {
            get { return (int)GetValue(LevelsProperty); }
            set { SetValue(LevelsProperty, value); }
        }

        public int Strings
        {
            get { return (int)GetValue(StringsProperty); }
            set { SetValue(StringsProperty, value); }
        }

        public int VUMetersByString
        {
            get { return (int)GetValue(VUMetersByStringProperty); }
            set { SetValue(VUMetersByStringProperty, value); }
        }

        public double[] Values
        {
            get { return (double[])GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        #endregion

        #region Dependency properties

        public static DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(PolyphonicTuner), new PropertyMetadata(DEFAULT_THRESHOLD, OnTemplateChanged));

        public static DependencyProperty LevelsProperty =
            DependencyProperty.Register("Levels", typeof(int), typeof(PolyphonicTuner), new PropertyMetadata(DEFAULT_NB_LEVELS, OnTemplateChanged));

        public static DependencyProperty StringsProperty =
            DependencyProperty.Register("Strings", typeof(int), typeof(PolyphonicTuner), new PropertyMetadata(DEFAULT_NB_STRINGS, OnTemplateChanged));

        public static DependencyProperty VUMetersByStringProperty =
            DependencyProperty.Register("VUMetersByString", typeof(int), typeof(PolyphonicTuner), new PropertyMetadata(DEFAULT_NB_VUMETERS_BY_STRING, OnTemplateChanged));

        public static DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(double[]), typeof(PolyphonicTuner), new PropertyMetadata(DEFAULT_VALUES, OnValuesChanged));

        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolyphonicTuner tuner = ((d as UserControl).DataContext) as PolyphonicTuner;
            tuner.ReDrawVUMeters();
        }

        private static void OnValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolyphonicTuner tuner = ((d as UserControl).DataContext) as PolyphonicTuner;
            double[] tab = (double[]) e.NewValue;
            tuner.setVUMeterValues(tab);
        }

        #endregion

    }
}
