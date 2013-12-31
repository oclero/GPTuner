using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Model
{
    public class VUMeterModel
    {
        private List<double> steps;
        private double min;
        private double max;

        public int NbSteps
        {
            get { return steps.Count; }
            set
            {
                updateModel(value, min, max);
            }
        }

        public double Minimum
        {
            get { return min; }
            set
            {
                updateModel(steps.Count, value, max);
            }
        }

        public double Maximum
        {
            get { return max; }
            set
            {
                updateModel(steps.Count, min, value);
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nbSteps"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public VUMeterModel(int nbSteps, double minimum, double maximum)
        {
            steps = new List<double>();
            updateModel(nbSteps, minimum, maximum);
        }

        #region Private Methods

        private void updateModel(int nbSteps, double minimum, double maximum)
        {
            min = minimum;
            max = maximum;
            updateSteps(nbSteps);
        }

        private void updateSteps(int nbSteps)
        {
            double range = max - min;
            double stepValue = range / (nbSteps - 1);

            steps.Clear();
            for (int i = 0; i < nbSteps; i++)
            {
                double delStep = max - i * stepValue;
                steps.Add(delStep);
            }
        }
        #endregion

        #region Public methods

        public int GetIndexForValue(double value)
        {
            double closest = steps.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
            return steps.IndexOf(closest);
        }

        public int GetIndexForMiddleValue()
        {
            double value = (max + min) / 2;
            double closest = steps.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
            return steps.IndexOf(closest);
        }

        public void Update(int nbSteps, double minimum, double maximum)
        {
            updateModel(nbSteps, minimum, maximum);
        }

        #endregion
    }
}
