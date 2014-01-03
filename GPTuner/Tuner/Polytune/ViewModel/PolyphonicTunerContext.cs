using Tuner.Polytune.Model;
using Tuner.TunerMockup.Model;
using System.Collections.Generic;
using System.Linq;

namespace Tuner.Polytune.ViewModel
{
    /// <summary>
    /// ViewModel pour un DelGrid devant afficher un accordeur chromatique
    /// </summary>
    public class PolyphonicTunerContext : TunerContext
    {
        public PolyphonicTunerContext()
            : base()
        {
            activeColumnsIndexes = new List<int>();
            inactiveColumnsIndexes = new List<int>();
            NbSteps = ScreenHeight;
        }

        private List<PlayedNote> notes;
        /// <summary>
        /// Liste des notes jouées
        /// </summary>
        public List<PlayedNote> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                updateDelsList();
            }
        }

        protected override void updateDelsList()
        {
            // Attention : NbStrings ne doit pas être nul
            if (NbStrings != 0)
            {
                // Ensemble des colonnes de Dels
                List<DelModel>[] delsColumns = new List<DelModel>[ScreenWidth];

                // Calcul des indices
                int nbEmptyColumns = ScreenWidth % NbStrings;
                if (nbEmptyColumns == NbStrings - 1)
                {
                    // Cas ideal : nbEmptyColumns = NbStrings - 1
                    nbColumnsByString = ScreenWidth / NbStrings;
                    for (int i = 0; i < NbStrings; i++)
                    {
                        int currentIndex = i * (nbColumnsByString + 1);
                        activeColumnsIndexes.Add(currentIndex);
                        if (i != NbStrings - 1)
                        {
                            inactiveColumnsIndexes.Add(nbColumnsByString + currentIndex);
                        }
                    }
                }
                else
                {
                    // Sinon
                    nbColumnsByString = 1;
                    int k = 0;
                    while (k < NbStrings)
                    {
                        activeColumnsIndexes.Add(k);
                        k++;
                    }
                    while (k < ScreenWidth)
                    {
                        inactiveColumnsIndexes.Add(k);
                        k++;
                    }
                }

                // Colonnes vides
                foreach (int index in inactiveColumnsIndexes)
                {
                    delsColumns[index] = DelColumnFactory.newEmptyColumn(ScreenHeight);
                }

                // Colonnes actives (même nombre que les cordes)
                int middleIndex = GetIndexForMiddleValue();

                for(int i = 0; i < NbStrings; i ++)
                {
                    int beginIndex = activeColumnsIndexes.ElementAt(i);
                    // Si la corde est jouée
                    if (isStringPlayed(i))
                    {
                        int idInNotes = getIndexForPlayedString(i);
                        PlayedNote note = Notes.ElementAt(idInNotes);
                        double value = note.ActualNote - note.TargetedNote;
                        int valueIndex = GetIndexForValue(value);

                        // Plusieurs colonne par corde
                        for (int j = 0; j < nbColumnsByString; j++)
                        {
                            delsColumns[beginIndex + j] = DelColumnFactory.newPolyphonicColumn(valueIndex, middleIndex, ScreenHeight);
                        }
                    }
                    else
                    {
                        // Plusieurs colonne par corde
                        for (int j = 0; j < nbColumnsByString; j++)
                        {
                            delsColumns[beginIndex + j] = DelColumnFactory.newEmptyColumn(ScreenHeight);
                        }
                    }
                }

                // Mise à jour de la liste de Dels
                DelsList.Clear();
                foreach (List<DelModel> lst in delsColumns)
                {
                    DelsList.Add(lst);
                }
            }
        }

        #region Private methods

        /// <summary>
        /// Indique si la corde de numéro stringNum est jouée
        /// </summary>
        /// <param name="stringNum"></param>
        /// <returns></returns>
        private bool isStringPlayed(int stringNum)
        {
            foreach (PlayedNote note in Notes)
            {
                if (note.GuitarStringNumber == stringNum)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Retourne l'index de la corde de numéro stringNum
        /// </summary>
        /// <param name="stringNum"></param>
        /// <returns></returns>
        private int getIndexForPlayedString(int stringNum)
        {
            for (int i = 0; i < Notes.Count; i++)
            {
                if (Notes[i].GuitarStringNumber == stringNum)
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion

        #region Private vars

        /// <summary>
        /// Liste pour savoir quels VUMeters correspondent à quelle corde (attention au nombre de VUMeters par corde)
        /// </summary>
        private List<int> activeColumnsIndexes;

        /// <summary>
        /// Liste Pour savoir quels VUMeters sont inactifs
        /// </summary>
        private List<int> inactiveColumnsIndexes;

        /// <summary>
        /// Nombre de VUMeters par corde
        /// </summary>
        int nbColumnsByString;

        #endregion
    }
}
