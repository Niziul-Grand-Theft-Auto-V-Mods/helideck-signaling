using GTA;
using Helideck_signaling.utils;
using LemonUI;
using LemonUI.Menus;

namespace Helideck_signaling.script.features.user_interface
{
    internal class UserInterface : Script
    {
        public UserInterface()
        {
            var nhpMenu
                = new NativeMenu("NHP", "Native Helipad", "Positions");

            var chpMenu
                = new NativeMenu("CHP", "Custom Helipad", "Positions");

            var homeMenu
                = new NativeMenu("Helideck Signaling")
                {
                    nhpMenu,
                    chpMenu,
                };

            nhpMenu.Opening += (o, e) =>
            {
                foreach (var nhp in Singleton.NativeHelipadPosition)
                {
                    nhpMenu.Add(new NativeItem($"{World.GetZoneLocalizedName(nhp)}", $"~b~{nhp:F6}~w~"));
                }
            };

            nhpMenu.Closing += (o, e) =>
            {
                nhpMenu.Items.Clear();
            };

            chpMenu.Opening += (o, e) =>
            {
                foreach (var chp in Singleton.CustomHelipadPosition)
                {
                    chpMenu.Add(new NativeItem($"{World.GetZoneLocalizedName(chp)}", $"~b~{chp:F6}~w~"));
                }
            };

            chpMenu.Closing += (o, e) =>
            {
                chpMenu.Items.Clear();
            };

            var pool
                = new ObjectPool()
                {
                    homeMenu,
                    nhpMenu,
                    chpMenu,
                };

            Tick    += (o, e) =>
            {
                pool.Process();

                if (Game.WasCheatStringJustEntered(/*open menu*/ "hsom"))
                {
                    homeMenu.Visible = true;
                }
            };

            Aborted += (o, e) =>
            {
            };
        }
    }
}
