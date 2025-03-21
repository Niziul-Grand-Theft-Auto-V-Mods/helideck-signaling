using GTA.Math;
using Helideck_signaling.settings.creators;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Helideck_signaling.script.features.helipad_blip.creators
{
    internal abstract class HelipadBlipCreator : SettingsCreator
    {
        private static string PathToTheHelipadsInformationFolder => $@"{PathToTheHelideckSignalingFolder}\HelipadsInformation";

        internal static string PathToTheNativePositionsXml
        {
            get
            {
                return $@"{PathToTheHelipadsInformationFolder}\NativePositions.xml";
            }
        }

        internal static string PathToTheCustomPositionsDataXml
        {
            get
            {
                return $@"{PathToTheHelipadsInformationFolder}\CustomPositions.xml";
            }
        }

        protected static int NativeHelipadCount;

        protected static int CustomHelipadCount;

        private static List<Vector3> DeserializePositions(string path)
        {
            var serializer
                = new XmlSerializer(typeof(List<Vector3>));

            using (var stream = new FileStream(path, FileMode.Open))
            {
                return (List<Vector3>)serializer.Deserialize(stream);
            }
        }

        protected static void SetAllNativeHelipadPositionData(out List<Vector3> allNativeHelipadPositionData)
        {
            allNativeHelipadPositionData
                = DeserializePositions(PathToTheNativePositionsXml);
            NativeHelipadCount
                = allNativeHelipadPositionData.Count;
        }

        protected static void SetAllCustomHelipadPositionData(out List<Vector3> allCustomHelipadPositionData)
        {
            allCustomHelipadPositionData
                = DeserializePositions(PathToTheCustomPositionsDataXml);
            CustomHelipadCount
                = allCustomHelipadPositionData.Count;
        }

        protected static void CreateNativeHelipadPositionDataFile(in List<Vector3> allNativePositions)
        {
            var helipadNativePositionDataSerialize
                = new XmlSerializer(typeof(List<Vector3>));

            using (var helipadNativePositionsFileStreamWrite = new StreamWriter(PathToTheNativePositionsXml))
            {
                helipadNativePositionDataSerialize
                    .Serialize(helipadNativePositionsFileStreamWrite, allNativePositions);

                helipadNativePositionsFileStreamWrite
                    .Flush();
            }
        }

        protected static void CreateCustomHelipadPositionDataFile(in List<Vector3> allCustomPositions)
        {
            var helipadCustomPositionDataSerialize
                = new XmlSerializer(typeof(List<Vector3>));

            using (var helipadCustomPositionsFileStreamWrite = new StreamWriter(PathToTheCustomPositionsDataXml))
            {
                helipadCustomPositionDataSerialize
                    .Serialize(helipadCustomPositionsFileStreamWrite, allCustomPositions);

                helipadCustomPositionsFileStreamWrite
                    .Flush();
            }
        }
    }
}
