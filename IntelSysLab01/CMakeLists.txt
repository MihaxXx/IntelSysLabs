cmake_minimum_required(VERSION 3.14)
project(IntelSysLab01)

set(CMAKE_CXX_STANDARD 11)


set(SOURCE_FILES main.cpp tree_search.h)
add_executable(forward ${SOURCE_FILES})

set(SOURCE_FILES tree_search.h backSearch.cpp )
add_executable(backSearch ${SOURCE_FILES})

set(SOURCE_FILES bidirectional.cpp)
add_executable(bidirect ${SOURCE_FILES})

