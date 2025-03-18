using GTA.Math;
using Helideck_signaling.features.helipadBlip.creators;
using System.Collections.Generic;

namespace Helideck_signaling.features.helipadBlip.managers
{
    internal abstract class HelipadBlipManager : HelipadBlipCreator
    {
        protected static List<Vector3> NativeHelipadPositions
            = new List<Vector3>();

        protected static List<Vector3> CustomHelipadPositions
            = new List<Vector3>();

        protected static bool AreAllNativeBlipsPositionsSet()
        {
            return NativeHelipadPositions.Count > 0
                   &&
                   NativeHelipadPositions.Count == NativeHelipadCount;
        }

        protected static bool AreAllCustomBlipsPositionsSet()
        {
            return CustomHelipadPositions.Count > 0
                   &&
                   CustomHelipadPositions.Count == CustomHelipadCount;
        }
    }
}
