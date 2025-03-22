using GTA;
using GTA.UI;
using Helideck_signaling.script.features.helipad_blip;
using System.Globalization;

namespace Helideck_signaling.script
{
    [ScriptAttributes(NoDefaultInstance = true)]
    internal sealed class HelideckSignaling : Script
    {
        private static readonly HelipadBlip _helipadBlip = new HelipadBlip();

        public HelideckSignaling()
        {
            SetCultureInvariant();

            Tick    += (o, e) =>
            {
                _helipadBlip
                    .SetAllNativeHelipadPosition();
                _helipadBlip
                    .SetAllCustomHelipadPosition();

                if (Game.WasCheatStringJustEntered(/* check for duplicate helipad position */ "hscfdhp"))
                {
                    var amount
                        = 0;

                    foreach (var nhp in _helipadBlip.GetAllNativeHelipadPosition())
                    {
                        foreach (var chp in _helipadBlip.GetAllCustomHelipadPosition())
                        {
                            if (nhp.Length() == chp.Length())
                            {
                                amount++;

                                Notification.PostTickerWithTokens($"~b~{nhp}~w~ == ~y~{chp}~w~", true, true);
                            }
                        }
                    }

                    Notification.PostTickerWithTokens($"Amount of duplicate helipad position: ~g~{amount}~w~", true, true);
                }
            };

            Aborted += (o, e) =>
            {
                DeleteAllNativeHelipadBlip();
                DeleteAllCustomHelipadBlip();

                ResetCultureInvariant();
            };

            void SetCultureInvariant()
            {
                CultureInfo
                    .DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

                CultureInfo
                    .DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            }

            void ResetCultureInvariant()
            {
                CultureInfo
                    .DefaultThreadCurrentCulture = CultureInfo.InstalledUICulture;

                CultureInfo
                    .DefaultThreadCurrentUICulture = CultureInfo.InstalledUICulture;
            }
        }

        internal void CreateAllNativeHelipadBlip()
        {
            _helipadBlip
                .CreateAllNativeHelipadBlip();
        }
        internal void DeleteAllNativeHelipadBlip()
        {
            _helipadBlip
                .DeleteAllNativeHelipadBlip();
        }

        internal void CreateAllCustomHelipadBlip()
        {
            _helipadBlip
                .CreateAllCustomHelipadBlip();
        }
        internal void DeleteAllCustomHelipadBlip()
        {
            _helipadBlip
                .DeleteAllCustomHelipadBlip();
        }
    }
}
