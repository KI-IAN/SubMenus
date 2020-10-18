using MvvmHelpers.Commands;
using SubMenu.SimpleSubMenu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SubMenu.SimpleSubMenu.ViewModels
{
    public class SubMenuViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GroupedMenuItem> _groupedMenuItems;

        public ObservableCollection<GroupedMenuItem> GroupedMenuItems
        {
            get { return _groupedMenuItems; }

            set
            {
                _groupedMenuItems = value;
                OnPreopertyChanged();
            }
        }



        public ICommand RefreshMenus { get; set; }


        public SubMenuViewModel()
        {
            SetMenuItems();

            RefreshMenus = new MvvmHelpers.Commands.Command<GroupedMenuItem>((item) => RefreshMenu(item));

        }


        private void RefreshMenu(GroupedMenuItem menu)
        {
            //Reload menus
            SetMenuItems();

            menu.IsExpanded = !menu.IsExpanded;

            // Show only the sub menus of the currently selected main menu
            // Collapse submenus for other main menus
            if (menu.IsExpanded)
            {
                var selectedMenu = _groupedMenuItems.Where(r => r.LongName.Equals(menu.LongName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();
                selectedMenu.IsExpanded = menu.IsExpanded;
                selectedMenu.StateIcon = "arrow_a.png";

                var unSelectedMenus = _groupedMenuItems.Where(r => !r.LongName.Equals(menu.LongName, StringComparison.OrdinalIgnoreCase));
                foreach (var closedMenus in unSelectedMenus)
                {
                    closedMenus.IsExpanded = false;
                    closedMenus.StateIcon = "arrow_b.png";

                    //remove summenus from those main menus to simulate collapsing
                    closedMenus.Clear();
                }
            }
            else
            {
                // collapse all the menus
                foreach (var closedMenus in _groupedMenuItems)
                {
                    closedMenus.IsExpanded = false;
                    closedMenus.StateIcon = "arrow_b.png";

                    //remove summenus from those main menus to simulate collapsing
                    closedMenus.Clear();
                }
            }

            GroupedMenuItems = _groupedMenuItems;

        }


        #region Dummy Data
        private void SetMenuItems()
        {
            //await Task.Delay(1);
            var mainMenu_1 = new GroupedMenuItem() { LongName = "MM#1", ShortName = "M1", StateIcon = "arrow_a.png" };
            var mainMenu_2 = new GroupedMenuItem() { LongName = "MM#2", ShortName = "M2", StateIcon = "arrow_a.png" };
            var mainMenu_3 = new GroupedMenuItem() { LongName = "MM#3", ShortName = "M3", StateIcon = "arrow_a.png" };


            mainMenu_1.Add(new NavigationMenuItem() { Title = "SM 1.1", PageType = typeof(SampleViews.SM1_1) });
            mainMenu_1.Add(new NavigationMenuItem() { Title = "SM 1.2", PageType = typeof(SampleViews.SM1_2) });
            mainMenu_1.Add(new NavigationMenuItem() { Title = "SM 1.3" });

            mainMenu_2.Add(new NavigationMenuItem() { Title = "SM 2.1" });
            mainMenu_2.Add(new NavigationMenuItem() { Title = "SM 2.2" });

            mainMenu_3.Add(new NavigationMenuItem() { Title = "SM 3.1" });

            _groupedMenuItems = new ObservableCollection<GroupedMenuItem>();
            _groupedMenuItems.Add(mainMenu_1);
            _groupedMenuItems.Add(mainMenu_2);
            _groupedMenuItems.Add(mainMenu_3);

        }

        #endregion


        #region INotifyPropertyChanged Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPreopertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
