using GTA.UI;

using System.Drawing;

using System.Collections.Generic;

using Helideck_Signaling.setup_manager;


namespace Helideck_Signaling.user_interface.pop_up.settings.creators
{
    internal abstract class SpriteCreator
    {
        protected IList<Sprite> ReturnOneListOfSpritesFromThis(LayoutGroups layoutGroup)
        {
            var settings =
                new Settings();

            var texturesName =
                settings
                    .ReturnAllTextureNamesIn(layoutGroup);

            var relativePositions =
                settings
                    .ReturnAllRelativePositionsIn(layoutGroup);

            var sprites =
                new List<Sprite>();

            for (var i = (byte)0; i < relativePositions.Count; i++)
            {
                sprites
                    .Add(item: CreateSpriteWithThis(texturesName[i],
                                                    relativePositions[i],
                                                    layoutGroup));
            }

            return sprites;
        }
        private Sprite CreateSpriteWithThis(string textureName, PointF position, LayoutGroups layoutGroup)
        {
            var textureDict =
                "niziul_popUp_resources";

            var settings =
                new Settings();

            var size =
                settings
                    .ReturnSizeOfThis(textureName);

            var customSize =
                new SizeF(size.Width / 4f,
                          size.Height / 4f);

            var positions =
                position;

            return new Sprite(textureDict,
                              textureName,
                              customSize,
                              positions) { Centered = true };
        }
    }
}
