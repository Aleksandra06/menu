using menu.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;

namespace RGR.Data
{
    public class EsaDbContext : DbContext
    {
        public EsaDbContext() : base("name=EsaDbConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EsaDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Categories> CategoriesList { get; set; }
        public virtual DbSet<DishIngredient> DishIngredientList { get; set; }
        public virtual DbSet<Ingredient> IngredientList { get; set; }
        public virtual DbSet<Dish> DishList { get; set; }
        public virtual DbSet<MenuModel> MenuList { get; set; }
        public virtual DbSet<MenuCatigorie> MenuCategorieList { get; set; }
        public virtual DbSet<MenuDish> MenuDishList { get; set; }
        public virtual DbSet<Store> StoreList { get; set; }


       

        public void IngredientAdd(Ingredient item)
        {
            try
            {
                this.IngredientList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void IngredientDelete(Ingredient ingredient)
        {
            try
            {
                IngredientList.Attach(ingredient);
                IngredientList.Remove(ingredient);
                
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public void CategoriesAdd(Categories item)
        {
            try
            {
                this.CategoriesList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void CategoriesDelete(Categories item)
        {
            try
            {

                CategoriesList.Attach(item);
                CategoriesList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void MenuCategoriesAdd(MenuCatigorie item)
        {
            try
            {
                this.MenuCategorieList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void MenuCategoriesDelete(MenuCatigorie item)
        {
            try
            {

                MenuCategorieList.Attach(item);
                MenuCategorieList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно удалить элемент!");
              
            }
        }
        public void DishAdd(Dish item)
        {
            try
            {
                this.DishList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void DishDelete(Dish item)
        {
            try
            {

                DishList.Attach(item);
                DishList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно удалить элемент!");

            }
        }
        public void MenuAdd(MenuModel item)
        {
            try
            {
                this.MenuList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Error");
                //throw;
            }
        }
        public void MenuDelete(MenuModel item)
        {
            try
            {

                MenuList.Attach(item);
                MenuList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно удалить элемент!");

            }
        }
        public void MenuDishDelete(MenuDish item)
        {
            try
            {

                MenuDishList.Attach(item);
                MenuDishList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно удалить элемент!");

            }
        }
        public void MenuDishAdd(MenuDish item)
        {
            try
            {
                this.MenuDishList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Error");
                //throw;
            }
        }
        //public void IngredientEdit(DbSet<Ingredient> List)
        //{
        //    try
        //    {   

        //        this.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}




















        //public virtual List<ViewKombiMassnahmeItem> ListKombiMassmahmeByKombiIdByMassnahmeId(int kombiId, int massnahmId)
        //{
        //    var db = new AmsEfDbContext();
        //    var sql = @"select km.KombiMassnahmId, km.KombiId, km.MassnahmId, km.PreisUe, k.Bezeichnung, k.IsActive, k.Nickname
        //                from KombiMassnahm km, Kombi k where k.KombiId = km.kombiId  ";
        //    sql += " and km.KombiId =" + kombiId;
        //    sql += " and km.MassnahmId =" + massnahmId;
        //    var kombiModuleList = db.Database.SqlQuery<ViewKombiMassnahmeItem>(sql).ToList();
        //    return kombiModuleList;
        //}

        //public virtual List<ViewKombiMassnahmeItem> ListKombiMassmahmeByMassnahmeId(int massnahmId)
        //{
        //    var db = new AmsEfDbContext();
        //    var sql = @"select km.KombiMassnahmId, km.KombiId, km.MassnahmId, km.PreisUe, k.Bezeichnung, k.IsActive, k.Nickname
        //                from KombiMassnahm km, Kombi k where k.KombiId = km.kombiId  ";
        //    sql += " and km.MassnahmId =" + massnahmId;
        //    var kombiModuleList = db.Database.SqlQuery<ViewKombiMassnahmeItem>(sql).ToList();
        //    return kombiModuleList;
        //}

        //public virtual List<ViewAnzahlGebuchteModuleItem> ListAnzahlGebuchteModuleByModuleId(int abModuleId)
        //{
        //    var db = new AmsEfDbContext();
        //    var sql = @"select cast(start as date) Start ,m.Nickname, m.dauer, COUNT (*) AnzahlAnmeldungen
        //                from gebuchte_module g, module m , standort s
        //                where m.modnr = g.modnr and s.Standortnr = g.standortnr and  m.modnr >= " + abModuleId;
        //    sql += @" group by start,m.Nickname,m.dauer 
        //                order by start, COUNT (*) desc  ";
        //    var kombiModuleList = db.Database.SqlQuery<ViewAnzahlGebuchteModuleItem>(sql).ToList();
        //    return kombiModuleList;
        //}

        //public ViewKombiModulItem GetKombiModuleByKombiIdByModuleId(int modid, int kombiId)
        //{
        //    var db = new AmsEfDbContext();
        //    var sql = @"SELECT
        //                m.Modnr,
        //                m.Modulname BezeichnungLang,
        //                m.Nickname Nickname, 
        //                k.Bezeichnung KombiBezeichnung, 
        //                m.Dauer,
        //                km.Reihenfolge,
        //                km.KombiModulId,
        //                km.KombiId

        //        FROM module m, KombiModul km, Kombi k
        //    where
        //       m.modnr = km.modnr and k.KombiId = km.KombiId
        //       and km.KombiId = " + kombiId;
        //    sql += " and km.Modnr =" + modid;
        //    var kombiModuleList = db.Database.SqlQuery<ViewKombiModulItem>(sql).FirstOrDefault();
        //    return kombiModuleList;
        //}

        //public virtual List<ViewKombiModulItem> GetKombiModulesByKombiId(int kombiId)
        //{
        //    var db = new AmsEfDbContext();
        //    var sql = @"SELECT
        //                m.Modnr,
        //                m.Modulname Bezeichnung,
        //                m.Nickname Nickname, 
        //                m.Aktiv,
        //                k.Bezeichnung KombiBezeichnung, 
        //                m.Dauer,
        //                km.Reihenfolge,
        //                km.KombiModulId,
        //                km.KombiId

        //        FROM module m, KombiModul km, Kombi k
        //    where
        //       m.modnr = km.modnr and k.KombiId = km.KombiId
        //       and m.modnr > 188 
        //       and km.KombiId = " + kombiId;
        //    var kombiModuleList = db.Database.SqlQuery<ViewKombiModulItem>(sql).ToList();
        //    return kombiModuleList;
        //}

        //public virtual List<ViewEcdlKursteilnehmerItem> ListEcdlKursteilnehmerItemByKursstartByUnterrichtsmodellId(
        //    string start, int interrichtsmodellId)
        //{
        //    var dauerInWochen = 8;
        //    if (interrichtsmodellId == 2)
        //        dauerInWochen = 16;

        //    var db = new AmsEfDbContext();
        //    var sql =
        //        @"
        //        declare @startGroß date, @endeGroß date 
        //        declare @unterrichtsmodellId int, @dauerInWochen int

        //        select 	@startGroß = t.Start, @endeGroß = t.Ende, @unterrichtsmodellId = UnterrichtsmodellId, @dauerInWochen = dauerInWochen  
        //        from Terminstamm t ";
        //    sql += " where t.Start = '" + start + "'";
        //    sql += " and t.DauerInWochen = " + dauerInWochen;
        //    sql += " and t.UnterrichtsmodellId = " + interrichtsmodellId;
        //    sql += @"
        //    --Kombi-Module
        //        select distinct g.LaufendeNr, kkm.Reihenfolge, mm.Nickname, 
        //               cast(g.start as date) Start ,cast(g.ende as date) Ende,
        //               g.standortnr StandortId,g.modnr ModulId, g.tnnr, g.virtualKr, 
        //               t.Vorname, t.Nachname, s.Ort Standort --, count(g.tnnr) 
        //        from Kombi kk, gebuchte_module g ,KombiModul kkm,module mm, standort s, teilnehmer t
        //        where
        //          s.Standortnr = g.standortnr and t.tnnr = g.tnnr and   
        //          kk.KombiId = kkm.KombiId and 
        //          mm.modnr = kkm.Modnr and 
        //          g.modnr = mm.modnr and
        //         (g.start >= @startGroß and g.start <= @endeGroß) and
        //         g.UnterrichtsmodellId = @unterrichtsmodellId and
        //        g.modnr in (    
        //         select km.modnr 
        //         from Kombi k, KombiModul km, module m 
        //         where 
        //          k.KombiId = km.KombiId and 
        //          m.modnr = km.Modnr and 
        //          k.Bezeichnung like '%ecdl%') 

        //        order by Reihenfolge, Start";
        //    var kombiModuleList = db.Database.SqlQuery<ViewEcdlKursteilnehmerItem>(sql).ToList();
        //    return kombiModuleList;
        //}
    }
}