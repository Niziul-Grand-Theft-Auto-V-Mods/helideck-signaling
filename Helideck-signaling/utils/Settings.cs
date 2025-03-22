using GTA.Math;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Helideck_signaling.utils
{
    internal static class Settings
    {
        internal static string PathToTheHelideckSignalingFolder
        {
            get
            {
                return $@"{Directory.GetCurrentDirectory()}\scripts\HelideckSignaling";
            }
        }

        internal static string PathToTheHelideckSignalingXml
        {
            get
            {
                return $@"{PathToTheHelideckSignalingFolder}\HelideckSignaling.xml";
            }
        }

        internal static string PathToTheHelipadsInformationFolder
        {
            get
            {
                return $@"{PathToTheHelideckSignalingFolder}\HelipadsInformation";
            }
        }

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

        internal static void DeserializePositions(string path, out List<Vector3> allPosition)
        {
            var serializer
                = new XmlSerializer(typeof(List<Vector3>));

            using (var stream = new FileStream(path, FileMode.Open))
            {
                allPosition
                    = (List<Vector3>)serializer.Deserialize(stream);
            }
        }

        internal static void SerializerPositions(string path, in List<Vector3> allPositions)
        {
            var serializer
                = new XmlSerializer(typeof(List<Vector3>));

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer
                    .Serialize(stream, allPositions);

                stream
                    .Flush();
            }
        }
    }
}
