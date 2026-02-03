#include "script.hpp"

#include "./settings/settings.hpp"

constexpr auto cheat_a = "rwtea"_J;
constexpr auto cheat_b = "rwteb"_J;

void example_toml_a()
{
  if (MISC::HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED(cheat_a))
  {
    log_helideck_signaling();

    postTickerWithTokens("log: ~b~Helideck-signaling.log~w~",
                         true,
                         true);
  }
}

void script_main()
{
  auto b_welcome
    = false;

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

    if (!b_welcome)
    {
      postTickerWithTokens("helideck signaling - by ~b~niziul~w~", true, true);
      b_welcome = true;
    }

    example_toml_a();

    WAIT(0);
  }

  return;
}

