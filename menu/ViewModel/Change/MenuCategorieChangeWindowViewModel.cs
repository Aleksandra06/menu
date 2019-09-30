using GalaSoft.MvvmLight.Command;
using menu.Data;
using menu.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace menu.ViewModel.Change
{
    class MenuCategorieChangeWindowViewModel : INotifyPropertyChanged
    {
        public MenuCategorie MeaningCat { get; set; }
        public string Name { get; set; }

        public MenuCategorieChangeWindowViewModel(MenuCategorie cat)
        {
            MeaningCat = cat;
            Name = MeaningCat.ToString();
        }

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    MeaningCat.Name = Name;
                    EsaDbContext db = new EsaDbContext();
                    db.MenuCategoriesChange(MeaningCat);
                    MenuCategorieViewModel.Update();
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
