using System.IO;

namespace Helideck_signaling.settings.creators
{
    internal abstract class SettingsCreator
    {
        protected static string PathToTheHelideckSignalingFolder
        {
            get
            {
                return $@"{Directory.GetCurrentDirectory()}\scripts\HelideckSignaling";
            }
        }

        protected static string PathToTheHelideckSignalingXml
        {
            get
            {
                return $@"{PathToTheHelideckSignalingFolder}\HelideckSignaling.xml";
            }
        }
    }
}
