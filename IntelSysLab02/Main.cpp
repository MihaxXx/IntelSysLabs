#include <iostream>
#include <ctime>
#include <map>
#include "Node.h"
#include "Algorithms.h"
#include "Heuristics.h"

#define PRINT_TIME
//#define PRINT_STEPS


using namespace std;
int showPath(Node *node);


int main()
{
    int prev_time = (double) clock() / CLOCKS_PER_SEC * 1e3;

    srand(time(0));
    //vector<size_t> startGrid = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};

    vector<size_t> startGrid = {5, 1, 14, 10, 15, 13, 7, 11, 9, 4, 12, 8, 3, 2, 6, 0};

    map<int, int> times;
    map<int, int> times_cnts;

    cout << endl;
    cout << "Start..." << endl;

    Node *start = new Node(startGrid);
    showGrid(start);
    start->parent = nullptr;
    //Node* answer = IDAStar(start, Manhetten);//53, 111 s
    Node *answer = AStar(start, Manhetten);//53, 21 s
    int steps = showPath(answer);
    showGrid(answer);

#ifdef PRINT_TIME
    cout << "\n=========== ";
    int time = ((double) clock() / CLOCKS_PER_SEC * 1e3) - prev_time;
    times[steps] += time;
    times_cnts[steps]++;
    //cout << time << " ms";
#endif

    cout << endl;
    for (auto val: times)
    {
        printf("steps = %d , time = %d ms\n", val.first, val.second / times_cnts[val.first]);
    }
    return 0;
}

///"Развёртка" пройденного пути
int showPath(Node *node)
{
    int steps = 0;
    while (node != nullptr)
    {
#ifdef PRINT_STEPS
        showGrid(node);
#endif
        node = node->parent;
        steps++;
    }
    return steps;
}