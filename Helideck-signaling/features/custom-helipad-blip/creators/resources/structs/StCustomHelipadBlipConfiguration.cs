using GTA;
using GTA.Math;

namespace Helideck_signaling.features.custom_helipad_blip.creators.resources.structs
{
    internal struct StCustomHelipadBlipConfiguration
    {
        internal bool IsShortRange
        {
            get;
            set;
        }
        internal float Scale
        {
            get;
            set;
        }
        internal BlipColor Color
        {
            get;
            set;
        }
        internal BlipSprite Sprite
        {
            get;
            set;
        }
        internal BlipDisplayType DisplayType
        {
            get;
            set;
        }
        internal BlipCategoryType CategoryType
        {
            get;
            set;
        }
    }
}
