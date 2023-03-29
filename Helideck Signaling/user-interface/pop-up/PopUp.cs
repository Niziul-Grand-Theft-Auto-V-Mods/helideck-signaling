//using GTA;
//using GTA.UI;

//using System.Drawing;

//using System.Collections.Generic;

//using Helideck_Signaling.setup_manager;

//using Helideck_Signaling.user_interface.pop_up.settings;

//using Helideck_Signaling.user_interface.pop_up.settings.creators;


//namespace Helideck_Signaling.user_interface.pop_up
//{
//    internal sealed class PopUp : Script
//    {
//        public PopUp()
//        {
//            var layoutGroupManager =
//                new LayoutGroupManager();

//            var layoutsGroupTop =
//                layoutGroupManager
//                                .LayoutsTop;

//            var layoutsGroupBody =
//                layoutGroupManager
//                                .LayoutsBody;

//            var layoutsGroupBottom =
//                layoutGroupManager
//                                .LayoutsBottom;

//            var layoutsGroupTimer =
//                layoutGroupManager
//                                .LayoutsTimer;

//            var layoutsGroupAlert =
//                layoutGroupManager
//                                .LayoutsAlert;

//            layoutsGroupTop
//                .Add(new LayoutCreator(LayoutGroups
//                                                .TOP));

//            layoutsGroupBody
//                .Add(new LayoutCreator(LayoutGroups
//                                                .BODY));

//            layoutsGroupBody
//                .Add(new LayoutCreator(LayoutGroups
//                                                .BODY, true));

//            layoutsGroupBottom
//                .Add(new LayoutCreator(LayoutGroups
//                                                .BOTTOM));

//            layoutsGroupAlert
//                .Add(new LayoutCreator(LayoutGroups
//                                                .ALERT));

//            //layoutsGroupTimer
//            //    .Add(new LayoutCreator(LayoutGroups
//            //                                    .TIMER));

//            var indexT = 0;

//            Tick += (o, e) =>
//            {
//                layoutGroupManager
//                    .ScaledDrawThis(layoutsGroupAlert);

//                layoutGroupManager
//                    .ScaledDrawThis(layoutsGroupBottom);

//                layoutGroupManager
//                    .ScaledDrawThis(layoutsGroupBody);

//                layoutGroupManager
//                    .ScaledDrawThis(layoutsGroupTop);

//                //layoutGroupManager
//                //    .ScaledDrawThis(layoutsGroupTimer);
//            };

//            Aborted += (o, e) =>
//            {
//                layoutGroupManager
//                        .DisposeAll();
//            };

//            KeyUp += (o, e) =>
//            {
//                switch (e.KeyCode)
//                {
//                    case System.Windows.Forms.Keys
//                                                  .I:
//                        {
//                            //layoutsGroupTop[0]
//                            //                .DisableAllSprites();
//                        }
//                        return;
//                    case System.Windows.Forms.Keys
//                                                  .U:
//                        {
//                            //layoutsGroupTop[0]
//                            //                .EnableAllSprites();
//                        }
//                        return;
//                    case System.Windows.Forms.Keys
//                                                  .Oemplus:
//                        {
//                            if (layoutsGroupBody.Count == 0)
//                            {
//                                layoutsGroupBody
//                                    .Add(new LayoutCreator(LayoutGroups
//                                                                    .BODY));

//                                ConfigureAsSelectedThis(element: 0, layout: layoutsGroupBody);
//                            }
//                            else
//                            {
//                                layoutsGroupBody
//                                    .Add(new LayoutCreator(LayoutGroups
//                                                                    .BODY, true));

//                                ConfigureAsNotSelectedThis(element: (byte)(layoutsGroupBody.Count - 1), layout: layoutsGroupBody);
//                            }
//                        }
//                        return;
//                    case System.Windows.Forms.Keys
//                                                  .OemMinus:
//                        {
//                            layoutsGroupBody
//                                .RemoveAt(layoutsGroupBody
//                                                        .Count - 1);
//                        }
//                        return;
//                    case System.Windows.Forms.Keys
//                                                  .Down:
//                        {
//                            if (layoutsGroupBody.Count != 0)
//                            {
//                                ConfigureAsNotSelectedThis((byte)(indexT), layoutsGroupBody);

//                                indexT++;

//                                if (indexT == layoutsGroupBody.Count)
//                                    indexT = 0;

//                                ConfigureAsSelectedThis((byte)indexT, layoutsGroupBody);
//                            }
//                        }
//                        return;
//                    case System.Windows.Forms.Keys
//                                                  .Up:
//                        {
//                            if (layoutsGroupBody.Count != 0)
//                            {
//                                ConfigureAsNotSelectedThis((byte)(indexT), layoutsGroupBody);

//                                indexT--;

//                                if (indexT < 0)
//                                    indexT = (byte)(layoutsGroupBody.Count - 1);

//                                ConfigureAsSelectedThis((byte)indexT, layoutsGroupBody);
//                            }
//                        }
//                        return;
//                    case System.Windows.Forms.Keys.NumPad5:
//                        {
//                            Pause();
//                        }
//                        return;
//                }
//            };
//        }

//        private void ConfigureAsSelectedThis(byte element, IList<LayoutCreator> layout)
//        {
//            layout[element]
//                .DisableThisSpriteInThis(index: 2);

//            layout[element]
//                .EnableThisSpriteInThis(index: 1);
//        }
//        private void ConfigureAsNotSelectedThis(byte element, IList<LayoutCreator> layout)
//        {
//            layout[element]
//                .DisableThisSpriteInThis(index: 1);

//            layout[element]
//                .EnableThisSpriteInThis(index: 2);
//        }
//    }
//}
