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
using menu.Data;
using menu.View.Change;

namespace menu.ViewModel.Change
{
    class CategoriesChangeWindowViewModel : INotifyPropertyChanged
    {
        public CategorieModel MeaningCat { get; set; }
        public string name { get; set; }

        public CategoriesChangeWindowViewModel(CategorieModel cat)
        {
            MeaningCat = cat;
            name = MeaningCat.Categorie;
        }

        public CategoriesChangeWindowViewModel()
        {
        }

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    MeaningCat.Categorie = name;
                    EsaDbContext db = new EsaDbContext();
                    Categories tmpCat = new Categories();
                    tmpCat.Id = Convert.ToInt32(MeaningCat.Id);
                    tmpCat.Categorie = MeaningCat.Categorie;
                    db.CategoriesChange(tmpCat);
                    CategoriesViewModel.Update();
                    Close();
                }));
            }
        }

        public event EventHandler Closed;
        private void Close()
        {
            if (Closed != null) Closed(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
