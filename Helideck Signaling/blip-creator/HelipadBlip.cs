using GTA;
using GTA.Math;
using GTA.Native;

using Helideck_Signaling.setup_manager;


namespace Helideck_Signaling.blip_creator
{
    internal sealed class HelipadBlip
    {
        internal Vector3 Position 
        { get; private set; }

        internal bool IsShortRange
        { get; private set; } = true;

        private Blip[] _blips;


        public HelipadBlip(Vector3 position)
        {
            if (_blips == null)
                _blips = 
                    new Blip[2];

            Position =
                position;

            MakerBlip();
        }

        private void MakerBlip()
        {
            for (var i = (byte)0; i < 2; i++)
            {
                _blips[i] =
                    World
                        .CreateBlip(Position);
            }

            DefaultSettingForBlips();
        }
        private void DefaultSettingForBlips()
        {
            var blipScale = new[]
            {
                0.40f,
                0.30f
            };
            var colorCode = new[]
            {
                "~w~",  // HUD_COLOUR_WHITE
                "~y~", // HUD_COLOUR_YELLOW
                "~b~" // HUD_COLOUR_BLUE
            };
            var blipColor = new[]
            {
                BlipColor.Yellow,
                BlipColor.White
            };
            var blipSprite = new[]
            {
                BlipSprite.SonicWave,
                BlipSprite.TheJewelStoreJob
            };
            var displayType = new[]
            {
                BlipDisplayType.BothMapNoSelectable,
                BlipDisplayType.BothMapSelectable
            };
            var categoryType = new[]
            {
                BlipCategoryType.Property,
                BlipCategoryType.OwnedProperty
            };

            var colorId = 
                new Settings()
                        .ReturnColorForTheBlip();

            var isShortRange =
                true;

            var zoneLocalizedName =
                World
                    .GetZoneLocalizedName(Position);

            var nameOfHelipadLocation =
                $"{colorCode[2]}Helipad{colorCode[0]} - {colorCode[1] + zoneLocalizedName}";


            for (var i = (byte)0; i < 2; i++)
            {
                _blips[i].Sprite =
                    blipSprite[i];

                _blips[i].Scale =
                    blipScale[i];

                _blips[i].Color =
                    blipColor[i];

                _blips[i].DisplayType =
                    displayType[i];

                _blips[i].CategoryType =
                    categoryType[1];

                _blips[i].IsShortRange =
                    isShortRange;

                _blips[i].Name =
                    nameOfHelipadLocation;
            }

            SetBlipColor(_blips[0], colorId);
        }
        private void SetBlipColor(Blip blip, byte color)
        {
            var hash =
                Hash
                    .SET_BLIP_COLOUR;

            var argument0 =
                blip;

            var argument1 =
                color;

            Function
                .Call(hash, 
                      argument0, 
                      argument1);
        }

        internal void MakeTheBlipVisibleOnTheMinimap()
        {
            for (var i = (byte)0; i < 2; i++)
            {
                _blips[i]
                    .IsShortRange =
                        false;
            }

            IsShortRange =
                false;
        }
        internal void MakeTheBlipInvisibleOnTheMinimap()
        {
            for (var i = (byte)0; i < 2; i++)
            {
                _blips[i]
                    .IsShortRange =
                        true;
            }

            IsShortRange =
                true;
        }
        internal void Delete()
        {
            foreach (var blip in _blips)
            {
                if (blip != null)
                {
                    blip
                        .Delete();
                }
            }
        }
    }
}