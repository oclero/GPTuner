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

namespace Tuner
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region playedStrings
        private bool[] playedStrings;
        public bool[] PlayedStrings
        {
            get { return playedStrings; }
            set { NotifyPropertyChanged(ref playedStrings, value); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            NotifyPropertyChanged(nomPropriete);
            return true;
        }
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            playedStrings = new bool[6]; // false by default
            CreateSaveCommand();
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
            foreach (bool value in playedStrings){
                if(value){ return true;}
            }
            return false;
        }

        public void PlayStringsExecute()
        {
            // TODO
        }

        private void CreateSaveCommand()
        {
            PlayStringsCommand = new RelayCommand(PlayStringsExecute, CanExecutePlayStringsCommand);
        }


        #endregion

    }

}
