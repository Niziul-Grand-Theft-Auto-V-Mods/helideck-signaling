using System.Drawing;

namespace Helideck_Signaling.user_interface.pop_up.settings.interfaces
{
    internal interface ISpriteGroups
    {
        PointF Position
        {
            get;
        }

        Color Color
        {
            get;
        }

        bool Enabled
        {
            get;
        }

        bool Centered
        {
            get;
        }

        void ScaledDrawAllSprites();

        void EnableAllSprites();

        void DisableAllSprites();
    }
}
