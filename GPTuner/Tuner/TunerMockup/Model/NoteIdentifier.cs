using System;
using System.Collections.Generic;
using System.Linq;

namespace Tuner.TunerMockup.Model
{
    /// <summary>
    /// Classe "mockée" (ne réalise pas véritablement l'algorithme)
    /// </summary>
    public class NoteIdentifier
    {
        public static double[] STANDARD_TUNING = { 40, 45, 50, 55, 59, 64 };
        private Random random;
        private List<double> targetedTuning;

        public NoteIdentifier()
        {
            random = new Random();
            targetedTuning = new List<double>(STANDARD_TUNING);
        }

        public NoteIdentifier(double[] tuning)
        {
            random = new Random();
            targetedTuning = new List<double>(tuning);
        }

        /// <summary>
        /// Methode "mockée"
        /// </summary>
        /// <param name="playedStrings"></param>
        /// <returns></returns>
        public ProcessedSignal getProcessedSignal(List<GuitarString> playedStrings)
        {
            ProcessedSignal signal = new ProcessedSignal();
            List<PlayedNote> notes = new List<PlayedNote>();
            double maximum = 0.5;
            double minimum = -0.5;

            for (int i = 0; i < playedStrings.Count; i++)
            {
                if (playedStrings.ElementAt(i).IsPlayed)
                {
                    // si la corde est jouée ons genere une erreur aleatoire
                    double rand = random.NextDouble() * (maximum - minimum) + minimum;
                    PlayedNote note = new PlayedNote();
                    int guitarStringNum = playedStrings.ElementAt(i).Number - 1;
                    note.TargetedNote = targetedTuning.ElementAt(guitarStringNum);
                    note.ActualNote = note.TargetedNote + rand;
                    note.GuitarStringNumber = guitarStringNum;
                    notes.Add(note);
                }
            }
            signal.Notes = notes;
            return signal;
        }
    }
}
