using GTA;

namespace Helideck_signaling.script.features.custom_blip.creators.resources.structs
{
    internal struct StCustomBlipConfiguration
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
