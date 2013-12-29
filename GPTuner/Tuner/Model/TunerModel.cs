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
        private TunerWrapper Tuner;
        #endregion

        #region Constructeur
        public TunerModel()
        {
            Tuner = new TunerWrapper();
        }
        #endregion

        #region Methods
        public double GetError(int guitarString)
        {
            return (double) Tuner.getError(guitarString);
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
