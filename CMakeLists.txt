cmake_minimum_required(VERSION 3.27)

project(Helidecksignaling
    LANGUAGES CXX
    VERSION 0.9.0.0
    DESCRIPTION ""
)

# C++ standard
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

# Prefer target-specific compile options
if(MSVC)
  set(HELIDECKSIGNALING_WARNING_FLAGS /EHsc /W4 /permissive-)
endif()

# Library target
add_library(Helidecksignaling SHARED
    src/dllscript.cpp
)

target_compile_options(Helidecksignaling PRIVATE ${HELIDECKSIGNALING_WARNING_FLAGS})

# ScriptHookV paths (can be cached so user can change from GUI/ccmake)
set(SCRIPTHOOKV_INC_DIR "${CMAKE_SOURCE_DIR}/lib/shv/inc" CACHE PATH "ScriptHookV include directory")
set(SCRIPTHOOKV_LIB     "${CMAKE_SOURCE_DIR}/lib/shv/lib/ScriptHookV.lib" CACHE FILEPATH "ScriptHookV import library")

# Includes
target_include_directories(Helidecksignaling PRIVATE
    "${CMAKE_SOURCE_DIR}/src"
    "${CMAKE_SOURCE_DIR}/inc"
    "${SCRIPTHOOKV_INC_DIR}"
)

# Linking
target_link_libraries(Helidecksignaling PRIVATE
    "${SCRIPTHOOKV_LIB}"
)

# Config-specific defines
target_compile_definitions(Helidecksignaling PRIVATE
    $<$<CONFIG:Debug>:_DEBUG>
    $<$<CONFIG:Release>:NDEBUG>
    $<$<CONFIG:RelWithDebInfo>:NDEBUG>
)

# Changes .dll to .asi on Windows
set_target_properties(Helidecksignaling PROPERTIES
    OUTPUT_NAME "Helideck-signaling"
    SUFFIX ".asi"
)
