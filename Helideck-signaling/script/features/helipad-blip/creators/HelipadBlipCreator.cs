using GTA.Math;
using Helideck_signaling.utils;
using System.Collections.Generic;

namespace Helideck_signaling.script.features.helipad_blip.creators
{
    internal abstract class HelipadBlipCreator
    {
        protected static int NativeHelipadCount;

        protected static int CustomHelipadCount;

        protected static void SetAllNativeHelipadPositionData(out List<Vector3> allNativeHelipadPositionData)
        {
            Settings.DeserializePositions(Settings.PathToTheNativePositionsXml, out allNativeHelipadPositionData);

            NativeHelipadCount
                = allNativeHelipadPositionData.Count;
        }

        protected static void SetAllCustomHelipadPositionData(out List<Vector3> allCustomHelipadPositionData)
        {
            Settings.DeserializePositions(Settings.PathToTheCustomPositionsDataXml, out allCustomHelipadPositionData);

            CustomHelipadCount
                = allCustomHelipadPositionData.Count;
        }

        protected static void CreateNativeHelipadPositionDataFile(in List<Vector3> allNativePositions)
        {
            Settings.SerializerPositions(Settings.PathToTheNativePositionsXml, allNativePositions);
        }

        protected static void CreateCustomHelipadPositionDataFile(in List<Vector3> allCustomPositions)
        {
            Settings.SerializerPositions(Settings.PathToTheCustomPositionsDataXml, allCustomPositions);
        }
    }
}
