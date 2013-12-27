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
        public float GetError(int guitarString)
        {
            return Tuner.getError(guitarString);
        }

        public float[] GetErrorsForAllStrings()
        {
            float[] errors = new float[6];
            for (int i = 0; i < 6; i++)
            {
                errors[i] = GetError(i);
            }
            return errors;
        }
        #endregion
    }
}
