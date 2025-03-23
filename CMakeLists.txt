cmake_minimum_required(VERSION 3.27)

project(Helidecksignaling
    LANGUAGES CXX
    VERSION 0.8.0.0
    DESCRIPTION ""
)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

if(MSVC)
    add_compile_options(/EHsc /W4 /permissive /std:c++17)
else()
    add_compile_options(-Wall -Wextra -pedantic -std=c++17)
endif()

if(CMAKE_SIZEOF_VOID_P EQUAL 8)
    set(PLATFORM x64)
endif()

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PLATFORM}/lib)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PLATFORM}/bin)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/${PLATFORM}/bin)

if (NOT toml11_FOUND)

    include(FetchContent)
    FetchContent_Declare(
        toml11
        GIT_REPOSITORY https://github.com/ToruNiina/toml11.git
        GIT_TAG        v4.4.0
    )

    if (NOT toml11_POPULATED)

        set(FETCHCONTENT_QUIET ON)
        FetchContent_MakeAvailable(toml11)

    endif()

endif()

if (NOT scripthookv_FOUND)

    include(FetchContent)
    FetchContent_Declare(
        scripthookv
        DOWNLOAD_EXTRACT_TIMESTAMP OFF
        URL https://ntscorp.ru/dev-c/ScriptHookV_SDK_1.0.617.1a.zip
    )

    if (NOT scripthookv_POPULATED)

        set(FETCHCONTENT_QUIET ON)
        FetchContent_MakeAvailable(scripthookv)

    endif()

endif()

set(SCRIPTHOOKV_INC_DIR ${scripthookv_SOURCE_DIR}/inc)
set(SCRIPTHOOKV_LIB_DIR ${scripthookv_SOURCE_DIR}/lib)

add_library(ScriptHookV STATIC IMPORTED)
set_target_properties(ScriptHookV PROPERTIES
    IMPORTED_LOCATION ${SCRIPTHOOKV_LIB_DIR}/ScriptHookV.lib
    INTERFACE_INCLUDE_DIRECTORIES ${SCRIPTHOOKV_INC_DIR}
)

if(NOT TARGET ScriptHookV)
    message(FATAL_ERROR "ScriptHookV library was not properly fetched.")
endif()

add_library(Helidecksignaling SHARED
    ${PROJECT_SOURCE_DIR}/src/dllscript.cpp
)

target_link_libraries(Helidecksignaling
    PUBLIC ScriptHookV
    INTERFACE toml11::toml11
)

set(GTA_V_DIR "D:/SteamLibrary/steamapps/common/Grand Theft Auto V" CACHE PATH "Path to GTA V installation directory")

if(NOT EXISTS "${GTA_V_DIR}")
    message(FATAL_ERROR "GTA_V_DIR is invalid or does not exist: ${GTA_V_DIR}")
endif()

add_custom_command(TARGET Helidecksignaling POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:Helidecksignaling> "${GTA_V_DIR}/"
)
add_custom_command(TARGET Helidecksignaling POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_PDB_FILE:Helidecksignaling> "${GTA_V_DIR}/"
)

set_target_properties(Helidecksignaling PROPERTIES
    OUTPUT_NAME "Helideck-signaling"
    SUFFIX ".asi"
    DEBUG_POSTFIX ""
    RELWITHDEBINFO_POSTFIX ""
)
