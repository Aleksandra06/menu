using GalaSoft.MvvmLight.Command;
using menu.Data;
using menu.Model;
using menu.View.Change;
using menu.ViewModel.Change;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace menu.ViewModel
{
    class IngredientViewModel : INotifyPropertyChanged
    {
        public static  ObservableCollection<Ingredient> IngredientCollection { get; set; } = new ObservableCollection<Ingredient>();

        public IngredientViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.IngredientList)
            {
                IngredientCollection.Add(e);
            }
        }

        public static void Update()
        {
            IngredientCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.IngredientList)
            {
                IngredientCollection.Add(e);
            }
        }

        private string newIngredient = "";
        public string NewIngredient
        {
            get
            {
                return newIngredient;
            }
            set
            {
                newIngredient = value;
                OnPropertyChanged("NewIngredient");
            }
        }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (newIngredient != "")
                    {
                        Ingredient newIng = new Ingredient();
                        EsaDbContext db = new EsaDbContext();
                        newIng.Id = IngredientCollection.Last().Id;
                        newIng.Name = newIngredient;
                        db.IngredientAdd(newIng);
                        newIngredient = "";
                        OnPropertyChanged("newIngredient");
                        Update();
                    }
                    else
                        MessageBox.Show("Введите ингредиент");
                }
                ));
            }
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand<object>((object parametr) =>
                {
                    var parametrtmp = (Ingredient)parametr;
                    Delete del = new Delete();
                    del.IngredientDelete(parametrtmp);
                    OnPropertyChanged("IngredientCollection");
                    Update();
                }
                ));

            }
        }

        private ICommand redactClick;
        public ICommand RedactClick
        {
            get
            {
                return redactClick ?? (redactClick = new RelayCommand<object>((object parametr) =>
                {
                    var Ing = (Ingredient)parametr;
                    IngredientChangeWindowViewModel modelChange = new IngredientChangeWindowViewModel(Ing);
                    IngredientChangeWindow viewChange = new IngredientChangeWindow();
                    viewChange.DataContext = modelChange;
                    viewChange.Show();
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
