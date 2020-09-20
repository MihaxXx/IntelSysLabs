#pragma once

#include <functional>
#include <iostream>
#include <unordered_set>
#include <queue>
#include "Node.h"

using std::function;

void showGrid(Node *node);

int showPath(Node *node);

Node *AStar(Node *node, function<int(Node *)> h);

Node *IDAStar(Node *node, function<int(Node *)> h);

Node *IDAStar_It(Node *node, function<int(Node *)> h, int deep);
