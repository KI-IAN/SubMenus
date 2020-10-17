using SubMenu.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubMenu.ViewModels
{
    public class RoomViewModel
    {
        private Room _room;

        public RoomViewModel(Room room)
        {
            this._room = room;
        }

        public string RoomName { get { return _room.RoomName; } }
        public int TypeID { get { return _room.TypeID; } }

        public Room Room
        {
            get => _room;
        }
    }
}
