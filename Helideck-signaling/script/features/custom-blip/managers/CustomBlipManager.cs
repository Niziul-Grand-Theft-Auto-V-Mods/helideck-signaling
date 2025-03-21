using GTA.Math;
using Helideck_signaling.script.features.custom_blip.creators;
using System.Collections.Generic;

namespace Helideck_signaling.script.features.custom_blip.managers
{
    internal abstract class CustomBlipManager
    {
        private static readonly IList<CustomBlipCreator> _nativeBlips
            = new List<CustomBlipCreator>();

        private static readonly IList<CustomBlipCreator> _customBlips
            = new List<CustomBlipCreator>();

        internal void CreateAllNativeBlipsAtThisPositions(in IList<Vector3> positions)
        {
            foreach (var position in positions)
            {
                _nativeBlips
                    .Add(new CustomBlipCreator(in position));
            }
        }
        internal void CreateAllCustomBlipsAtThisPositions(in IList<Vector3> positions)
        {
            foreach (var position in positions)
            {
                _customBlips
                    .Add(new CustomBlipCreator(in position));
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
