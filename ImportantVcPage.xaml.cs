using NICVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace NICVC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportantVcPage : ContentPage
    {
        ImportantVcDatabase importantVcDatabase;
        List<SaveUserPreferences> SavedUserPreferList;
        SaveUserPreferencesDatabase saveUserPreferencesDatabase;
        string districtname, studioname;

        public ImportantVcPage()
        {
            InitializeComponent();
            importantVcDatabase = new ImportantVcDatabase();

            lbl_user_header.Text = App.GetLabelByKey("ImportantVc");      
            Lbl_Header.Text = App.GetLabelByKey("nicvdconf");
        

            saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
            SavedUserPreferList = saveUserPreferencesDatabase.GetSaveUserPreferences("select * from saveUserPreferences").ToList();
            string statename = SavedUserPreferList.ElementAt(0).StateName.ToString();

            try
            {
                districtname = SavedUserPreferList.ElementAt(0).DistrictName.ToString();
            }
            catch
            {
                districtname = "";
            }
            try
            {
               studioname = SavedUserPreferList.ElementAt(0).StudioName.ToString();
            }
            catch
            {
                studioname = "";
            }


            if (!string.IsNullOrEmpty(studioname))
            {
                lbl_user_header1.Text = statename + " - " + districtname + " - " + studioname;
            }
            else if (!string.IsNullOrEmpty(districtname))
            {
                lbl_user_header1.Text = statename + " - " + districtname;
            }
            else
            {
                lbl_user_header1.Text = statename;
            }

            List<ImportantVc> importantVclist;
            //string query = $"SELECT Startingtime, DateofVC, count(*) as NoOfVc from TodayVc where Important='Y' and Startingtime !='All' and VCStatus = 'Confirmed'  group by substr(dateofvc,7)||substr(dateofvc,4,2)||substr(dateofvc,1,2)";
            string query = $"Select *,count(*) as NoOfVc from TodayVc where Startingtime <> 'All' and VCStatus = 'Confirmed' and Important='Y' group by time(Startingtime)";
            importantVclist = importantVcDatabase.GetImportantVc(query).ToList();
            if (App.Language == 0)
            {
                listView_ImportantVc.IsVisible = true;
                listView_ImportantVclocal.IsVisible = false;
                listView_ImportantVc.ItemsSource = importantVclist;
            }
            else
            {
                listView_ImportantVc.IsVisible = false;
                listView_ImportantVclocal.IsVisible = true;
                listView_ImportantVclocal.ItemsSource = importantVclist;
            }
        }



        private void listView_ImportantVc_ItemTapped(object sender, EventArgs e)
        {
            var tappedEventArgs = e as TappedEventArgs;
            var currentRecord = tappedEventArgs?.Parameter as ImportantVc;
            if (currentRecord == null)
            {
                // For CollectionView, get the binding context from the sender
                var grid = sender as Microsoft.Maui.Controls.Grid;
                currentRecord = grid?.BindingContext as ImportantVc;
            }
            
            if (currentRecord != null)
            {
                App.vcdateimportantvc = currentRecord.DateofVC.ToString();
                Navigation.PushAsync(new ImportantVcDetailsPage(currentRecord.Startingtime));
            }
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new NICVCTabbedPage());
        }


    }
}