using GTA;
using GTA.Math;
using GTA.UI;
using Helideck_signaling.script.features.custom_blip;
using Helideck_signaling.script.features.helipad_blip.managers;
using Helideck_signaling.utils;
using System.Collections.Generic;
using System.IO;

namespace Helideck_signaling.script.features.helipad_blip
{
    internal class HelipadBlip : HelipadBlipManager
    {
        private readonly static CustomBlip _customHelipadBlip
            = new CustomBlip();

        public HelipadBlip()
        {
            if (!File.Exists(PathToTheNativePositionsXml))
            {
                NativeHelipadPositions
                    = new List<Vector3>
                    {
                        /* 
                            Pillbox Hill
                        */
                        new Vector3(-75.08569f, -819.1439f, 326.1699f),
                        new Vector3(-144.4644f, -593.465f, 210.7766f),

                        /* 
                            Paleto Forest
                        */
                        new Vector3(-475.3618f, 5988.628f, 30.33812f),

                        /* 
                            Grapeseed
                        */
                        new Vector3(2140.583f, 4819.857f, 40.24243f),

                        /* 
                            Deserto Grand Senora
                        */
                        new Vector3(1770.338f, 3239.389f, 41.14093f),

                        /* 
                            N.O.O.S.E
                        */
                        new Vector3(2510.708f, -342.1089f, 117.1878f),
                        new Vector3(2511.363f, -426.7333f, 117.1901f),

                        /* 
                            Mission Row
                        */
                        new Vector3(449.3471f, -981.133f, 42.69308f),
                        new Vector3(481.8694f, -982.2396f, 40.00952f),
                        new Vector3(476.5612f, -1106.66f, 43.07563f),

                        /* 
                            La Mesa
                        */
                        new Vector3(910.4505f, -1681.347f, 50.13351f),

                        /* 
                            Rockford Hills
                        */
                        new Vector3(-913.53f, -378.311f, 135.0652f),

                        /* 
                            Rancho
                        */
                        new Vector3(362.8121f, -1598.282f, 35.95024f),

                        /* 
                            Davis
                        */
                        new Vector3(313.1763f, -1465.093f, 45.51094f),
                        new Vector3(299.202f, -1453.386f, 45.51094f),

                        /* 
                            Centro
                        */
                        new Vector3(-286.4591f, -618.125f, 47.45354f),

                        /* 
                            Del Perro
                        */
                        new Vector3(-1391.69f, -477.6565f, 88.41081f),
                        new Vector3(-1582.09f, -569.5357f, 113.4491f),
                        new Vector3(-1219.673f, -832.0778f, 28.41448f),

                        /* 
                            Vespucci Canals
                        */
                        new Vector3(-1095.551f, -834.9195f, 36.67681f),

                        /* 
                            La Puerta
                        */
                        new Vector3(-745.1007f, -1434.14f, 5.367467f),
                        new Vector3(-761.9567f, -1454.142f, 5.36609f),
                        new Vector3(-701.4378f, -1446.713f, 5.367623f),
                        new Vector3(-722.9951f, -1472.082f, 5.36568f),
                        new Vector3(-723.7492f, -1442.956f, 4.747449f),
                        new Vector3(-745.7048f, -1468.901f, 4.74284f),

                        /* 
                            Aeroporto Internacional De Los Santos
                        */
                        new Vector3(-1178.369f, -2846.069f, 12.94717f),
                        new Vector3(-1146.019f, -2864.815f, 12.94744f),
                        new Vector3(-1112.553f, -2884.272f, 12.94744f),

                        /* 
                            Elysian Island
                        */
                        new Vector3(478.4574f, -3369.927f, 5.071337f),

                        /* 
                            Fort Zancudo
                        */
                        new Vector3(-1859.731f, 2795.23f, 31.80801f),
                        new Vector3(-1877.106f, 2805.398f, 31.80801f),

                        /* 
                            Downtown Vinewood
                        */
                        new Vector3(579.8312f, 12.61518f, 103.2283f),

                        /* 
                            Little Seoul
                        */
                        new Vector3(-582.8999f, -930.4944f, 36.83356f),

                        /* 
                            Textile City
                        */
                        new Vector3(352.0242f, -588.0543f, 74.4528f),
                    };

                SaveAllNewNativeHelipadPositionsToTheDataFile();
            }

            if (!File.Exists(PathToTheCustomPositionsDataXml))
            {
                SaveAllNewCustomHelipadPositionsToTheDataFile();
            }
        }

        internal void CreateAllNativeHelipadBlip()
        {
            if (_customHelipadBlip
                    .AreAllNativeBlipsCreated())
            {
                return;
            }
            
            _customHelipadBlip
                .CreateAllNativeBlipsAtThisPositions(NativeHelipadPositions);

            Notification
                .PostTickerWithTokens($"NativeHelipadCount: ~b~ {NativeHelipadCount}~w~", true);
        }
        internal void CreateAllCustomHelipadBlip()
        {
            if (_customHelipadBlip
                    .AreAllCustomBlipsCreated())
            {
                return;
            }

            _customHelipadBlip
                .CreateAllCustomBlipsAtThisPositions(CustomHelipadPositions);

            Notification
                .PostTickerWithTokens($"CustomHelipadCount: ~g~ {CustomHelipadCount}~w~", true);
        }

        internal void DeleteAllNativeHelipadBlip()
        {
            if (!_customHelipadBlip
                    .AreAllNativeBlipsCreated())
            {
                return;
            }

            _customHelipadBlip
                .DeleteAllNativeBlips();
        }
        internal void DeleteAllCustomHelipadBlip()
        {
            if (!_customHelipadBlip
                    .AreAllCustomBlipsCreated())
            {
                return;
            }

            foreach (var building in World.GetAllBuildings())
            {
                if (building.Model == "prop_helipad_01"
                    ||
                    building.Model == "prop_helipad_02")
                {
                    if (!CustomHelipadPositions.Contains(building.Position)
                        &&
                        !NativeHelipadPositions.Contains(building.Position))
                    {
                        CustomHelipadPositions
                            .Add(building.Position);
                    }
                }
            }

            foreach (var prop in World.GetAllProps())
            {
                if (prop.Model == "prop_helipad_01"
                    ||
                    prop.Model == "prop_helipad_02")
                {
                    if (!CustomHelipadPositions.Contains(prop.Position)
                        &&
                        !NativeHelipadPositions.Contains(prop.Position))
                    {
                        CustomHelipadPositions
                            .Add(prop.Position);
                    }
                }
            }

            if (CustomHelipadPositions.Count > CustomHelipadCount)
            {
                SaveAllNewCustomHelipadPositionsToTheDataFile();
            }
            
            _customHelipadBlip
                .DeleteAllCustomBlips();
        }

        internal void SetAllNativeHelipadPosition()
        {
            if (AreAllNativeBlipsPositionsSet())
            {
                return;
            }

            SetAllNativeHelipadPositionData(out NativeHelipadPositions);

            Singleton.NativeHelipadPosition = NativeHelipadPositions;
        }
        internal void SetAllCustomHelipadPosition()
        {
            if (AreAllCustomBlipsPositionsSet())
            {
                return;
            }

            SetAllCustomHelipadPositionData(out CustomHelipadPositions);

            Singleton.CustomHelipadPosition = CustomHelipadPositions;
        }

        internal IList<Vector3> GetAllNativeHelipadPosition()
        {
            return NativeHelipadPositions.AsReadOnly();
        }
        internal IList<Vector3> GetAllCustomHelipadPosition()
        {
            return CustomHelipadPositions.AsReadOnly();
        }

        protected void SaveAllNewNativeHelipadPositionsToTheDataFile()
        {
            CreateNativeHelipadPositionDataFile(NativeHelipadPositions);
        }
        protected void SaveAllNewCustomHelipadPositionsToTheDataFile()
        {
            CreateCustomHelipadPositionDataFile(CustomHelipadPositions);
        }
    }
}
