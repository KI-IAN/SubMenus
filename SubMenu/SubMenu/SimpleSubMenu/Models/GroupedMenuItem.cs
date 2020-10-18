using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SubMenu.SimpleSubMenu.Models
{
    public class GroupedMenuItem : ObservableCollection<NavigationMenuItem>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }

        public string StateIcon { get; set; }

        public bool IsExpanded { get; set; }

    }
}
