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
    class IngredientChangeWindowViewModel : INotifyPropertyChanged
    {
        public Ingredient MeaningIng { get; set; }
        public string name { get; set; }

        public IngredientChangeWindowViewModel(Ingredient Ing)
        {
            MeaningIng = Ing;
            name = MeaningIng.Name.ToString();
        }

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    EsaDbContext db = new EsaDbContext();
                    MeaningIng.Name = name;
                    db.IngredientChange(MeaningIng);
                    IngredientViewModel.Update();
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
