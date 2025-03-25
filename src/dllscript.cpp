#include "../inc/util/natives.hpp"
#include "../inc/util/hashing.hpp"
#include "../inc/util/post_ticker.hpp"
#include "../build/_deps/toml11-src/include/toml.hpp"
#include <filesystem>

auto static const log_path
    = std::filesystem::current_path() / "Helideck-signaling" / "log.toml";
auto static const config_path
    = std::filesystem::current_path() / "Helideck-signaling" / "config.toml";

// check close helipad
constexpr auto cheat_a
    = joaat("cch");
// check parse config file
constexpr auto cheat_b
    = joaat("cpcf");
// check format log file
constexpr auto cheat_c
    = joaat("cflf");
// check parse log file
constexpr auto cheat_d
    = joaat("cplf");

constexpr auto cheat_0x1A
    = joaat("spolmav");
constexpr auto cheat_0x2A
    = joaat("srafaleb");
constexpr auto cheat_0x3A
    = joaat("smd500civ");

// get number of dlc vehicles
constexpr auto cheat_0x1B
    = joaat("gndv");

// get first vehicle model hash
constexpr auto cheat_0x2B
    = joaat("gfvmh");

void cheat_to_check_close_helipad()
{
    if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_a))
    {
        postTickerWithTokens("~b~cch~w~ - check close helipad", true, false);
    }
}

void cheat_to_check_parse_config_file()
{
    if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_b))
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

        sprintf_s(buffer_a,
                  "is_to_enable_native_helipad: ~y~%s~w~",
                  is_to_enable_native_helipad ? "true" : "false");

        sprintf_s(buffer_b,
                  "is_to_enable_custom_helipad: ~y~%s~w~",
                  is_to_enable_custom_helipad ? "true" : "false");

        postTickerWithTokens(buffer_a, true, false);

        WAIT(1000);

        postTickerWithTokens(buffer_b, true, false);

        WAIT(1000);

        for (auto models : helipad_models)
        {
            postTickerWithTokens(models.c_str(), true, false);

            WAIT(500);
        }

        postTickerWithTokens("~b~cpcf~w~ - check parse config file", true, false);
    }
}
void cheat_to_check_format_log_file()
{
    if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_c))
    {
        auto log = std::ofstream
        {
            log_path.c_str(),
            std::ios_base::trunc,
        };

        toml::value value = toml::table({
            {"aircrafts", toml::array{"f16", "velum", "do228", "globo"}},
            {"helicopters", toml::array{"as350", "ec130", "ec140", "aw109"}},
        });

        value.at("aircrafts").as_array_fmt().fmt = toml::array_format::multiline;
        value.at("aircrafts").as_array_fmt().body_indent = 4;

        value.at("helicopters").as_array_fmt().fmt = toml::array_format::multiline;
        value.at("helicopters").as_array_fmt().body_indent = 4;

        log << toml::format(value);

        log.close();

        postTickerWithTokens("~b~cflf~w~ - check format log file", true, false);
    }
}

void cheat_to_check_parse_log_file()
{
    if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_d))
    {
        auto log_toml
            = toml::parse(log_path);

        for(auto model : toml::find<std::vector<std::string>>(log_toml, "aircrafts"))
        {
            postTickerWithTokens(model.c_str(), true, false);
            WAIT(500);
        }

        for(auto model : toml::find<std::vector<std::string>>(log_toml, "helicopters"))
        {
            postTickerWithTokens(model.c_str(), true, false);
            WAIT(500);
        }

        postTickerWithTokens("~b~cplf~w~ - check parse log file", true, false);
    }
}

Vehicle spawn_vehicle(Hash hash, Vector3 coords, float heading, DWORD timeout)
{
    if (!(STREAMING::IS_MODEL_IN_CDIMAGE(hash) && STREAMING::IS_MODEL_A_VEHICLE(hash)))
    {
        return 0 ;
    }
    STREAMING::REQUEST_MODEL(hash);
    DWORD startTime = GetTickCount();

    while (!STREAMING::HAS_MODEL_LOADED(hash))
    {
        WAIT(0);
        if (GetTickCount() > startTime + timeout)
        {
            WAIT(0);
            STREAMING::SET_MODEL_AS_NO_LONGER_NEEDED(hash);
            return 0;
        }
    }

    Vehicle veh = VEHICLE::CREATE_VEHICLE(hash, coords, heading, 0, 1, 0);
    VEHICLE::SET_VEHICLE_ON_GROUND_PROPERLY(veh, 5.0f);
    WAIT(0);
    STREAMING::SET_MODEL_AS_NO_LONGER_NEEDED(hash);

    ENTITY::SET_ENTITY_AS_MISSION_ENTITY(veh, false, true);
    ENTITY::SET_ENTITY_AS_NO_LONGER_NEEDED(&veh);

    return veh;
}

void script_main()
{
    while (true)
    {
        auto player
            = PLAYER::PLAYER_ID();
        auto player_ped
            = PLAYER::PLAYER_PED_ID();

        if (DLC::GET_IS_LOADING_SCREEN_ACTIVE()
            ||
            !ENTITY::DOES_ENTITY_EXIST(player_ped)
            ||
            !PLAYER::IS_PLAYER_CONTROL_ON(player))
        {
            WAIT(0);
            continue;
        }

        if (PED::IS_PED_INJURED(player_ped)
            ||
            PLAYER::IS_PLAYER_BEING_ARRESTED(player, TRUE))
        {
            WAIT(0);
            continue;
        }

        cheat_to_check_close_helipad();

        cheat_to_check_parse_config_file();

        cheat_to_check_format_log_file();

        cheat_to_check_parse_log_file();

        if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_0x1A))
        {
            auto vehicle_id
                = spawn_vehicle(joaat("polmav"), ENTITY::GET_ENTITY_COORDS(player_ped, false), ENTITY::GET_ENTITY_HEADING(player_ped), 1000);

            PED::SET_PED_INTO_VEHICLE(player_ped, vehicle_id, -1);

            postTickerWithTokens("~b~spolmav~w~", true, false);
        }

        if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_0x2A))
        {
            auto vehicle_id
                = spawn_vehicle(joaat("rafaleb"), ENTITY::GET_ENTITY_COORDS(player_ped, false), ENTITY::GET_ENTITY_HEADING(player_ped), 1000);

            PED::SET_PED_INTO_VEHICLE(player_ped, vehicle_id, -1);

            postTickerWithTokens("~b~srafaleb~w~", true, false);
        }

        if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_0x3A))
        {
            auto vehicle_id
                = spawn_vehicle(joaat("md500civ"), ENTITY::GET_ENTITY_COORDS(player_ped, false), ENTITY::GET_ENTITY_HEADING(player_ped), 1000);

            PED::SET_PED_INTO_VEHICLE(player_ped, vehicle_id, -1);

            postTickerWithTokens("~b~smd500civ~w~", true, false);
        }

        if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_0x1B))
        {
            char info_dlc_vehicles[64];

            sprintf_s(info_dlc_vehicles,
                      "number of available dlc vehicles: \'~b~%i~w~\'",
                      FILES::GET_NUM_DLC_VEHICLES());

            postTickerWithTokens(info_dlc_vehicles, true, false);
        }

        if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_0x2B))
        {
            char info_vehicle[64];

            sprintf_s(info_vehicle,
                      "first vehicle hash: \'~b~%X~w~\'",
                      (Hash)FILES::GET_DLC_VEHICLE_MODEL(0/*FILES::GET_NUM_DLC_VEHICLES()*/));

            postTickerWithTokens(info_vehicle, true, false);
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
