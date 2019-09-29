using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using menu.ViewModel;
using menu.View;
using menu.Model;

namespace menu.ViewModel
{
    class CategoriesChangeWindowViewModel : INotifyPropertyChanged
    {
        public Categories MeaningCat { get; set; }
        public string name { get; set; }

        public CategoriesChangeWindowViewModel(Categories cat)
        {
            MeaningCat = cat;
            name = MeaningCat.ToString();
        }

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    CategoriesViewModel catModel = new CategoriesViewModel();
                    MeaningCat.Categorie = name;
                    catModel.Redact(MeaningCat);
                }));
            }
        }
            
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
