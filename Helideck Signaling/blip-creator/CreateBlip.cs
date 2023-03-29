using GTA;
using GTA.Math;
using System.Collections.Generic;

namespace Helideck_Signaling.blip_creator
{
    class CreateBlip
    {
        private readonly string[] modelHelipad =
        {
            "prop_helipad_01",
            "prop_helipad_02"
        };


        private List<HelipadBlip> Helipads
        { get; set; }

        public CreateBlip()
        {
            Helipads = new List<HelipadBlip>();
        }
        public CreateBlip(Vector3[] positions) : this()
        {
            foreach (var position in positions)
            {
                Helipads.Add(new HelipadBlip(position));
            }
            
            foreach (var build in World.GetAllBuildings())
            {
                if (build.Model == modelHelipad[0] || 
                    build.Model == modelHelipad[1])
                {
                    Helipads.Add(new HelipadBlip(build.Position));
                }
            }

            foreach (var propHelipad in World.GetAllProps(modelHelipad[0],
                                                          modelHelipad[1]))
            {
                Helipads.Add(new HelipadBlip(propHelipad.Position));
            }
        }

        internal void MakeTheHelipadBlipInvisible()
        {
            foreach (var helipad in Helipads)
            {
                if (!helipad.IsTheBlipShortRange())
                {
                    helipad.MakeTheBlipInvisibleOnTheMinimap();
                }
            }
        }

        internal void MakeTheHelipadBlipVisibleOnTheMinimapAtLongDistances()
        {
            MakeSureThePositionOfTheHelipadMatchesThatOfTheWaypoint();
        }

        void MakeSureThePositionOfTheHelipadMatchesThatOfTheWaypoint()
        {
            var waypointPosition = World.WaypointPosition;

            foreach (var helipad in Helipads)
            {
                if (helipad.Position.X == waypointPosition.X &&
                    helipad.Position.Y == waypointPosition.Y &&
                    helipad.IsTheBlipShortRange())
                {
                    MakeTheBlipVisibleOnTheMinimap(helipad);
                }
            }
        }
        void MakeTheBlipVisibleOnTheMinimap(HelipadBlip helipad)
        {
            helipad.MakeTheBlipVisibleOnTheMinimap();
            World.WaypointBlip.IsShortRange = true;
        }


        internal bool IsEmpty() => Helipads.Count == 0;
        internal void Delete()
        {
            foreach (var blip in Helipads)
            {
                blip.Delete();
            }
        }
    }
}