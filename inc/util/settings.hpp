#pragma once

#include "../../lib/toml11/include/toml.hpp"
#include <filesystem>

auto static const log_path = std::filesystem::current_path() / "Helideck-signaling.log";

auto static const native_helipads_file =
    std::filesystem::current_path() / "HelideckSignaling" / "LocationOfHelipads" / "native-helipads.toml";

inline void log_helideck_signaling()
{
    auto log = std::ofstream{
        log_path.c_str(),
        std::ios_base::trunc,
    };

    const auto parse_result = toml::try_parse(native_helipads_file);

    if (parse_result.is_ok())
    {
        for (const auto& [name, loc] : parse_result.unwrap().at("locations").as_table())
        {
            log << name << ": " << loc.at("coords").as_array().size() << " coords\n";
        }
    }
    else
    {
        log << parse_result.unwrap_err().at(0) << std::endl;
    }

    log.close();
}
