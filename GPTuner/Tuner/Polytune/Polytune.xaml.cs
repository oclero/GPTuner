using Tuner.Polytune.ViewModel;
using Tuner.TunerMockup.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System;
using System.Windows.Threading;

namespace Tuner.Polytune
{
    /// <summary>
    /// Logique d'interaction pour Polytune.xaml
    /// </summary>
    public partial class Polytune : UserControl
    {
        /// <summary>
        /// Datacontext d'accordeur chromatic pour delGrid
        /// </summary>
        private ChromaticTunerContext chromatic;
        /// <summary>
        /// Datacontext d'accordeur polyphonic pour delGrid
        /// </summary>
        private PolyphonicTunerContext polyphonic;
        /// <summary>
        /// Datacontext pour afficher la lettre représentant la note jouée
        /// </summary>
        private NoteLetterContext letter;

        public Polytune()
        {
            chromatic = new ChromaticTunerContext();
            polyphonic = new PolyphonicTunerContext();
            letter = new NoteLetterContext();
            InitializeComponent();
            letterGrid.DataContext = letter;
        }

        public ProcessedSignal Signal
        {
            get { return (ProcessedSignal)GetValue(SignalProperty); }
            set { SetValue(SignalProperty, value); }
        }

        #region Dependency properties

        public static DependencyProperty SignalProperty = DependencyProperty.Register("Dels", typeof(ProcessedSignal), typeof(Polytune), new PropertyMetadata(OnSignalChanged));

        private static void OnSignalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Polytune polytune = d as Polytune;
            ProcessedSignal signal = (ProcessedSignal)e.NewValue;
            polytune.updateScreen(signal);
        }

        #endregion

        /// <summary>
        /// Actualise l'écran
        /// </summary>
        /// <param name="signal">Signal à afficher</param>
        public void updateScreen(ProcessedSignal signal)
        {
            Action<ProcessedSignal> actionUpdateScreen = new Action<ProcessedSignal>(delegateUpdateScreen);
            //delegateUpdateScreen(signal);
            this.Dispatcher.Invoke(actionUpdateScreen, new Object[] { signal });
        }

        public void delegateUpdateScreen(ProcessedSignal signal)
        {
            switch (signal.Notes.Count)
            {
                case 0:
                    delGrid.Dels = null;
                    letterGrid.Dels = null;
                    break;
                case 1:
                    chromatic.Note = signal.Notes.First();
                    delGrid.DataContext = chromatic;
                    letter.Note = signal.Notes.First();
                    break;
                default:
                    polyphonic.Notes = signal.Notes;
                    delGrid.DataContext = polyphonic;
                    letter.Note = null;
                    break;
            }
        }
    }
}
