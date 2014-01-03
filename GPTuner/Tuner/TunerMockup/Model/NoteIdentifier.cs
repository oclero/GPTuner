using System;
using System.Collections.Generic;
using System.Linq;
using GPTunerWrapper;

namespace Tuner.TunerMockup.Model
{
    /// <summary>
    /// Classe "mockée" (ne réalise pas véritablement l'algorithme)
    /// </summary>
    public class NoteIdentifier
    {
        public static double[] STANDARD_TUNING = { 40, 45, 50, 55, 59, 64 };
        private List<double> targetedTuning;
        private TunerWrapper tunerWrapper;

        public NoteIdentifier()
        {
            tunerWrapper = new TunerWrapper();
            targetedTuning = new List<double>(STANDARD_TUNING);
        }

        public NoteIdentifier(double[] tuning)
        {
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

            for (int i = 0; i < playedStrings.Count; i++)
            {
                if (playedStrings.ElementAt(i).IsPlayed)
                {
                    int guitarStringNum = playedStrings.ElementAt(i).Number - 1;

                    PlayedNote note = new PlayedNote();
                    note.TargetedNote = targetedTuning.ElementAt(guitarStringNum);
                    note.GuitarStringNumber = guitarStringNum;

                    // Avec la dll C++/CLI
                    double error = tunerWrapper.getError(guitarStringNum);
                    note.ActualNote = note.TargetedNote + error;

                    // Ajout de la note
                    notes.Add(note);
                }
            }

            signal.Notes = notes;
            return signal;
        }
    }
}
