#pragma once

#include <Windows.h>
#include <array>
#include <cstddef>
#include <filesystem>
#include <fstream>
#include <future>
#include <iostream>
#include <map>
#include <string_view>
#include <vector>

#include "post_ticker.hpp"
#include "natives.hpp"
#include "../../lib/shv/inc/main.h"
#include "../../lib/shv/inc/enums.h"
#include "../../lib/shv/inc/types.h"

#include "../../lib/toml11/include/toml.hpp"

constexpr uint32_t Joaat(const std::string_view str)
{
    std::uint32_t hash = 0;

    for (auto c : str)
    {
        hash += c >= 'A' && c <= 'Z' ? c | 1 << 5 : c;
        hash += (hash << 10);
        hash ^= (hash >> 6);
    }

    hash += (hash << 3);
    hash ^= (hash >> 11);
    hash += (hash << 15);

    return hash;
}

inline constexpr uint32_t operator""_J(const char* s, size_t n)
{
    uint32_t hash = 0;

    for (size_t i = 0; i < n; i++)
    {
        hash += s[i] >= 'A' && s[i] <= 'Z' ? s[i] | 1 << 5 : s[i];
        hash += (hash << 10);
        hash ^= (hash >> 6);
    }

    hash += (hash << 3);
    hash ^= (hash >> 11);
    hash += (hash << 15);

    return hash;
}
