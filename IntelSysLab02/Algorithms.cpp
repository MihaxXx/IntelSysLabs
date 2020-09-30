#include "Algorithms.h"

using namespace std;


///Вывод поля
void showGrid(Node *node)
{
    vector<size_t> &grid = node->grid;
    for (int i = 0; i < grid.size(); ++i)
    {
        if (i % 4 == 0)
            cout << endl;
        cout << grid[i] << ' ';
    }
    cout << endl;
}

///Проверка упорядоченности поля
bool isGoal(vector<size_t> grid)
{
    for (int i = 0; i < grid.size() - 1; ++i)
    {
        if (i + 1 != grid[i])
            return false;
    }
    return grid[grid.size() - 1] == 0;
}

///Вычислиение реащаемости позиции путём подсчёта кол-ва инверсий
bool solvable(Node *node)
{
    vector<size_t> grid = node->grid;
    int cntInversions = 0;
    for (int i = 0; i < grid.size(); ++i)
    {
        for (int j = i + 1; j < grid.size(); ++j)
        {
            if (grid[i] && grid[j] && grid[i] > grid[j])
                cntInversions++;
        }
    }

    int y = node->y;
    return (cntInversions % 2 == 0 && y % 2 == 1) || (cntInversions % 2 == 1 && y % 2 == 0);
}

///A* alg
Node *AStar(Node *node, function<int(Node *)> h)
{
    if (!solvable(node))
    {//на старте проверить решаемость исходной позиции
        cout << "Unsolvable" << endl;
        return node;
    }

    unordered_set<Node *, GridHash, GridEquals> used;
    priority_queue<pair<int, Node *>> qq;

    qq.emplace(-h(node), node);// в очередь добавляем с приоритетом обратным эвристике, чтобы сначала рассматривать те,
    // в у которых кратчайший прогнозируемый путь
    while (!qq.empty())
    {//продолжаем пока есть варианты
        auto cur = qq.top();
        used.insert(cur.second);//записываем отработанные в множество
        qq.pop();

        if (isGoal(cur.second->grid))
        {//Если цель достигнута, возвращаем итог
            return cur.second;
        }

        int pathLength = -cur.first - h(cur.second);//пройденный путь
        for (Node *to: cur.second->getMoves())
        {//для каждого из возможных движений из текущей позиции
            //showGrid(to);
            if (used.find(to) == used.end())
            {//если не проверяли
                size_t heur = h(to) + pathLength + 1;//то посчитать эвристику
                qq.emplace(-heur, to);//и положить с ней в очередь
            }
        }
    }
}

///Iterative deepening A* - iteration
Node *IDAStar_It(Node *node, function<int(Node *)> h, int deep)
{

    unordered_set<Node *, GridHash, GridEquals> used;
    priority_queue<pair<int, Node *>> qq;

    qq.emplace(-h(node), node);

    while (!qq.empty())
    {
        auto cur = qq.top();
        used.insert(cur.second);
        qq.pop();

        if (isGoal(cur.second->grid))
        {
            return cur.second;
        }

        int pathLength = -cur.first - h(cur.second);
        if (pathLength + 1 >= deep)//если превышена глубина, остановиться
            return nullptr;
        for (Node *to: cur.second->getMoves())
        {
            //showGrid(to);
            if (used.find(to) == used.end())
            {
                size_t heur = h(to) + pathLength + 1;
                qq.emplace(-heur, to);
            }
        }
    }
    return nullptr;
}

///Iterative deepening A*
Node *IDAStar(Node *node, function<int(Node *)> h)
{

    if (!solvable(node))
    {
        cout << "Unsolvable" << endl;
        return node;
    }

    //итеративное углубление
    for (int i = h(node); i < 90; ++i)
    {
        Node *answer = IDAStar_It(node, h, i);
        if (answer != nullptr && isGoal(answer->grid))
        {
            return answer;
        }
    }
}