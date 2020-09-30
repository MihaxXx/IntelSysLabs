#pragma once
#include <set>

using namespace std;
template<class T>
set<T> operator -(set<T> reference, set<T> items_to_remove)
{
    set<T> result;
    std::set_difference(
            reference.begin(), reference.end(),
            items_to_remove.begin(), items_to_remove.end(),
            std::inserter(result, result.end()));
    return result;
}

