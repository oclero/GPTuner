using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Tuner.Model;

namespace Tuner
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int DEFAULT_NB_STRINGS = 6;

        #region Strings
        private List<GuitarString> playedStrings;
        public List<GuitarString> PlayedStrings
        {
            get { return playedStrings; }
            //set { NotifyPropertyChanged(ref playedStrings, value); }
            set { playedStrings = value; }
        }
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            // Pour simplifier, cette classe sera le Datacontexte
            DataContext = this;
            // Remplir la liste des cordes jouees (nombre de cordes "hardcoded" à 6)
            playedStrings = new List<GuitarString>();
            for (int i = 0; i < DEFAULT_NB_STRINGS; i++)
            {
                playedStrings.Add(new GuitarString { Number = i + 1, IsPlayed = false });
            }
            // Commandes
            CreatePlayCommand();
        }
        #endregion

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
            foreach (GuitarString guitarString in playedStrings)
            {
                if (guitarString.IsPlayed) { return true; }
            }
            return false;
        }

        /// <summary>
        /// "Joue" les cordes et declenche l'affichage en consequence
        /// </summary>
        public void PlayStringsExecute()
        {
            polytune.displayErrors(playedStrings);
        }

        private void CreatePlayCommand()
        {
            PlayStringsCommand = new RelayCommand(PlayStringsExecute, CanExecutePlayStringsCommand);
        }

        #endregion

    }

}
