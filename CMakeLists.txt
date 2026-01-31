# Require a recent enough CMake version for the features we use
cmake_minimum_required(VERSION 3.27)

# Declare the project: name, language, version and optional description
project(Helidecksignaling
    LANGUAGES CXX
    VERSION 0.9.0.0
    DESCRIPTION "Helideck signaling ScriptHookV plugin"
)

# ---------------- C++ standard configuration ----------------

# Use C++17 for all targets unless overridden
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

# Generate compile_commands.json (useful for IDEs, clangd, etc.)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

# ---------------- Compiler warnings / flags -----------------

# Prefer target-specific options, but we gather them here for MSVC
if(MSVC)
    # /EHsc   : standard C++ exception handling
    # /W4     : warning level 4 (high)
    # /permissive- : enforce strict C++ conformance
    set(HELIDECKSIGNALING_WARNING_FLAGS /EHsc /W4 /permissive-)
endif()

# ---------------- Library target definition -----------------

# Build the main plugin as a shared library (.dll which we will rename to .asi)
add_library(Helidecksignaling SHARED
    src/dllscript.cpp           # Your plugin source file
)

# Apply warning flags only to this target (and only if defined, e.g. on MSVC)
target_compile_options(Helidecksignaling PRIVATE ${HELIDECKSIGNALING_WARNING_FLAGS})

# ---------------- Third‑party -----------------------

add_subdirectory(lib/spdlog)
add_subdirectory(lib/toml11)

# ---------------- ScriptHookV configuration -----------------

# Path to ScriptHookV import library (.lib)
# Marked CACHE so users can override it from cmake-gui / ccmake
set(SCRIPTHOOKV_LIB
    "${CMAKE_SOURCE_DIR}/lib/shv/lib/ScriptHookV.lib"
    CACHE FILEPATH "ScriptHookV import library"
)

# Path to ScriptHookV headers (also cached for easy override)
set(SCRIPTHOOKV_INC_DIR
    "${CMAKE_SOURCE_DIR}/lib/shv/inc"
    CACHE PATH "ScriptHookV include directory"
)

# ---------------- Include directories -----------------------

# Private includes used only by this target:
# - Project headers under inc/
# - ScriptHookV headers
target_include_directories(Helidecksignaling PRIVATE
    "${SCRIPTHOOKV_INC_DIR}"
)

# ---------------- Linking -----------------------

# Link against ScriptHookV and toml11
# toml11::toml11 is the target exported by toml11's CMake code
target_link_libraries(Helidecksignaling PRIVATE
    spdlog::spdlog
    toml11::toml11
    "${SCRIPTHOOKV_LIB}"
)

# ---------------- Configuration‑specific defines ----------------

# Define _DEBUG for Debug builds, NDEBUG for Release/RelWithDebInfo
target_compile_definitions(Helidecksignaling PRIVATE
    $<$<CONFIG:Debug>:_DEBUG>
    $<$<CONFIG:Release>:NDEBUG>
    $<$<CONFIG:RelWithDebInfo>:NDEBUG>
)

# ---------------- Output naming / versioning ----------------

# On Windows, the shared library normally has .dll extension.
# We override it to .asi so ScriptHookV treats it as a plugin.
set_target_properties(Helidecksignaling PROPERTIES
    OUTPUT_NAME "Helideck-signaling"   # Base name of the produced file
    SUFFIX ".asi"                      # Change .dll to .asi
    VERSION ${PROJECT_VERSION}         # Use project version for the library
)

