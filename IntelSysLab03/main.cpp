#include <chrono>
#include <iostream>
#include <vector>
#include "SudokuSolver.h"


void printPuzzle(const Puzzle &puzzle)
{
    if (puzzle.size() == 9 && puzzle[0].size() == 9)
    {
        for (int i = 0; i < puzzle.size(); i++)
        {
            if ((i % 3 == 0) && i != 0)
                std::cout << "_________________" << std::endl;
            for (int j = 0; j < puzzle[i].size(); j++)
            {
                std::cout << puzzle[i][j];
                if (((j + 1) % 3 != 0) || (j + 1) == 9)
                    std::cout << " ";
                else
                    std::cout << "|";
            }
            std::cout << std::endl;
        }
    }
    else
    {
        for (const auto &row : puzzle)
        {
            for (const auto &v : row)
            {
                std::cout << v << " ";
            }
            std::cout << std::endl;
        }
    }
}

int main()
{
    std::vector<std::vector<int>> puzzle = {
            {0, 0, 0, 0, 6, 0, 7, 0, 0},
            {0, 5, 9, 0, 0, 0, 0, 0, 0},
            {0, 1, 0, 2, 0, 0, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 0, 0, 0},
            {6, 0, 0, 5, 0, 0, 0, 0, 0},
            {3, 0, 0, 0, 0, 0, 4, 6, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 9, 1},
            {8, 0, 0, 7, 4, 0, 0, 0, 0}
    };

    printPuzzle(puzzle);
    std::cout << std::endl;
    std::cout << std::endl;


    auto time_start = std::chrono::steady_clock::now();
    std::vector<std::vector<int>> solution = SudokuSolver::solve(puzzle);
    auto finish = std::chrono::steady_clock::now();

    if (!solution.empty()) printPuzzle(solution);
    std::cout << std::endl;

    std::chrono::duration<double> diff = finish - time_start;
    std::cout << diff.count() << std::endl;

    return 0;
}