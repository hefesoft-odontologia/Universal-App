using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.BusyBox
{
    public class Busy : ViewModelBase
    {
        public Busy()
        {
            
        }

        private bool isBusy;
                
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; RaisePropertyChanged("IsBusy"); }
        }

        private string texto;

        public string Texto
        {
            get { return texto; }
            set { texto = value; RaisePropertyChanged("Texto"); }
        }
    }
}
