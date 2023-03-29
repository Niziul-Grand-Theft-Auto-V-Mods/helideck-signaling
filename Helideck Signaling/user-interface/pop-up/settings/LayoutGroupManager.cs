using System.Collections.Generic;

using Helideck_Signaling.user_interface.pop_up.settings.creators;


namespace Helideck_Signaling.user_interface.pop_up.settings
{
    internal sealed class LayoutGroupManager
    {
        internal IList<LayoutCreator> LayoutsTop 
        { get; private set; }

        internal IList<LayoutCreator> LayoutsBody 
        { get; private set; }

        internal IList<LayoutCreator> LayoutsBottom
        { get; private set; }

        internal IList<LayoutCreator> LayoutsTimer 
        { get; private set; }

        internal IList<LayoutCreator> LayoutsAlert 
        { get; private set; }


        public LayoutGroupManager()
        {
            LayoutsTop =
                new List<LayoutCreator>();

            LayoutsBody =
                new List<LayoutCreator>();

            LayoutsBottom =
                new List<LayoutCreator>();

            LayoutsTimer =
                new List<LayoutCreator>();

            LayoutsAlert =
                new List<LayoutCreator>();
        }

        internal void ScaledDrawAll()
        {
            foreach (var listOfLayouts in ReturnListsOfLayouts())
            {
                ScaledDrawThis(listOfLayouts);
            }
        }
        internal void ScaledDrawThis(IList<LayoutCreator> listOfLayouts)
        {
            foreach (var layout in listOfLayouts)
            {
                layout
                    .ScaledDrawAllSprites();
            }
        }


        internal void DisposeAll()
        {
            foreach (var listOfLayouts in ReturnListsOfLayouts())
            {
                DisposeThis(listOfLayouts);
            }
        }
        internal void DisposeThis(IList<LayoutCreator> listOfLayouts)
        {
            foreach (var layout in listOfLayouts)
            {
                layout
                    .DisposeAllSprites();
            }
        }

        private IList<LayoutCreator>[] ReturnListsOfLayouts()
        {
            return new[]
            {
                LayoutsTop,
                LayoutsBody,
                LayoutsBottom,
                LayoutsTimer,
                LayoutsAlert
            };
        }
    }
}
