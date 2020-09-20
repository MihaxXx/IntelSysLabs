#include "Heuristics.h"

int Manhetten(Node *node)
{
    vector<size_t> &grid = node->grid;
    int sum = 0;
    for (int i = 0; i < 16; ++i)
    {
        if (grid[i] == 0)
            continue;
        size_t currentI = (15 + grid[i]) % 16;//текущая позиция
        int dx = abs((i % 4) - int(currentI % 4));//расстояние по x до "своей" позиции
        int dy = abs(i / 4 - int(currentI / 4));//расстояние по y до "своей" позиции
        sum += dx + dy;
    }
    return sum;
}

