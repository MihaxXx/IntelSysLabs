#include <iostream>
#include <cstdlib>
#include <ctime>
#include <cstring>
#include <chrono>
#include <functional>
#include <algorithm>
using namespace std;

class Board;
int max_depth = 10;
#define AlphaBetaPruning

// Function prototypes
void minimaxDecision(Board board, int& x, int& y);
int minimaxValue(Board board, char originalTurn, int searchPly);
int minimaxABValue(Board board, char originalTurn, int searchPly, int alpha_max, int beta_min);

// The board class represents a board and current player making a move
class Board
{
private:
	char whoseTurn;
	char board[8][8];
	bool checkFlip(int x, int y, int deltaX, int deltaY);
	void flipPieces(int x, int y, int deltaX, int deltaY);
public:
	Board();
	char getWhosePiece();
	char getOpponentPiece();
	void setCurrentPlayer(char player);
	void display();
	void makeMove(int x, int y);
	bool validMove(int x, int y);
	void getMoveList(int moveX[], int moveY[], int& numMoves);
	void getRandomMove(int& x, int& y);
	bool gameOver();
	int heuristic(char whoseTurn);
	int score(char piece);

	bool operator==(const Board& rhs) const;

	bool operator!=(const Board& rhs) const;

    int scoreCorners(char whoseTurn);
};


// Initialize the board in the constructor
Board::Board()
{
	for (int i = 0; i < 8; i++)
		for (int j = 0; j < 8; j++)
			board[i][j] = '.';
	board[3][3] = 'X';
	board[3][4] = 'O';
	board[4][3] = 'O';
	board[4][4] = 'X';
}

// New methods to get the player currently moving and the opponent
char Board::getWhosePiece()
{
	return whoseTurn;
}

char Board::getOpponentPiece()
{
	if (whoseTurn == 'X')
		return 'O';
	return 'X';
}

void Board::setCurrentPlayer(char player)
{
	whoseTurn = player;
}

// Output the board
void Board::display()
{
	for (int y = 0; y < 8; y++)
	{
		cout << 1 + y << " ";
		for (int x = 0; x < 8; x++)
			cout << " " << board[x][y];
		cout << endl;
	}
	cout << "   a b c d e f g h" << endl;
}

// Checks a direction from x,y to see if we can make a move
bool Board::checkFlip(int x, int y, int deltaX, int deltaY)
{
	char opponentPiece = getOpponentPiece();
	char myPiece = whoseTurn;
	if (board[x][y] == opponentPiece)
	{
		while ((x >= 0) && (x < 8) && (y >= 0) && (y < 8))
		{
			x += deltaX;
			y += deltaY;
			// BUG in original code, allows -1 or 8. For example
			// if x = 0 and deltaX is -1, then now x = -1
			// I patched this with another if statement, but it might be better
			// to move the += to the bottom of the loop
			if ((x >= 0) && (x < 8) && (y >= 0) && (y < 8))
			{
				if (board[x][y] == '.') // not consecutive
					return false;
				if (board[x][y] == myPiece)
					return true; // At least one piece we can flip
				else
				{
					// It is an opponent piece, just keep scanning in our direction
				}
			}
		}
	}
	return false; // Either no consecutive opponent pieces or hit the edge of the board
}

// Flips pieces in the given direction until we don't hit any more opponent pieces.
// Assumes this is a valid direction to flip (we eventually hit one of our pieces).
void Board::flipPieces(int x, int y, int deltaX, int deltaY)
{
	while (board[x][y] == getOpponentPiece())
	{
		board[x][y] = whoseTurn;;
		x += deltaX;
		y += deltaY;
	}
}


// Makes a move on the board, assumes the move is valid.
void Board::makeMove(int x, int y)
{
	// Put the piece at x,y
	board[x][y] = whoseTurn;

	// Check to the left
	if (checkFlip(x - 1, y, -1, 0))
		flipPieces(x - 1, y, -1, 0);
	// Check to the right
	if (checkFlip(x + 1, y, 1, 0))
		flipPieces(x + 1, y, 1, 0);
	// Check down
	if (checkFlip(x, y - 1, 0, -1))
		flipPieces(x, y - 1, 0, -1);
	// Check up
	if (checkFlip(x, y + 1, 0, 1))
		flipPieces(x, y + 1, 0, 1);
	// Check down-left
	if (checkFlip(x - 1, y - 1, -1, -1))
		flipPieces(x - 1, y - 1, -1, -1);
	// Check down-right
	if (checkFlip(x + 1, y - 1, 1, -1))
		flipPieces(x + 1, y - 1, 1, -1);
	// Check up-left
	if (checkFlip(x - 1, y + 1, -1, 1))
		flipPieces(x - 1, y + 1, -1, 1);
	// Check up-right
	if (checkFlip(x + 1, y + 1, 1, 1))
		flipPieces(x + 1, y + 1, 1, 1);
}

// Returns true if the move is valid, false if invalid
bool Board::validMove(int x, int y)
{
	// Check that the coordinates are empty
	if (board[x][y] != '.')
		return false;

	// If we can flip in any direction, it is valid
	// Check to the left
	if (checkFlip(x - 1, y, -1, 0))
		return true;
	// Check to the right
	if (checkFlip(x + 1, y, 1, 0))
		return true;
	// Check down
	if (checkFlip(x, y - 1, 0, -1))
		return true;
	// Check up
	if (checkFlip(x, y + 1, 0, 1))
		return true;
	// Check down-left
	if (checkFlip(x - 1, y - 1, -1, -1))
		return true;
	// Check down-right
	if (checkFlip(x + 1, y - 1, 1, -1))
		return true;
	// Check up-left
	if (checkFlip(x - 1, y + 1, -1, 1))
		return true;
	// Check up-right
	if (checkFlip(x + 1, y + 1, 1, 1))
		return true;

	return false; // If we get here, we didn't find a valid flip direction
}

// Fills in the arrays with valid moves for the piece.  numMoves is the number of valid moves.
void Board::getMoveList(int moveX[], int moveY[], int& numMoves)
{
	numMoves = 0; // Initially no moves found

	// Check each square of the board and if we can move there, remember the coordinates
	for (int x = 0; x < 8; x++)
		for (int y = 0; y < 8; y++)
		{
			if (validMove(x, y)) // If find valid move, remember coordinates
			{
				moveX[numMoves] = x;
				moveY[numMoves] = y;
				numMoves++; // Increment number of moves found
			}
		}
}

// True if the game is over, false if not over
bool Board::gameOver()
{
	int XMoveX[60], XMoveY[60], OMoveX[60], OMoveY[60];
	int numXMoves, numOMoves;

	getMoveList(XMoveX, XMoveY, numXMoves);
	// Temporarily flip whoseturn to opponent to get opponent move list
	whoseTurn = getOpponentPiece();
	getMoveList(OMoveX, OMoveY, numOMoves);
	whoseTurn = getOpponentPiece(); // Flip back to original
	if ((numXMoves == 0) && (numOMoves == 0))
		return true;
	return false;
}

// Using the move list, gets a random move out of this list
void Board::getRandomMove(int& x, int& y)
{
	int moveX[60], moveY[60], numMoves;
	getMoveList(moveX, moveY, numMoves);
	if (numMoves == 0)
	{
		x = -1;
		y = -1;
	}
	else
	{
		int i = rand() % numMoves;
		x = moveX[i];
		y = moveY[i];
	}
}

// Returns the score for the piece
int Board::score(char piece)
{
	int total = 0;
	for (int x = 0; x < 8; x++)
		for (int y = 0; y < 8; y++)
		{
			if (board[x][y] == piece)
				total++;
		}
	return total;
}
int Board::scoreCorners(char whoseTurn)
{
    int multCorner = 7;
    int multNearCorner = -3;
    int sum = 0;
    sum+= multCorner*(board[0][0]==whoseTurn? 1:-1) + multNearCorner*
            ((board[1][1]==whoseTurn? 1:-1)+(board[0][1]==whoseTurn? 1:-1)+(board[1][0]==whoseTurn? 1:-1));

    sum+= multCorner*(board[0][7]==whoseTurn? 1:-1) + multNearCorner*
            ((board[0][6]==whoseTurn? 1:-1)+(board[1][6]==whoseTurn? 1:-1)+(board[1][7]==whoseTurn? 1:-1));

    sum+= multCorner*(board[7][7]==whoseTurn? 1:-1) + multNearCorner*
            ((board[6][7]==whoseTurn? 1:-1)+(board[7][6]==whoseTurn? 1:-1)+(board[6][6]==whoseTurn? 1:-1));

    sum+= multCorner*(board[7][0]==whoseTurn? 1:-1) + multNearCorner*
            ((board[7][1]==whoseTurn? 1:-1)+(board[6][0]==whoseTurn? 1:-1)+(board[6][1]==whoseTurn? 1:-1));
    return sum;
}


// The simple heuristic is simply the number of our pieces - the number of opponent pieces.
// Weighting the edges and corners will result in a better player.
int Board::heuristic(char whoseTurn)
{
	int ourScore = score(whoseTurn);
	char opponent = 'X';
	if (whoseTurn == 'X')
		opponent = 'O';
	int opponentScore = score(opponent);
	return (ourScore - opponentScore + scoreCorners(whoseTurn));
}

bool Board::operator==(const Board& rhs) const
{
	return whoseTurn == rhs.whoseTurn &&
		board == rhs.board;
}

bool Board::operator!=(const Board& rhs) const
{
	return !(rhs == *this);
}

// This is the minimax decision function. It calls minimaxValue for each position
// on the board and returns the best move (largest value returned) in x and y.
void minimaxDecision(Board board, int& x, int& y)
{
	int moveX[60], moveY[60];
	int numMoves;
	char opponent = board.getOpponentPiece();

	board.getMoveList(moveX, moveY, numMoves);
	if (numMoves == 0) // if no moves return -1
	{
		x = -1;
		y = -1;
	}
	else
	{
		// Remember the best move
		int bestMoveVal = -99999;
		int bestX = moveX[0], bestY = moveY[0];
		// Try out every single move
		for (int i = 0; i < numMoves; i++)
		{
			// Apply the move to a new board
			Board tempBoard = board;
			tempBoard.makeMove(moveX[i], moveY[i]);
			// Recursive call
			// Set turn to opponent
			tempBoard.setCurrentPlayer(tempBoard.getOpponentPiece());
#ifdef AlphaBetaPruning
			int val = minimaxABValue(tempBoard, board.getWhosePiece(), 1, 99999, -99999);
#else
            int val = minimaxValue(tempBoard, board.getWhosePiece(), 1);
#endif
			// Remember best move
			if (val > bestMoveVal)
			{
				bestMoveVal = val;
				bestX = moveX[i];
				bestY = moveY[i];
			}
		}
		// Return the best x/y
		x = bestX;
		y = bestY;
	}
}

// minimaxValue makes a recursive call to itself to search another ply in the tree.
// It is hard-coded here to look 5 ply ahead.  originalTurn is the original player piece
// which is needed to determine if this is a MIN or a MAX move.  It is also needed to 
// calculate the heuristic. currentTurn flips between X and O.
int minimaxValue(Board board, char originalTurn, int searchPly)
{
	if ((searchPly == max_depth) || board.gameOver()) // Change to desired ply lookahead
	{
		//cout << "searchPly: "<< searchPly << endl;
		return board.heuristic(originalTurn); // Termination criteria
	}
	int moveX[60], moveY[60];
	int numMoves;
	char opponent = board.getOpponentPiece();

	board.getMoveList(moveX, moveY, numMoves);
	if (numMoves == 0) // if no moves skip to next player's turn
	{
		Board temp = board;
		temp.setCurrentPlayer(opponent);
		return minimaxValue(temp, originalTurn, searchPly + 1);
	}
	else
	{
		// Remember the best move
		int bestMoveVal = -99999; // for finding max
		if (originalTurn != board.getWhosePiece())
			bestMoveVal = 99999; // for finding min
		// Try out every single move
		for (int i = 0; i < numMoves; i++)
		{
			// Apply the move to a new board
			Board tempBoard = board;
			tempBoard.makeMove(moveX[i], moveY[i]);
			// Recursive call
			// Opponent's turn
			tempBoard.setCurrentPlayer(tempBoard.getOpponentPiece());
			int val = minimaxValue(tempBoard, originalTurn, searchPly + 1);
			// Remember best move
			if (originalTurn == board.getWhosePiece())
			{
				// Remember max if it's the originator's turn
				if (val > bestMoveVal)
					bestMoveVal = val;
			}
			else
			{
				// Remember min if it's opponent turn
				if (val < bestMoveVal)
					bestMoveVal = val;
			}
		}
		return bestMoveVal;
	}
	return -1; // Should never get here
}


int minimaxABValue(Board board, char originalTurn, int searchPly, int alpha_max, int beta_min)
{
	if ((searchPly == max_depth) || board.gameOver()) // Change to desired ply lookahead
	{
		//cout << "searchPly: " << searchPly << endl;
		return board.heuristic(originalTurn); // Termination criteria
	}
	int moveX[60], moveY[60];
	int numMoves;
	char opponent = board.getOpponentPiece();

	board.getMoveList(moveX, moveY, numMoves);
	if (numMoves == 0) // if no moves skip to next player's turn
	{
		Board temp = board;
		temp.setCurrentPlayer(opponent);
		return minimaxABValue(temp, originalTurn, searchPly + 1, alpha_max, beta_min);
	}
	else
	{
		// Remember the best move
		int bestMoveVal = -99999; // for finding max
		if (originalTurn != board.getWhosePiece())
			bestMoveVal = 99999; // for finding min
		// Try out every single move
		for (int i = 0; i < numMoves; i++)
		{
			// Apply the move to a new board
			Board tempBoard = board;
			tempBoard.makeMove(moveX[i], moveY[i]);
			// Recursive call
			// Opponent's turn
			tempBoard.setCurrentPlayer(tempBoard.getOpponentPiece());
			int val = minimaxABValue(tempBoard, originalTurn, searchPly + 1, alpha_max, beta_min);
			// Remember best move
			if (originalTurn == board.getWhosePiece())
			{
				// Remember max if it's the originator's turn
				if (val > bestMoveVal)
					bestMoveVal = val;
				alpha_max = max(alpha_max, bestMoveVal);
				if (beta_min <= alpha_max)
					break;
			}
			else
			{
				// Remember min if it's opponent turn
				if (val < bestMoveVal)
					bestMoveVal = val;
				beta_min = min(beta_min, bestMoveVal);

				if (beta_min <= alpha_max)
					break;
			}
		}
		return bestMoveVal;
	}
	return -1; // Should never get here
}

// true if need undo move (u|r\d+)
bool readOpponentMove(int& x, int& y)
{
	cout << "Enter move." << endl;
	char column;
	int row;
	cin >> column >> row;
	if (column == 'u' || column == 'r')
		return true;
	x = column - 'a';
	y = row - 1;
	return false;
}

ostream& printMove(ostream& out, int x, int y)
{
	return out << static_cast<char>(x + 'a') << y + 1;
}

enum PlayerType {RANDOM, HUMAN, BOT};

const char WHITE = 'X';
const char BLACK = 'O';

function<bool(Board&, int&, int&)> makePlayer(PlayerType ptype)
{
	switch(ptype)
	{
	case RANDOM:
		return [](Board& board, int& x, int& y) {  board.getRandomMove(x, y);  return false; };
	case HUMAN:
		return [](Board& board, int& x, int& y) { return readOpponentMove(x, y); };
	default:
		return [](Board& board, int& x, int& y)
		{
			minimaxDecision(board, x, y);
			printMove(cerr, x, y) << endl;
			return false;
		};
	}
}

// Main game loop
int main(int argc, char* argv[])
{
	char botColor = BLACK;
	PlayerType opponentMode = RANDOM;
	if (argc == 2)
	{
		botColor = argv[1][0] == '0' ? BLACK : WHITE;
		opponentMode = HUMAN;
	} else if (argc == 3)
	{
		opponentMode = argv[1] == "rand" ? RANDOM : HUMAN;
		botColor = argv[2][0] == '0' ? BLACK : WHITE;
	}
	
	srand(time(NULL));
	Board gameBoard;
	gameBoard.setCurrentPlayer(BLACK);
	const auto black = makePlayer(botColor == BLACK ? BOT : opponentMode);
	const auto white = makePlayer(botColor == WHITE ? BOT : opponentMode);
	auto time_start = std::chrono::steady_clock::now();
	Board prev;
	Board preprev;

	// for checking possible moves
	int moveX[60], moveY[60], numMoves;
	while (!gameBoard.gameOver())
	{
		gameBoard.display();
		gameBoard.getMoveList(moveX, moveY, numMoves);
		if (numMoves == 0)
		{
			gameBoard.setCurrentPlayer(gameBoard.getOpponentPiece());
		}
		cout << "It is player " << gameBoard.getWhosePiece() << "'s turn." << endl;
		int x, y;
		bool undo;
		if (gameBoard.getWhosePiece() == BLACK)
			undo = black(gameBoard, x, y);
		else
			undo = white(gameBoard, x, y);
		if (undo)
		{
			if (gameBoard == preprev)
				cout << "Sorry, can't undo more moves." << endl;
			else
			{
				gameBoard = preprev;
				cout << "Undone 1 move." << endl;
			}
		}
		else if (gameBoard.validMove(x, y) || (x == -1))
		{
			preprev = prev;
			prev = gameBoard;
			printMove(cout << "Moving to ", x, y) << endl;
			// Use -1 if no move possible
			if (x != -1)
				gameBoard.makeMove(x, y);
			gameBoard.setCurrentPlayer(gameBoard.getOpponentPiece());
		}
		else
		{
			cout << "Invalid move.  Enter move again. " << endl;
		}
	}
	auto finish = std::chrono::steady_clock::now();
	cout << endl << "The game is over!" << endl;
	gameBoard.display();
	const int scoreBlack = gameBoard.score('O');
	cout << "O's score: " << scoreBlack << endl;
	const int scoreWhite = gameBoard.score('X');
	cout << "X's score: " << scoreWhite << endl;
	std::chrono::duration<double> diff = finish - time_start;
	std::cout << diff.count() << std::endl;
	const int total = scoreBlack - scoreWhite;
	if (total == 0)
	{
		cout << "Draw!" << endl;
		return 4;
	}
	if ((total > 0 && botColor == BLACK )||( total < 0 && botColor == WHITE))
	{
		cout << "Bot Win!" << endl;
		return 0;
	}
	cout << "Bot Lose" << endl;
	return 3;
}
