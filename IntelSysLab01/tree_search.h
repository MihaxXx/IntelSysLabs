#pragma once

#include <iostream>
#include <queue>
#include <chrono>
#include <vector>

class TreeNode{

    int digit;
    TreeNode* parent;
    std::vector<TreeNode*> nodes;

public:

    TreeNode(TreeNode* parent, int digit);

    std::vector<TreeNode *> getNodes();

    static void getPath(TreeNode* tNode, int from);
    static void findPath(int from, int to);
};

