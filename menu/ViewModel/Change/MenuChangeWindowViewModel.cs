using GalaSoft.MvvmLight.Command;
using menu.Data;
using menu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace menu.ViewModel.Change
{
    class MenuChangeWindowViewModel : INotifyPropertyChanged
    {
        public MenuMModel Menu { get; set; }
        public string Name { get; set; }
        public string MenuCategory { get; set; }

        public MenuChangeWindowViewModel(MenuMModel menu)
        {
            Name = menu.Name;
            Menu = menu;
            MenuCategory = menu.MenuCategory;
        }

        public ObservableCollection<string> MenuCategorySpis
        {
            get
            {
                EsaDbContext db = new EsaDbContext();
                ObservableCollection<string> tmpMCSpis = new ObservableCollection<string>();
                foreach (var e in db.MenuCategorieList)
                {
                    tmpMCSpis.Add(e.Name.ToString());
                }
                return tmpMCSpis;
            }
        }

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    MenuModel changeMC = new MenuModel();
                    EsaDbContext db = new EsaDbContext();
                    changeMC.Datum = DateTime.Now;
                    changeMC.Name = Name;
                    changeMC.MenuCategoryId = db.MenuCategorieList.Where(i => i.Name == MenuCategory).Single().Id;
                    changeMC.Id = Menu.Id;
                    db.MenuChange(changeMC);
                    MenuViewModel.Update();
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
