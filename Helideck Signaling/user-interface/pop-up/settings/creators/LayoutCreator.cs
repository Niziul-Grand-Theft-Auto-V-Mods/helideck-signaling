using GTA.UI;

using System.Drawing;

using System.Collections.Generic;

using Helideck_Signaling.setup_manager;

using Helideck_Signaling.user_interface.pop_up.settings.interfaces;


namespace Helideck_Signaling.user_interface.pop_up.settings.creators
{
    internal sealed class LayoutCreator : SpriteCreator, ISpriteGroups
    {
        private readonly IList<Sprite> _spriteList;


        private static PointF _positionForTheNextLayoutGroup;

        private static int _topCount = 0;

        private static int _bodyCount = 0;

        private static int _bottomCount = 0;

        private static int _timerCount = 0;

        private static int _alertCount = 0;


        public PointF Position
        { get; private set; }
        
        public Color Color
        { get; private set; }
        
        public bool Enabled
        { get; private set; }

        public bool Centered
        { get; private set; }


        public LayoutCreator(LayoutGroups layoutGroup) 
            :this(layoutGroup, false)
        {
        }
        public LayoutCreator(LayoutGroups layoutGroup, bool multiPilha)
        {
            _spriteList =
                ReturnOneListOfSpritesFromThis(layoutGroup);

            var firstSprite =
                _spriteList[0];

            Position =
                firstSprite
                    .Position;

            Color =
                firstSprite
                    .Color;

            Enabled =
                firstSprite
                    .Enabled;

            Centered =
                firstSprite
                    .Centered;


            switch (layoutGroup)
            {
                case LayoutGroups
                                .BODY:
                    {
                        if (multiPilha)
                        {
                            for (var i = 0; i < _spriteList.Count; i++)
                            {
                                var sprite =
                                    _spriteList[i];

                                var position =
                                    sprite
                                        .Position;

                                if (i == 0)
                                {
                                    sprite
                                        .Position = new PointF(position.X,
                                                               position.Y + sprite
                                                                                .Size
                                                                                    .Height * _bodyCount);

                                    Position =
                                        sprite
                                            .Position;

                                    continue;
                                }

                                sprite
                                    .Position = new PointF(position.X,
                                                           position.Y + Position.Y);
                            }

                            _bodyCount++;
                        }
                        else
                        {
                            for (var i = 0; i < _spriteList.Count; i++)
                            {
                                if (i == 0)
                                {
                                    continue;
                                }

                                var sprite =
                                    _spriteList[i];

                                var position =
                                    sprite
                                        .Position;

                                sprite
                                    .Position = new PointF(position.X,
                                                           position.Y + Position.Y);
                            }

                            if (_bodyCount == 0)
                            {
                                _bodyCount++;
                            }
                            else if (_bodyCount > 0)
                            {
                                _bodyCount = 0;
                            }
                        }
                    }
                    break;
                case LayoutGroups
                                .BOTTOM:
                    {
                        for (var i = 0; i < _spriteList.Count; i++)
                        {
                            var sprite =
                                _spriteList[i];

                            var position =
                                sprite
                                    .Position;

                            sprite
                                .Position = new PointF(position.X,
                                                       _positionForTheNextLayoutGroup.Y - 1.5f);
                        }
                    }
                    break;
                case LayoutGroups
                                .TIMER:
                    {
                        for (var i = 0; i < _spriteList.Count; i++)
                        {
                            var sprite =
                                _spriteList[i];

                            var position =
                                sprite
                                    .Position;

                            sprite
                                .Position = new PointF(position.X,
                                                       _positionForTheNextLayoutGroup.Y);
                        }
                    }
                    break;
                case LayoutGroups
                                .ALERT:
                    {

                    }
                    break;
            }


            _positionForTheNextLayoutGroup = 
                firstSprite
                    .Position + firstSprite
                                        .Size;
        }


        public void EnableAllSprites()
        {
            EnableThis();
        }
        private void EnableThis()
        {
            for (var i = (byte)0; i < _spriteList.Count; i++)
                EnableThis(spriteIndex: i);
        }
        internal void EnableThis(byte spriteIndex)
        {
            if (spriteIndex > _spriteList.Count)
                return;

            var sprite =
                _spriteList[spriteIndex];

            sprite
                .Enabled = true;
        }


        public void DisableAllSprites()
        {
            DisableThis();
        }
        private void DisableThis()
        {
            for (var i = (byte)0; i < _spriteList.Count; i++)
            {
                DisableThis(spriteIndex: i);
            }
        }
        internal void DisableThis(byte spriteIndex)
        {
            if (spriteIndex > _spriteList.Count)
                return;

            var sprite =
                _spriteList[spriteIndex];

            sprite
                .Enabled = false;
        }


        public void ScaledDrawAllSprites()
        {
            if (Enabled)
            {
                foreach (var sprite in _spriteList)
                {
                    ScaledDrawThis(sprite);
                }
            }
        }
        internal void ScaledDrawThis(Sprite sprite)
        {
            sprite
                .ScaledDraw();
        }

        public void DisposeAllSprites()
        {
            foreach (var sprite in _spriteList)
            {
                DisposeThis(sprite);
            }
        }
        internal void DisposeThis(Sprite sprite)
        {
            sprite
                .Dispose();
        }
    }
}
