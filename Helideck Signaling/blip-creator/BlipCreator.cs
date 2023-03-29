using GTA;

using System.Collections.Generic;

using Helideck_Signaling.setup_manager;


namespace Helideck_Signaling.blip_creator
{
    internal sealed class BlipCreator
    {
        private IList<HelipadBlip> _helipadBlips;
        
        public BlipCreator()
        {
            if (_helipadBlips == null)
                _helipadBlips =
                    new List<HelipadBlip>();
        }

        internal void CreateAllAvailableBlips()
        {
            var locationOfTheNativeHelipads =
                new Settings()
                        .ReturnLocationOfAllNativeHelipads();

            foreach (var locationOfTheNativeHelipad in locationOfTheNativeHelipads)
            {
                _helipadBlips
                    .Add(new HelipadBlip(locationOfTheNativeHelipad));
            }

            var modelHelipads = new[]
            {
                "prop_helipad_01",
                "prop_helipad_02"
            };

            var buildings =
                World
                    .GetAllBuildings();

            foreach (var building in buildings)
            {
                if (building.Model == modelHelipads[0] ||
                    building.Model == modelHelipads[1])
                {
                    var hasThisHelipadBeenIdentifiedYet =
                        false;

                    foreach (var locationOfTheNativeHelipad in locationOfTheNativeHelipads)
                    {
                        if (building.Position.ToString() == locationOfTheNativeHelipad.ToString())
                            hasThisHelipadBeenIdentifiedYet =
                                true;
                    }

                    if (!hasThisHelipadBeenIdentifiedYet)
                        _helipadBlips
                            .Add(new HelipadBlip(building
                                                    .Position));
                }
            }

            var props =
                World
                    .GetAllProps();

            foreach (var prop in props)
            {
                if (prop.Model == modelHelipads[0] ||
                    prop.Model == modelHelipads[1])
                {
                    var hasThisHelipadBeenIdentifiedYet =
                        false;

                    foreach (var locationOfTheNativeHelipad in locationOfTheNativeHelipads)
                    {
                        if (prop.Position.ToString() == locationOfTheNativeHelipad.ToString())
                            hasThisHelipadBeenIdentifiedYet =
                                true;
                    }

                    if (!hasThisHelipadBeenIdentifiedYet)
                        _helipadBlips
                            .Add(new HelipadBlip(prop
                                                    .Position));
                }
            }
        }
        
        internal void ResetBlipVisibility()
        {
            if (_helipadBlips != null)
            {
                foreach (var helipadBlip in _helipadBlips)
                {
                    if (!helipadBlip.IsShortRange)
                    {
                        helipadBlip
                            .MakeTheBlipInvisibleOnTheMinimap();
                    }
                }
            }
        }
        internal void IncreaseBlipVisibility()
        {
            var blip =
                World
                    .WaypointBlip;

            var waypointPosition =
                blip
                    .Position;

            foreach (var helipadBlip in _helipadBlips)
            {
                if (helipadBlip.Position.X == waypointPosition.X &&
                    helipadBlip.Position.Y == waypointPosition.Y &&
                    helipadBlip.IsShortRange)
                {
                    helipadBlip
                        .MakeTheBlipVisibleOnTheMinimap();
                        
                    World
                        .WaypointBlip
                            .IsShortRange = true;
                }
            }
        }

        internal void ClearsMapLeavingOnlyTheSelectedBlips()
        {
            foreach (var helipadBlip in _helipadBlips)
            {
                if (helipadBlip.IsShortRange)
                {
                    helipadBlip
                        .Delete();
                }
            }
        }
        internal bool IsEmpty()
        {
            if (_helipadBlips != null)
                return _helipadBlips.Count == 0;

            return true;
        }
        internal void Delete()
        {
            if (_helipadBlips != null)
            {
                foreach (var blip in _helipadBlips)
                {
                    blip
                        .Delete();
                }
            }
        }
    }
}