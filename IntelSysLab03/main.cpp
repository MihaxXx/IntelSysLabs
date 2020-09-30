#include <iostream>
#include <vector>
#include <set>
#include "set.h"
#include <chrono>


typedef std::vector< std::vector< int > > Puzzle;
typedef std::set< int > Values;

class SudokuSolver {
public:
    static Puzzle solve( const Puzzle& puzzle ) {
        Puzzle solution = puzzle;
        if( solveHelper( &solution ) ) {
            return solution;
        }
        return Puzzle();
    }

    static bool solveHelper( Puzzle* solution ) {
        int minRow = -1;
        int minColumn = -1;
        Values minValues;
        while (true) {
            minRow = -1;
            // Для каждой клетки проверяем выполнение условий на уникальность в ряду, столбце и блоке:
            for( int rowIndex = 0; rowIndex < 9; ++rowIndex ) {
                for( int columnIndex = 0; columnIndex < 9; ++columnIndex ) {
                    if( ( *solution )[ rowIndex ][ columnIndex ] != 0 ) {
                        continue;
                    }
                    Values possibleValues = findPossibleValues( rowIndex, columnIndex, *solution );
                    int possibleValuesCount = possibleValues.size();
                    //Если для какой-то клетки подходящей цифры не нашлось, то завершаем работу алгоритма (решения нет);
                    if(possibleValuesCount == 0 ) {
                        return false;
                    }
                    //Если существует единственная подходящая цифра, то заполняем клетку соответствующим образом;
                    if(possibleValuesCount == 1 ) {
                        ( *solution )[ rowIndex ][ columnIndex ] = *possibleValues.begin();
                    }
                    //Minimum Remaining Values
                    if( minRow < 0 || possibleValuesCount < minValues.size() ) {
                        minRow = rowIndex;
                        minColumn = columnIndex;
                        minValues = possibleValues;
                    }
                }
            }
            //Если все клетки заполнены, то завершаем цикл и возвращаем найденное решение;
            if( minRow == -1 ) {
                return true;
                //Иначе если ни одну клетку за проход заполнить не удалось, то завершаем цикл;
            } else if( 1 < minValues.size() ) {
                break;
            }
        }
        //Для клетки с минимальным количеством вариантов:
        for( auto v : minValues ) {
            //Пробуем ставить каждую цифру по порядку и рекурсивно решать получившиеся Судоку;
            Puzzle solutionCopy = *solution;
            solutionCopy[ minRow ][ minColumn ] = v;
            //Если решение было найдено, то возвращаем его;
            if( solveHelper( &solutionCopy ) ) {
                *solution = solutionCopy;
                return true;
            }
        }
        return false;
    }

    static Values findPossibleValues( int rowIndex, int columnIndex, const Puzzle& puzzle ) {
        Values values;
        for( int i = 1; i < 10; ++i ) {
            values.insert(i);
        }
        values = values - getRowValues( rowIndex, puzzle );
        values = values - getColumnValues( columnIndex, puzzle );
        values = values - getBlockValues( rowIndex, columnIndex, puzzle );

        return values;
    }

    static Values getRowValues( int rowIndex, const Puzzle& puzzle ) {
         Values res;
        res.insert(puzzle[ rowIndex ].begin(),puzzle[ rowIndex ].end());
        return  res;
    }

    static Values getColumnValues( int columnIndex, const Puzzle& puzzle ) {
        Values values;
        for( int r = 0; r < 9; ++r ) {
            values.insert(puzzle[ r ][ columnIndex ]);
        }
        return values;
    }

    static Values getBlockValues( int rowIndex, int columnIndex, const Puzzle& puzzle ) {
        Values values;
        int blockRowStart = 3 * ( rowIndex / 3 );
        int blockColumnStart = 3 * ( columnIndex / 3 );
        for( int r = 0; r < 3; ++r ) {
            for( int c = 0; c < 3; ++c ) {
                values.insert(puzzle[ blockRowStart + r ][ blockColumnStart + c ]);
            }
        }
        return values;
    }
};

void printPuzzle( const Puzzle& puzzle ) {
    for( auto row : puzzle ) {
        for( auto v : row ) {
            std::cout << v << " ";
        }
        std::cout << std::endl;
    }
}

int main() {
    Puzzle puzzle = {
        { 0, 0, 0, 0, 6, 0, 7, 0, 0 },
        { 0, 5, 9, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 2, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 6, 0, 0, 5, 0, 0, 0, 0, 0 },
        { 3, 0, 0, 0, 0, 0, 4, 6, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 9, 1 },
        { 8, 0, 0, 7, 4, 0, 0, 0, 0 }
    };

    printPuzzle( puzzle );
    std::cout << std::endl;


    auto time_start = std::chrono::steady_clock::now();
    Puzzle solution = SudokuSolver::solve( puzzle );
    if( !solution.empty() ) printPuzzle( solution );

    std::cout << std::endl;
    auto finish = std::chrono::steady_clock::now();
    std::chrono::duration<double> diff = finish-time_start;
    std::cout<< diff.count() << std::endl;

    return 0;
}