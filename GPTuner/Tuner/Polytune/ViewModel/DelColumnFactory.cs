using Tuner.Polytune.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Polytune.ViewModel
{
    /// <summary>
    /// Factory aidant à la création de Listes de DelModels
    /// </summary>
    public class DelColumnFactory
    {
        /// <summary>
        /// Retourne une liste de height DelModels éteints
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static List<DelModel> newEmptyColumn(int height)
        {
            List<DelModel> result = new List<DelModel>();
            for (int i = 0; i < height; i++)
            {
                result.Add(new DelModel { On = false, Level = Del.LEVEL_0 });
            }
            return result;
        }

        /// <summary>
        /// Retourne une liste de DelModels tous identiques
        /// </summary>
        /// <param name="height">Longueur de la liste</param>
        /// <param name="ons">Etat de toutes les Dels</param>
        /// <param name="levels">Niveau de toutes les Dels</param>
        /// <returns></returns>
        public static List<DelModel> newColumn(int height, bool on, int level)
        {
            List<DelModel> result = new List<DelModel>();
            for (int i = 0; i < height; i++)
            {
                result.Add(new DelModel { On = on, Level = level });
            }
            return result;
        }

        /// <summary>
        /// Retourne une liste de DelModels selon les valeurs des tableaux en paramètres
        /// </summary>
        /// <param name="ons">ons[i] est l'etat de la Del i</param>
        /// <param name="levels">levels[i] est le niveau de la Del i</param>
        /// <returns></returns>
        public static List<DelModel> newColumn(bool[] ons, int[] levels)
        {
            List<DelModel> result = new List<DelModel>();
            for (int i = 0; i < ons.Length; i++)
            {
                result.Add(new DelModel { On = ons[i], Level = levels[i] });
            }
            return result;
        }

        /// <summary>
        /// Retourne une liste de DelModels correspondant à une colonne de l'accordeur polyphonic
        /// </summary>
        /// <param name="valueIndex">Index de la valeur du VU-Metre</param>
        /// <param name="middleIndex">Index du curseur du milieu du VU-Metre</param>
        /// <param name="height">Hauteur</param>
        /// <returns></returns>
        public static List<DelModel> newPolyphonicColumn(int valueIndex, int middleIndex, int height)
        {
            List<DelModel> result = new List<DelModel>();
            for (int i = 0; i < height; i++)
            {
                result.Add(new DelModel { On = false, Level = 0 });
            }
            if (valueIndex != middleIndex)
            {
                result.ElementAt(valueIndex).On = true;
                result.ElementAt(valueIndex).Level = Del.LEVEL_WRONG;
                result.ElementAt(middleIndex).On = true;
                result.ElementAt(middleIndex).Level = Del.LEVEL_RIGHT;
            }
            else
            {
                result.ElementAt(middleIndex).On = true;
                result.ElementAt(middleIndex).Level = Del.LEVEL_RIGHT;
            }
            return result;
        }
    }
}
