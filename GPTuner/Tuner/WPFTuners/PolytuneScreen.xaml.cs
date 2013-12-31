using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Tuner.Dels;
using Tuner.FrameworkMVVM;
using Tuner.Model;

namespace Tuner.WPFTuners
{
    /// <summary>
    /// Logique d'interaction pour PolytuneScreen.xaml
    /// </summary>
    public abstract partial class PolytuneScreen : BaseUserControl
    {
        private static int DEFAULT_NB_DELS_HEIGHT = 5;
        private static int DEFAULT_NB_DELS_WIDTH = 17;
        public static double DEFAULT_THRESHOLD = 0.5;

        private BindingList<VUMeter> vuMeters;
        public BindingList<VUMeter> VUMeters
        {
            get { return vuMeters; }
            set
            {
                NotifyPropertyChanged(ref vuMeters, value);
            }
        }

        protected VUMeterModel model;

        public PolytuneScreen()
        {
            InitializeComponent();
            VUMeters = new BindingList<VUMeter>();
        }

        protected void applyTransformationOnVUMeter(VUMeter meter, int index)
        {
            // TODO Corriger translation

            //Calculer la position de chaque colonne de Dels
            double StartAngle = -20;
            double Endangle = 40;
            double AngleTotal = Endangle - StartAngle;
            double AngleBetweenEach = AngleTotal / NbDelsWidth;
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

        #region Public vars

        public int NbDelsHeight
        {
            get { return (int)GetValue(NbDelsHeightProperty); }
            set { SetValue(NbDelsHeightProperty, value); }
        }

        public int NbDelsWidth
        {
            get { return (int)GetValue(NbDelsWidthProperty); }
            set { SetValue(NbDelsWidthProperty, value); }
        }

        public double Threshold
        {
            get { return (double)GetValue(ThresholdProperty); }
            set { SetValue(ThresholdProperty, value); }
        }

        #endregion

        #region Dependency properties

        public static DependencyProperty NbDelsHeightProperty =
    DependencyProperty.Register("NbDelsHeight", typeof(int), typeof(PolytuneScreen), new PropertyMetadata(DEFAULT_NB_DELS_HEIGHT, OnScreenTemplateChanged));

        public static DependencyProperty NbDelsWidthProperty =
            DependencyProperty.Register("NbDelsWidth", typeof(int), typeof(PolytuneScreen), new PropertyMetadata(DEFAULT_NB_DELS_WIDTH, OnScreenTemplateChanged));

        public static DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(PolytuneScreen), new PropertyMetadata(DEFAULT_THRESHOLD, OnThresholdChanged));

        protected static void OnScreenTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolytuneScreen tuner = ((d as UserControl).DataContext) as PolytuneScreen;
            tuner.UpdateScreen();
        }

        protected static void OnThresholdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolytuneScreen tuner = ((d as UserControl).DataContext) as PolytuneScreen;
            tuner.UpdateThreshold();
        }

        #endregion

        #region A implementer par les classes filles

        protected abstract void UpdateScreen();
        protected abstract void UpdateThreshold();

        #endregion
    }
}
