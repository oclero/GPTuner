using Tuner.FrameworkMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tuner.Dels;

namespace Tuner.Dels
{
    /// <summary>
    /// Logique d'interaction pour DelColumnView.xaml
    /// </summary>
    public partial class VUMeter : BaseUserControl
    {

        public static int DEFAULT_NB_DELS = 5;

        private ObservableCollection<Del> dels;
        public ObservableCollection<Del> Dels
        {
            get { return dels; }
            set
            {
                NotifyPropertyChanged(ref dels, value);
            }
        }

        #region Public methods

        public void SetDelOn(int index, bool on)
        {
            Del del = dels.ElementAtOrDefault(index);
            if (del != null)
            {
                del.On = on;
            }
        }

        public void SetDelLevel(int index, int level)
        {
            Del del = dels.ElementAtOrDefault(index);
            if (del != null)
            {
                del.Level = level;
            }
        }

        public void SetDel(int index, bool on, int level)
        {
            Del del = dels.ElementAtOrDefault(index);
            if (del != null)
            {
                del.Level = level;
                del.On = on;
            }
        }

        public void SetAllDels(bool on, int level)
        {
            foreach (Del d in Dels)
            {
                d.On = on;
                d.Level = level;
            }
        }

        public void SetAllDels(bool on)
        {
            foreach (Del d in Dels)
            {
                d.On = on;
            }
        }

        public void SetAllDels(int level)
        {
            foreach (Del d in Dels)
            {
                d.Level = level;
            }
        }

        #endregion

        public VUMeter()
        {
            InitializeComponent();
            Dels = new ObservableCollection<Del>();
            UpdateDels();
        }

        private void UpdateDels()
        {
            for (int i = 0; i < NbDels; i++)
            {
                Dels.Add(new Del());
            }
        }

        #region Public vars

        public int NbDels
        {
            get { return (int)GetValue(NbDelsProperty); }
            set { SetValue(NbDelsProperty, value); }
        }

        #endregion

        #region Dependecy properties

        public static DependencyProperty NbDelsProperty =
            DependencyProperty.Register("NbDels", typeof(int), typeof(VUMeter), new PropertyMetadata(DEFAULT_NB_DELS, OnNbDelsChanged));

        private static void OnNbDelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VUMeter meter = ((d as UserControl).DataContext) as VUMeter;
            meter.UpdateDels();
        }

        #endregion

    }
}
