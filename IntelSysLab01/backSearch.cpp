#include "tree_search.h"

using namespace std;

TreeNode::TreeNode(TreeNode *parent, int digit): parent(parent), digit(digit)  {}


vector<struct TreeNode *> TreeNode::getNodes(){
    nodes.push_back(new TreeNode(this, digit - 3) );
    if (digit % 2 ==0)
        nodes.push_back(new TreeNode(this, digit / 2) );
    return nodes;
}

void TreeNode::getPath(TreeNode* tNode, int from){
    vector<int> order;
    TreeNode* node = tNode;
    while(node->digit != from){
        order.push_back(node->digit);
        node = node->parent;
    }
    order.push_back(node->digit);

    cout << order[order.size()-1];
    for (int i = order.size()-2; i >= 0; --i){
        cout << " -> " << order[i];
    }
    cout << endl;
}

void TreeNode::findPath(int from, int to){
    TreeNode* treeRoot = new TreeNode(nullptr, to);
    queue<TreeNode*> queue;
    queue.push(treeRoot);

    while (!queue.empty()){
        TreeNode* node = queue.front();
        queue.pop();
        if (node->digit == from){
            getPath(node, to);
            break;
        }

        for (auto tNode: node->getNodes()){
            queue.push(tNode);
        }
    }

}


int main() {
    int from, to;
    cin >> from >> to;
    auto time_start = std::chrono::steady_clock::now();
    TreeNode::findPath(from, to);

    auto finish = std::chrono::steady_clock::now();
    std::chrono::duration<double> diff = finish-time_start;
    std::cout<< diff.count() << std::endl;

    return 0;
}

