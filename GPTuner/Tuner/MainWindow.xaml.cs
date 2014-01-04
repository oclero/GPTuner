using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using Tuner.TunerMockup.Model;

namespace Tuner
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int DEFAULT_NB_STRINGS = 6;

        public List<GuitarString> PlayedStrings {get; set;}

        public MainWindow()
        {
            InitializeComponent();

            // Pour simplifier, cette classe sera son propre Datacontext
            DataContext = this;

            // Remplir la liste des cordes jouees (nombre de cordes "hardcoded" à 6)
            PlayedStrings = new List<GuitarString>();
            for (int i = 0; i < DEFAULT_NB_STRINGS; i++)
            {
                PlayedStrings.Add(new GuitarString { Number = i + 1, IsPlayed = false });
            }

            // Commandes
            CreatePlayCommand();
        }

        #region Command : Jouer les cordes
        public ICommand PlayStringsCommand
        {
            get;
            internal set;
        }

        /// <summary>
        /// Possible uniquement si au moins 1 corde est jouée
        /// </summary>
        private bool CanExecutePlayStringsCommand()
        {
            foreach (GuitarString guitarString in PlayedStrings)
            {
                if (guitarString.IsPlayed) { return true; }
            }
            return false;
        }

        private static NoteIdentifier identifier = new NoteIdentifier();

        /// <summary>
        /// "Joue" les cordes et declenche l'affichage en consequence
        /// </summary>
        public void PlayStringsExecute()
        {
            PolytuneView.Signal = identifier.getProcessedSignal(PlayedStrings);
        }

        private void CreatePlayCommand()
        {
            PlayStringsCommand = new RelayCommand(PlayStringsExecute, CanExecutePlayStringsCommand);
        }

        #endregion

    }

}
