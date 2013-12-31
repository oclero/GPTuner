using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPTunerWrapper;

namespace Tuner.Model
{
    class TunerModel
    {
        #region Members
        //private TunerWrapper tuner;
        // Provisoire
        private Random random = new Random();
        #endregion

        #region Constructeur
        public TunerModel()
        {
            // TODO corriger exception
            //tuner = new TunerWrapper();
        }
        #endregion

        #region Methods
        public double GetError(int guitarString)
        {
            // TODO corriger exception
            //return (double) tuner.getError(guitarString);

            // Provisoire :
            double maximum = 0.5;
            double minimum = -0.5;
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public double[] GetErrorsForAllStrings()
        {
            double[] errors = new double[6];
            for (int i = 0; i < 6; i++)
            {
                errors[i] = GetError(i);
            }
            return errors;
        }
        #endregion
    }
}
