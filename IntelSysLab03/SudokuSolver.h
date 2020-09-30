#pragma once

#include <chrono>
#include "set.h"
#include <set>
#include <vector>
#include <iostream>
#include "SudokuSolver.h"

typedef std::vector<std::vector<int> > Puzzle;
typedef std::set<int> Values;

class SudokuSolver
{
public:
    static Puzzle solve(const Puzzle &puzzle);

    static bool solveHelper(Puzzle *solution);

    static Values findPossibleValues(int rowIndex, int columnIndex, const Puzzle &puzzle);

    static Values getRowValues(int rowIndex, const Puzzle &puzzle);

    static Values getColumnValues(int columnIndex, const Puzzle &puzzle);

    static Values getBlockValues(int rowIndex, int columnIndex, const Puzzle &puzzle);
};
