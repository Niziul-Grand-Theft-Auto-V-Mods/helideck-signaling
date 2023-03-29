using GTA;
using GTA.Math;
using Helideck_Signaling.blip_creator;

namespace Helideck_Signaling
{
    public class Main : Script
    {
        private CreateBlip HelipadsBlips =
            new CreateBlip();

        private bool areBlipsOnTheMap =
            false;

        private readonly Vector3[] positionOfHelipads =
        {
            new Vector3(-75.0856934f, -819.14386f, 326.169922f),
            new Vector3(-475.361755f, 5988.62842f, 30.3381233f),
            new Vector3(2140.58301f, 4819.85693f, 40.2424278f),
            new Vector3(1770.33789f, 3239.38916f, 41.1409264f),
            new Vector3(2510.70776f, -342.108948f, 117.187805f),
            new Vector3(2511.36279f, -426.733276f, 117.190147f),
            new Vector3(449.347107f, -981.132996f, 42.6930847f),
            new Vector3(481.869446f, -982.239624f, 40.0095177f),
            new Vector3(910.4505f, -1681.3468f, 50.1335144f),
            new Vector3(362.812103f, -1598.28186f, 35.9502373f),
            new Vector3(313.1763f, -1465.09253f, 45.5109367f),
            new Vector3(299.201965f, -1453.38647f, 45.5109367f),
            new Vector3(-144.464355f, -593.464966f, 210.776611f),
            new Vector3(-286.335968f, -618.134583f, 49.3393173f),
            new Vector3(-1391.49451f, -477.454987f, 90.2572632f),
            new Vector3(-1581.92261f, -569.445129f, 115.334038f),
            new Vector3(-1219.6731f, -832.077759f, 28.4144821f),
            new Vector3(-1095.55115f, -834.919495f, 36.6768074f),
            new Vector3(-735.308044f, -1456.47986f, 4.00194979f),
            new Vector3(-1178.36938f, -2846.06885f, 12.9471722f),
            new Vector3(-1146.01917f, -2864.81543f, 12.9474392f),
            new Vector3(-1112.55347f, -2884.27222f, 12.9474373f),
            new Vector3(478.457397f, -3369.92749f, 5.07133722f),
            new Vector3(-1859.73132f, 2795.22998f, 31.8080139f),
            new Vector3(-1877.10583f, 2805.39771f, 31.808012f),
            new Vector3(476.561188f, -1106.66003f, 43.075634f),
            new Vector3(579.831238f, 12.6151848f, 103.228325f),
            new Vector3(-582.899902f, -930.494446f, 36.8335571f),
            new Vector3(352.0242f, -588.054321f, 74.4528046f)
        };


        public Main()
        {
            Tick += (o, e) =>
            { Start(); };

            Aborted += (o, e) =>
            { End(); };
        }

        private void End()
        {
            WipeBlips();
        }
        private void Start()
        {
            if (Game.Player.Character.IsInHeli)
            {
                if (!areBlipsOnTheMap)
                {
                    AddBlipsOnTheMap();
                }
                MakeBlipVisibleAtLongDistancesOnTheMinimap();
            }
            else
            {
                WipeBlips();
            }
        }

        private void WipeBlips()
        {
            if (!HelipadsBlips.IsEmpty())
            {
                HelipadsBlips.Delete();
                areBlipsOnTheMap = false;
            }
        }
        private void AddBlipsOnTheMap()
        {
            HelipadsBlips =
                new CreateBlip(positionOfHelipads);

            areBlipsOnTheMap = true;
        }
        private void MakeBlipVisibleAtLongDistancesOnTheMinimap()
        {
            var isIheWaypointInUse = World.WaypointBlip != null;

            if (isIheWaypointInUse)
                HelipadsBlips.MakeTheHelipadBlipVisibleOnTheMinimapAtLongDistances();
            else
                HelipadsBlips.MakeTheHelipadBlipInvisible();
        }
    }
}