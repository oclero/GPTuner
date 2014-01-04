using Tuner.Polytune.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

namespace Tuner.Polytune.ViewModel
{
    /// <summary>
    /// Classe abstraite représentant un ViewModel du composant DelGrid
    /// </summary>
    public abstract class TunerContext : INotifyPropertyChanged
    {
        public static int DEFAULT_NB_STRINGS = 6;
        public static int DEFAULT_SCREEN_HEIGHT = 5;
        public static int DEFAULT_SCREEN_WIDTH = 17;
        public static double DEFAULT_THRESHOLD = 0.5;

        public ObservableCollection<List<DelModel>> DelsList { get; set; }

        private int nbStrings;
        public int NbStrings
        {
            get { return nbStrings; }
            set
            {
                nbStrings = value;
                OnNbStringsChanged();
                OnNotifyPropertyChanged("NbStrings");
            }
        }

        private int screenHeight;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set
            {
                screenHeight = value;
                OnScreenHeightChanged();
                OnNotifyPropertyChanged("ScreenHeight");
            }
        }

        private int screenWidth;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set
            {
                screenWidth = value;
                OnScreenWidthChanged();
                OnNotifyPropertyChanged("ScreenWidth");
            }
        }

        private double threshold;
        public double Threshold {
            get { return threshold; }
            set
            {
                threshold = value;
                updateSteps();
                OnNotifyPropertyChanged("Threshold");
            }
        }

        protected List<double> Steps { get; set; }

        protected DelColumnFactory Factory { get; set; }

        private int nbSteps;
        protected int NbSteps
        {
            get { return nbSteps; }
            set
            {
                nbSteps = value;
                updateSteps();
                OnNotifyPropertyChanged("NbSteps");
            }
        }

        public TunerContext()
        {
            nbStrings = DEFAULT_NB_STRINGS;
            screenHeight = DEFAULT_SCREEN_HEIGHT;
            screenWidth = DEFAULT_SCREEN_WIDTH;
            threshold = DEFAULT_THRESHOLD;
            Steps = new List<double>();
            DelsList = new ObservableCollection<List<DelModel>>();
        }

        /// <summary>
        /// Actualise les crans du VU-Mètre
        /// </summary>
        protected void updateSteps()
        {
            if (NbSteps != 0)
            {
                double max = Threshold;
                double min = -Threshold;
                double range = max - min;
                double stepValue = range / (NbSteps - 1);
                List<double> newSteps = new List<double>();
                for (int i = 0; i < NbSteps; i++)
                {
                    double delStep = max - i * stepValue;
                    newSteps.Add(delStep);
                }
                Steps = newSteps;
            }
        }

        /// <summary>
        /// Retourne l'index du curseur indiquant la valeur du VU-Mètre
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected int GetIndexForValue(double value)
        {
            double closest = Steps.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
            return Steps.IndexOf(closest);
        }

        /// <summary>
        /// Retourne l'index du curseur indiquant le milieu du VU-Mètre
        /// </summary>
        /// <returns></returns>
        protected int GetIndexForMiddleValue()
        {
            double max = Threshold;
            double min = -Threshold;
            double value = (max + min) / 2;
            double closest = Steps.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
            return Steps.IndexOf(closest);
        }

        /// <summary>
        /// Actualise la Collection de Liste de DelModel
        /// </summary>
        protected abstract void updateDelsList();

        protected abstract void OnNbStringsChanged();

        protected abstract void OnScreenHeightChanged();

        protected abstract void OnScreenWidthChanged();

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Members
    }
}
