using GTA.Math;
using Helideck_signaling.features.custom_helipad_blip.creators;
using System.Collections.Generic;

namespace Helideck_signaling.features.custom_helipad_blip.managers
{
    internal abstract class CustomHelipadBlipManager
    {
        private static readonly IList<CustomHelipadBlipCreator> _nativeBlips
            = new List<CustomHelipadBlipCreator>();

        private static readonly IList<CustomHelipadBlipCreator> _customBlips
            = new List<CustomHelipadBlipCreator>();

        internal void CreateAllNativeBlipsAtThisPositions(in IList<Vector3> positions)
        {
            foreach (var position in positions)
            {
                _nativeBlips
                    .Add(new CustomHelipadBlipCreator(in position));
            }
        }
        internal void CreateAllCustomBlipsAtThisPositions(in IList<Vector3> positions)
        {
            foreach (var position in positions)
            {
                _customBlips
                    .Add(new CustomHelipadBlipCreator(in position));
            }
        }
        
        internal void EnableAllNativeBlips()
        {
            foreach (var nativeBlip in _nativeBlips)
            {
                nativeBlip
                    .Enable();
            }
        }
        internal void DisableAllNativeBlips()
        {
            foreach (var nativeBlip in _nativeBlips)
            {
                nativeBlip
                    .Disable();
            }
        }

        internal void EnableAllCustomBlips()
        {
            foreach (var customBlip in _customBlips)
            {
                customBlip
                    .Enable();
            }
        }
        internal void DisableAllCustomBlips()
        {
            foreach (var customBlip in _customBlips)
            {
                customBlip
                    .Disable();
            }
        }

        internal void DeleteAllNativeBlips()
        {
            foreach (var nativeBlip in _nativeBlips)
            {
                nativeBlip
                    .Delete();
            }

            _nativeBlips
                .Clear();
        }
        internal void DeleteAllCustomBlips()
        {
            foreach (var customBlip in _customBlips)
            {
                customBlip
                    .Delete();
            }

            _customBlips
                .Clear();
        }
        
        internal bool AreAllNativeBlipsCreated()
        {
            return _nativeBlips.Count > 0;
        }
        internal bool AreAllCustomBlipsCreated()
        {
            return _customBlips.Count > 0;
        }
    }
}
