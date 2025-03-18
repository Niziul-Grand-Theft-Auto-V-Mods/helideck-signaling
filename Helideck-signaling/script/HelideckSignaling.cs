using GTA;
using Helideck_signaling.features.helipadBlip;
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
