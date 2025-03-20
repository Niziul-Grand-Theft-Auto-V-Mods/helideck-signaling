using GTA;
using Helideck_signaling.script;

namespace Helideck_signaling
{
    internal sealed class Main : Script
    {
        private HelideckSignaling _helideckSignaling;

        public Main()
        {
            _helideckSignaling
                = InstantiateScript<HelideckSignaling>();

            Tick += (o, e) =>
            {
                switch (Game.LocalPlayerPed.IsInHeli)
                {
                    case true:
                        {
                            if (_helideckSignaling == null)
                            {
                                return;
                            }

                            _helideckSignaling
                                .CreateAllNativeHelipadBlip();

                            _helideckSignaling
                                .CreateAllCustomHelipadBlip();

                            if (_helideckSignaling.IsRunning)
                            {
                                return;
                            }

                            _helideckSignaling
                                .Resume();
                        };
                    break;
                    case false:
                        {
                            if (_helideckSignaling == null)
                            {
                                return;    
                            }

                            _helideckSignaling
                                .DeleteAllNativeHelipadBlip();

                            _helideckSignaling
                                .DeleteAllCustomHelipadBlip();

                            if (_helideckSignaling.IsPaused)
                            {
                                return;
                            }

                            _helideckSignaling
                                .Pause();
                        };
                    break;
                }
            };

            Aborted += (o, e) =>
            {
                if (_helideckSignaling == null)
                {
                    return;
                }

                _helideckSignaling
                    .Abort();

                _helideckSignaling
                    = null;
            };

            Interval = 500;
        }
    }
}