using GTA;
using GTA.Math;
using Helideck_signaling.script.features.custom_blip.creators.resources.structs;

namespace Helideck_signaling.script.features.custom_blip.creators
{
    internal sealed class CustomBlipCreator
    {
        private readonly Blip _topBlip;

        private readonly Blip _bottomBlip;

        private static readonly StCustomBlipConfiguration _topBlipConfig
            = new StCustomBlipConfiguration
            {
                IsShortRange
                = true,
                Scale
                = 0.40f,
                Color
                = BlipColor
                        .Yellow,
                Sprite
                = BlipSprite
                        .SonicWave,
                DisplayType
                = BlipDisplayType
                        .BothMapNoSelectable,
                CategoryType
                = BlipCategoryType
                        .Property
            };

        private static readonly StCustomBlipConfiguration _bottomBlipConfig
            = new StCustomBlipConfiguration
            {
                IsShortRange
                = true,
                Scale
                = 0.30f,
                Color
                = BlipColor
                        .White,
                Sprite
                = BlipSprite
                        .TheJewelStoreJob,
                DisplayType
                = BlipDisplayType
                        .BothMapSelectable,
                CategoryType
                = BlipCategoryType
                        .OwnedProperty
            };

        public CustomBlipCreator(in Vector3 position)
        {
            _topBlip
                = World.CreateBlip(position);

            _bottomBlip
                = World.CreateBlip(position);

            var blips
                = new[]
                {
                    _topBlip,
                    _bottomBlip,
                };

            var blipsConfig
                = new[]
                {
                    _topBlipConfig,
                    _bottomBlipConfig,
                };

            SetBlipConfig();

            void SetBlipConfig()
            {
                for (var i = 0; i < 2; i++)
                {
                    blips[i]
                    .IsShortRange
                        = blipsConfig[i]
                                    .IsShortRange;
                    blips[i]
                    .Scale
                        = blipsConfig[i]
                                    .Scale;
                    blips[i]
                    .Sprite
                        = blipsConfig[i]
                                    .Sprite;
                    blips[i]
                    .Color
                        = blipsConfig[i]
                                    .Color;
                    blips[i]
                    .DisplayType
                        = blipsConfig[i]
                                    .DisplayType;
                    blips[i]
                    .CategoryType
                        = blipsConfig[i]
                                    .CategoryType;
                }
            }

            SetBlipMetadata();

            void SetBlipMetadata()
            {
                _bottomBlip
                    .Name = $"~b~Helipad~w~ - ~y~{World.GetZoneLocalizedName(_bottomBlip.Position)}~w~";
            }
        }

        internal void Disable()
        {
            _topBlip
            .DisplayType
                = BlipDisplayType
                                .NoDisplay;

            _bottomBlip
            .DisplayType
                = BlipDisplayType
                                .NoDisplay;
        }
        internal void Enable()
        {
            _topBlip
            .DisplayType
                = _topBlipConfig
                                .DisplayType;
            _bottomBlip
            .DisplayType
                = _bottomBlipConfig
                                .DisplayType;
        }

        internal void Delete()
        {
            _topBlip?
                .Delete();

            _bottomBlip?
                .Delete();
        }
    }
}
