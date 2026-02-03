#include "script.hpp"

BOOL APIENTRY DllMain(HMODULE hInstance, DWORD reason, LPVOID lpReserved)
{
    switch (reason)
    {
    case DLL_PROCESS_ATTACH:
        // Logger::Init("ArcadeSP.log");
        scriptRegister(hInstance, script_main);
        // keyboardHandlerRegister(OnKeyboardMessage);
        break;
    case DLL_PROCESS_DETACH:
        // Logger::Destroy();
        scriptUnregister(hInstance);
        // keyboardHandlerUnregister(OnKeyboardMessage);
        break;
    }

    return TRUE;
}
