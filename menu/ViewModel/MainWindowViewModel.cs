using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using menu.Model;
using menu;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using menu.View;
using System.Windows;


namespace menu.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        //public DataTable Select(string selectSQL) 
        //{
        //    DataTable dataTable = new DataTable("something");               
                                                                            
        //    SqlConnection sqlConnection = new SqlConnection("server=ALEKSANDRA-ПК;Trusted_Connection=Yes;DataBase=something;");
        //    sqlConnection.Open();                                           
        //    SqlCommand sqlCommand = sqlConnection.CreateCommand();          
        //    sqlCommand.CommandText = selectSQL;                           
        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); 
        //    sqlDataAdapter.Fill(dataTable);                                 
        //    return dataTable;
        //}

        public ObservableCollection<string> Spis_table
        {
            get
            {
                ObservableCollection<string> tmp_spis = new ObservableCollection<string>();
                //string tmp_name;
                //DataTable tmp_table = Select("SELECT table_name FROM information_schema.tables");
                //for (int i = 0; i < tmp_table.Rows.Count; i++)
                //{
                //    tmp_name = tmp_table.Rows[i][0].ToString();
                //    tmp_spis.Add(tmp_name);
                //}
                tmp_spis.Add("Categorie");
                tmp_spis.Add("Dish");
                tmp_spis.Add("DishIngredient");
                tmp_spis.Add("Ingredient");
                tmp_spis.Add("Menu");
                tmp_spis.Add("MenuCategorie");
                tmp_spis.Add("MenuDish");
                tmp_spis.Add("Store");
                return tmp_spis;
            }
            set
            {
            }
        }

        private string selectTable;
        public string SelectTable
        {
            get
            {
                return selectTable;
            }
            set
            {
                selectTable = value;
            }
        }

        private ICommand changeTable;
        public ICommand ChangeTable
        {
            get
            {
                return changeTable ?? (changeTable = new RelayCommand(() =>
                {
                    switch (selectTable)
                    {
                        case "Categorie":
                            CategoriesViewModel modelCat = new CategoriesViewModel();
                            CategoriesView viewCat = new CategoriesView();
                            viewCat.DataContext = modelCat;
                            viewCat.Show();
                            break;
                        case "Dish":
                            DishViewModel modelDish = new DishViewModel();
                            DishView viewDish = new DishView();
                            viewDish.DataContext = modelDish;
                            viewDish.Show();
                            break;
                        case "DishIngredient":
                            DishIngredientViewModel modelDI = new DishIngredientViewModel();
                            DishIngredientView viewDI = new DishIngredientView();
                            viewDI.DataContext = modelDI;
                            viewDI.Show();
                            break;
                        case "Ingredient":
                            IngredientViewModel modelIngredient = new IngredientViewModel();
                            IngredientView viewIngredient = new IngredientView();
                            viewIngredient.DataContext = modelIngredient;
                            viewIngredient.Show();
                            break;
                        case "MenuDish":
                            MenuDishViewModel modelMenuDish = new MenuDishViewModel();
                            MenuDishView viewMenuDish = new MenuDishView();
                            viewMenuDish.DataContext = modelMenuDish;
                            viewMenuDish.Show();
                            break;
                        default:
                            break;
                }

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
