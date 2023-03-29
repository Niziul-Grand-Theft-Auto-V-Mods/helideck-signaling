using GTA;
using GTA.UI;

using System.Drawing;

using System.Collections.Generic;

using Helideck_Signaling.setup_manager;

using Helideck_Signaling.user_interface.pop_up.settings;

using Helideck_Signaling.user_interface.pop_up.settings.creators;


namespace Helideck_Signaling.user_interface.pop_up
{
    internal sealed class PopUp : Script
    {
        public PopUp()
        {
            var layoutGroupManager =
                new LayoutGroupManager();

            var layoutsGroupTop =
                layoutGroupManager
                                .LayoutsTop;

            var layoutsGroupBody =
                layoutGroupManager
                                .LayoutsBody;

            var layoutsGroupBottom =
                layoutGroupManager
                                .LayoutsBottom;

            var layoutsGroupTimer =
                layoutGroupManager
                                .LayoutsTimer;

            layoutsGroupTop
                .Add(new LayoutCreator(LayoutGroups
                                                .TOP));

            layoutsGroupBody
                .Add(new LayoutCreator(LayoutGroups
                                                .BODY));

            layoutsGroupBody
                .Add(new LayoutCreator(LayoutGroups
                                                .BODY, true));

            layoutsGroupBody
                .Add(new LayoutCreator(LayoutGroups
                                                .BODY, true));

            layoutsGroupBody
                .Add(new LayoutCreator(LayoutGroups
                                                .BODY, true));

            layoutsGroupBottom
                .Add(new LayoutCreator(LayoutGroups
                                                .BOTTOM));

            //layoutsGroupTimer
            //    .Add(new LayoutCreator(LayoutGroups
            //                                    .TIMER));

            ConfigureAsNotSelectedThis(element: 0, layout: layoutsGroupBody);
            ConfigureAsNotSelectedThis(element: 1, layout: layoutsGroupBody);
            ConfigureAsNotSelectedThis(element: 2, layout: layoutsGroupBody);
            ConfigureAsNotSelectedThis(element: 3, layout: layoutsGroupBody);

            var indexT = 0;

            Tick += (o, e) =>
            {
                layoutGroupManager
                    .ScaledDrawThis(layoutsGroupBottom);

                layoutGroupManager
                    .ScaledDrawThis(layoutsGroupBody);

                layoutGroupManager
                    .ScaledDrawThis(layoutsGroupTop);

                //layoutGroupManager
                //    .ScaledDrawThis(layoutsGroupTimer);
            };

            Aborted += (o, e) =>
            {
                layoutGroupManager
                        .DisposeAll();
            };

            KeyUp += (o, e) =>
            {
                switch (e.KeyCode)
                {
                    case System.Windows.Forms.Keys
                                                  .I:
                        {
                            layoutsGroupTop[0]
                                            .DisableAllSprites();
                        }
                        return;
                    case System.Windows.Forms.Keys
                                                  .U:
                        {
                            layoutsGroupTop[0]
                                            .EnableAllSprites();
                        }
                        return;
                    case System.Windows.Forms.Keys
                                                  .Oemplus:
                        {
                            if (layoutsGroupBody.Count == 0)
                            {
                                layoutsGroupBody
                                    .Add(new LayoutCreator(LayoutGroups
                                                                    .BODY));

                                ConfigureAsSelectedThis(element: 0, layout: layoutsGroupBody);
                            }
                            else
                            {
                                layoutsGroupBody
                                    .Add(new LayoutCreator(LayoutGroups
                                                                    .BODY, true));

                                ConfigureAsNotSelectedThis(element: (byte)(layoutsGroupBody.Count - 1), layout: layoutsGroupBody);
                            }
                        }
                        return;
                    case System.Windows.Forms.Keys
                                                  .OemMinus:
                        {
                            layoutsGroupBody
                                .RemoveAt(layoutsGroupBody
                                                        .Count - 1);
                        }
                        return;
                    case System.Windows.Forms.Keys
                                                  .Down:
                        {
                            if (layoutsGroupBody.Count > 0)
                            {
                                ConfigureAsNotSelectedThis((byte)(indexT), layoutsGroupBody);

                                indexT++;

                                if (indexT == layoutsGroupBody.Count)
                                    indexT = 0;

                                ConfigureAsSelectedThis((byte)indexT, layoutsGroupBody);
                            }
                        }
                        return;
                    case System.Windows.Forms.Keys
                                                  .Up:
                        {
                            if (layoutsGroupBody.Count > 0)
                            {
                                ConfigureAsNotSelectedThis((byte)(indexT), layoutsGroupBody);

                                indexT--;

                                if (indexT < 0)
                                    indexT = (byte)(layoutsGroupBody.Count - 1);

                                ConfigureAsSelectedThis((byte)indexT, layoutsGroupBody);
                            }
                        }
                        return;
                }
            };
        }

        private void ConfigureAsSelectedThis(byte element, IList<LayoutCreator> layout)
        {
            layout[element]
                .DisableAllSprites();

            layout[element]
                .EnableThis(spriteIndex: 0);

            layout[element]
                .EnableThis(spriteIndex: 1);

            layout[element]
                .EnableThis(spriteIndex: 3);

            layout[element]
                .EnableThis(spriteIndex: 5);

            layout[element]
                .EnableThis(spriteIndex: 7);

            layout[element]
                .EnableThis(spriteIndex: 8);
        }
        
        private void ConfigureAsNotSelectedThis(byte element, IList<LayoutCreator> layout)
        {
            layout[element]
                .DisableAllSprites();

            layout[element]
                .EnableThis(spriteIndex: 0);

            layout[element]
                .EnableThis(spriteIndex: 2);

            layout[element]
                .EnableThis(spriteIndex: 4);

            layout[element]
                .EnableThis(spriteIndex: 6);

            layout[element]
                .EnableThis(spriteIndex: 7);

            layout[element]
                .EnableThis(spriteIndex: 8);
        }
    }
}
