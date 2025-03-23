#include "../inc/util/natives.hpp"
#include "../inc/util/hashing.hpp"
#include "../inc/util/post_ticker.hpp"
#include "../build/_deps/toml11-src/include/toml.hpp"
#include <filesystem>

auto static const log_path = std::filesystem::current_path() / "Helideck-signaling" / "log.txt";
auto static const config_path = std::filesystem::current_path() / "Helideck-signaling" / "config.toml";

void script_main()
{
    constexpr auto hash_a
        = joaat(/* check close helipad */ "cch");

    constexpr auto hash_b
        = joaat(/* check config file */ "ccf");

    while (true)
    {
        auto player
            = PLAYER::PLAYER_ID();
        auto playerPed
            = PLAYER::PLAYER_PED_ID();

        if (DLC::GET_IS_LOADING_SCREEN_ACTIVE()
            ||
            !ENTITY::DOES_ENTITY_EXIST(playerPed)
            ||
            !PLAYER::IS_PLAYER_CONTROL_ON(player))
        {
            WAIT(0);
            continue;
        }

        if (PED::IS_PED_INJURED(playerPed)
            ||
            PLAYER::IS_PLAYER_BEING_ARRESTED(player, TRUE))
        {
            WAIT(0);
            continue;
        }

        if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(hash_a))
        {
            /*auto log*/
            /*    = std::ofstream*/
            /*    {*/
            /*        log_path.c_str(),*/
            /*        std::ios_base::trunc,*/
            /*    };*/
            /**/
            /*log.close();*/

            postTickerWithTokens("~b~cch~w~ - check close helipad", true, false);
        }

        if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(hash_b))
        {
            auto config
                = toml::parse(config_path);

            auto helipad_models
                = toml::find<std::vector<std::string>>(config, "models");

            auto is_to_enable_native_helipad
                = toml::find<bool>(config, "native_helipad_blip", "enable");

            auto is_to_enable_custom_helipad
                = toml::find<bool>(config, "custom_helipad_blip", "enable");

            char buffer_a[64];
            char buffer_b[64];

            sprintf_s(buffer_a, "is_to_enable_native_helipad: ~y~%s~w~", is_to_enable_native_helipad ? "true" : "false");

            sprintf_s(buffer_b, "is_to_enable_custom_helipad: ~y~%s~w~", is_to_enable_custom_helipad ? "true" : "false");

            postTickerWithTokens(buffer_a, true, false);

            WAIT(1000);

            postTickerWithTokens(buffer_b, true, false);

            WAIT(1000);

            for(auto models : helipad_models)
            {
                postTickerWithTokens(models.c_str(), true, false);

                WAIT(500);
            }

            postTickerWithTokens("~b~ccf~w~ - check config file", true, false);
        }

        WAIT(0);
    }

    return;
}

BOOL APIENTRY DllMain(HMODULE h_module,
                      DWORD ul_reason_for_call)
{
    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
        {
            scriptRegister(h_module, script_main);
        }
        break;
        case DLL_PROCESS_DETACH:
        {
            scriptUnregister(h_module);
        }
        break;
    }
    return TRUE;
}
