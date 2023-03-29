using GTA;
using GTA.Math;

namespace Helideck_Signaling.blip_creator
{
    class HelipadBlip
    {
        /*
             #1: bigCircleOutline
             #2: theJewelStoreJob
        */
        internal Vector3 Position
        { get; private set; }

        internal readonly Blip[] blips = new Blip[2];
        private readonly float[] blipSize =
        {
            0.40f,
            0.30f
        };
        private readonly BlipColor[] blipColor =
        {
            BlipColor.Yellow6,
            BlipColor.White
        };
        private readonly BlipSprite[] blipSprite =
        {
            BlipSprite.SonicWave,
            BlipSprite.TheJewelStoreJob
        };
        private readonly BlipDisplayType[] displayType =
        {
            BlipDisplayType.BothMapNoSelectable,
            BlipDisplayType.BothMapSelectable
        };




        public HelipadBlip(Vector3 position)
        {
            Position = position;

            MakerBlip();
        }

        private void MakerBlip()
        {
            for (int i = 0; i < 2; i++)
            {
                blips[i] = World.CreateBlip(Position);
            }

            DefaultSettingForBlips();
        }

        void DefaultSettingForBlips()
        {
            for (int i = 0; i < 2; i++)
            {
                blips[i].Sprite =
                    blipSprite[i];

                blips[i].Scale =
                    blipSize[i];

                blips[i].Color =
                    blipColor[i];

                blips[i].DisplayType =
                    displayType[i];

                blips[i].Name =
                    "Helipad";

                blips[i].IsShortRange =
                    true;
            }
        }

        internal void MakeTheBlipVisibleOnTheMinimap()
        {
            for (int i = 0; i < 2; i++)
                blips[i].IsShortRange = false;
        }
        internal void MakeTheBlipInvisibleOnTheMinimap()
        {
            for (int i = 0; i < 2; i++)
                blips[i].IsShortRange = true;
        }
        internal bool IsTheBlipShortRange() => blips[0].IsShortRange;
        internal void Delete()
        {
            foreach (var blip in blips)
            {
                if (blip != null)
                {
                    blip.Delete();
                }
            }
        }
    }
}