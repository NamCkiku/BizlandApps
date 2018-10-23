using System.Collections.Generic;

namespace Bizland.Model
{
    public class Convenient
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public static List<Convenient> All
        {
            get
            {
                return ConvenientList;
            }
        }

        public Convenient(string Name, string Icon)
        {
            this.Name = Name;
            this.Icon = Icon;
        }

        private static readonly List<Convenient> ConvenientList = new List<Convenient>
        {
            new Convenient("Máy lạnh","ic_airconditioner.png"),
            new Convenient("WC riêng","ic_wc.png"),
            new Convenient("Chỗ để xe","ic_garage.png"),
            new Convenient("Wifi","ic_wifi.png"),
            new Convenient("Máy giặt","ic_washingmachine.png"),
            new Convenient("Tủ lạnh","ic_fridge.png"),
            new Convenient("Tivi","ic_tv.png"),
            new Convenient("Nhà bếp","ic_kitchen.png"),
            new Convenient("Giường","ic_bed.png"),
            new Convenient("An ninh","ic_security.png"),
        };
    }
}
