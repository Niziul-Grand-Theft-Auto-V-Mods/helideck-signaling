using GTA;

using Helideck_Signaling.blip_creator;


namespace Helideck_Signaling
{
    internal sealed class Main : Script
    {
        private bool _areThereBlipsOnTheMap;
        
        private BlipCreator _blipCreator;
        
        private Ped _player;


        public Main()
        {
            _areThereBlipsOnTheMap =
                false;
            
            _player =
                Game
                    .Player
                        .Character;

            _blipCreator =
                new BlipCreator();

            Tick    += (o, e) =>
            { Start(); };

            Aborted += (o, e) =>
            { Finish(); };
        }
 
        private void Start()
        {
            var playerIsInVehicleWithRotatingWings =
                _player
                    .IsInHeli;

            switch (playerIsInVehicleWithRotatingWings)
            {
                case true:
                    {
                        BlipBehaviors();
                    }
                    return;
                case false:
                    {
                        EntersStandbyAndManagesOldBlips();
                    }
                    return;
            }
        }
        private void Finish()
        {
            if (_blipCreator != null)
                DeleteOldBlips();
        }


        private void BlipBehaviors()
        {
            var isVehicleInFlight =
                _player
                    .CurrentVehicle
                        .IsInAir;

            switch (isVehicleInFlight)
            {
                case true:
                    {
                        FlightOperation();
                    }
                    break;
                case false:
                    {
                        if (!_areThereBlipsOnTheMap)
                            PreparationForFlight();

                        VisibilityOfTheBlipOnMinimap();
                    }
                    break;
            }
        }
        private void FlightOperation()
        {
            if (!_blipCreator.IsEmpty())
            {
                _blipCreator
                    .ClearsMapLeavingOnlyTheSelectedBlips();


                var isTheWaypointInUse =
                    World
                        .WaypointBlip != null;

                if (isTheWaypointInUse)
                    LandingZoneControl();
            }

            _areThereBlipsOnTheMap = 
                false;
        }
        private void PreparationForFlight()
        {
            var areThereBlipsFromThePreviousOperationOnMap =
                !_blipCreator
                    .IsEmpty();

            if (areThereBlipsFromThePreviousOperationOnMap)
                DeleteOldBlips();

            DisplayAvailableBlipsOnMap();
        }

        private void LandingZoneControl()
        {
            if (PlayerIsNearTheLandingZone())
            {
                var vehicleOnTheLandingArea =
                    VehicleOnTheLandingArea();

                var hasVehicleOnTheLandingArea =
                    vehicleOnTheLandingArea != null;

                switch (hasVehicleOnTheLandingArea)
                {
                    case true:
                        {
                            var vehicleBelongsToThePlayer =
                                vehicleOnTheLandingArea == _player
                                                                .CurrentVehicle;

                            if (vehicleBelongsToThePlayer)
                            {
                                return;
                            }
                            else
                            {

                                if (vehicleOnTheLandingArea
                                        .IsHelicopter &&
                                    vehicleOnTheLandingArea
                                        .IsStopped    &&
                                   !vehicleOnTheLandingArea
                                        .IsInAir)
                                {
                                    GTA.UI
                                        .Notification
                                            .Show($"~b~{vehicleOnTheLandingArea.DisplayName}~w~ - deleted!");
                                    
                                    vehicleOnTheLandingArea
                                        .Delete();
                                    
                                }
                            }
                        }
                        return;
                }
            }
        }
        private bool PlayerIsNearTheLandingZone()
        {
            var playerPosition =
                _player
                    .Position;

            var waypointPosition =
                World
                    .WaypointPosition;

            var distanceToTheLandingArea =
                playerPosition
                    .DistanceTo2D(waypointPosition);

            var areaForLandingOperations =
                150f;

            return distanceToTheLandingArea < areaForLandingOperations;
        }
        private Vehicle VehicleOnTheLandingArea()
        {
            var waypointPosition =
                    World
                        .WaypointPosition;

            return World
                    .GetClosestVehicle(waypointPosition,
                                       10f);
        }

        private void EntersStandbyAndManagesOldBlips()
        {
            if (!_blipCreator.IsEmpty())
                DeleteOldBlips();
        }
        private void DeleteOldBlips()
        {
            _blipCreator
                .Delete();

            _areThereBlipsOnTheMap =
                false;
        }
        private void DisplayAvailableBlipsOnMap()
        {
            _blipCreator
                .CreateAllAvailableBlips();

            _areThereBlipsOnTheMap =
                true;
        }
        private void VisibilityOfTheBlipOnMinimap()
        {
            var isTheWaypointInUse =
                World
                    .WaypointBlip != null;

            if (isTheWaypointInUse)
            {
                _blipCreator
                    .IncreaseBlipVisibility();
            }
            else
            {
                _blipCreator
                    .ResetBlipVisibility();
            }
        }
    }
}