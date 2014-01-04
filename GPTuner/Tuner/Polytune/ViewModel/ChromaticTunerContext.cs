using Tuner.Polytune.Model;
using Tuner.TunerMockup.Model;
using System.Collections.Generic;

namespace Tuner.Polytune.ViewModel
{
    /// <summary>
    /// ViewModel pour un DelGrid devant afficher un accordeur chromatique
    /// </summary>
    public class ChromaticTunerContext : TunerContext
    {
        public ChromaticTunerContext()
            : base()
        {
            NbSteps = ScreenWidth;
        }

        private PlayedNote note;
        /// <summary>
        /// Note jouée
        /// </summary>
        public PlayedNote Note
        {
            get { return note; }
            set
            {
                note = value;
                updateDelsList();
            }
        }

        protected override void updateDelsList()
        {
            // Ensemble des colonnes de Dels
            List<DelModel>[] delsColumns = new List<DelModel>[ScreenWidth];

            // Eteindre les colonnes
            for (int i = 0; i < ScreenWidth; i++)
            {
                delsColumns[i] = DelColumnFactory.newEmptyColumn(ScreenHeight);
            }

            // Colonne de la note jouée et du milieu
            double value = Note.ActualNote - Note.TargetedNote;
            int indexMiddle = GetIndexForMiddleValue();
            int indexValue = GetIndexForValue(value);

            if (indexMiddle != indexValue)
            {
                delsColumns[indexMiddle] = DelColumnFactory.newColumn(ScreenHeight, true, 2);
                delsColumns[indexValue] = DelColumnFactory.newColumn(ScreenHeight, true, 1);
            }
            else
            {
                delsColumns[indexMiddle] = DelColumnFactory.newColumn(ScreenHeight, true, 1);
            }
            DelsList.Clear();
            foreach(List<DelModel> lst in delsColumns)
            {
                DelsList.Add(lst);
            }
        }

        protected override void OnNbStringsChanged()
        {
            // Rien
        }

        protected override void OnScreenHeightChanged()
        {
            updateDelsList();
        }

        protected override void OnScreenWidthChanged()
        {
            NbSteps = ScreenWidth;
            updateDelsList();
        }
    }
}
