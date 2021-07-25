using System;
using UnityEngine;

namespace test {
public class BoardManager {

    public static bool whiteMove = true;
    public bool isActive = false;
    public static bool [,] canMoveMap = new bool [12,12];
    public static char [,] board = new [, ]{{'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                            {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                            {'x', 'x', 'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r', 'x', 'x'},
                                            {'x', 'x', 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'x', 'x'},
                                            {'x', 'x', 'P', '.', 'P', '.', '.', '.', '.', '.', 'x', 'x'},
                                            {'x', 'x', '.', '.', '.', '.', '.', '.', '.', '.', 'x', 'x'},
                                            {'x', 'x', '.', '.', '.', '.', '.', '.', '.', '.', 'x', 'x'},
                                            {'x', 'x', '.', '.', '.', '.', '.', '.', '.', '.', 'x', 'x'},
                                            {'x', 'x', 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'x', 'x'},
                                            {'x', 'x', 'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R', 'x', 'x'},
                                            {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                            {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'}};

    public class Figure {
        public int x;
        public int y;
        public char type;
    }

    public static Figure activeFigure;
    public static Figure GetFigure (int xPos, int yPos) {
        Figure figure = new Figure {
            x = xPos,
            y = yPos,
            type = board[xPos, yPos],           
        };
        
        return figure;
    }
    public static Figure GetActiveFigure(Figure figure) {
        if (whiteMove && Char.IsUpper(figure.type)) {
            activeFigure = figure;
            return activeFigure;
        }
        if (!whiteMove && !Char.IsUpper(figure.type)
            && figure.type != '.') {
            activeFigure = figure;
            return activeFigure;
        }
        return null;
    }   
    public static void CleaningCanMoveMap() {
        for (int i = 0; i < canMoveMap.GetLength(0); i++) {
          
            for (int j = 0; j < canMoveMap.GetLength(1); j++) {
                canMoveMap[i,j] = false;
            }
        }
    }
    public static void MoveFigure (Figure from, int toX, int toY) {
        if (canMoveMap[toX, toY]) {
            board[toX, toY] = board[from.x, from.y];
            board[from.x, from.y] = '.';
            whiteMove = !whiteMove;
            Debug.Log(whiteMove);
            CleaningCanMoveMap(); 
        }
    } 

    public static bool [,] GetMovePawnMap(Figure figure) {

        Figure leftDiagonalFigure;
        Figure rightDiagonalFigure;

        if (figure.type == 'p') {
            leftDiagonalFigure = GetFigure(figure.x + 1, figure.y - 1);
            rightDiagonalFigure = GetFigure(figure.x + 1, figure.y + 1);
           // Debug.Log(figure.white);

           // Debug.Log(leftDiagonalFigure.white);

            if (figure.x == 3 && board[figure.x + 1, figure.y] == '.'
                && board[figure.x + 2, figure.y] == '.') {
                    canMoveMap[figure.x + 1, figure.y] = true;
                    canMoveMap[figure.x + 2, figure.y] = true;
            }

            else if (figure.x != 3 && board[figure.x + 1, figure.y] == '.') {
                canMoveMap[figure.x + 1, figure.y] = true;
            }

            if (Char.IsUpper(leftDiagonalFigure.type) 
                && leftDiagonalFigure.type != 'x'
                && leftDiagonalFigure.type != '.') {

                    canMoveMap[figure.x + 1, figure.y - 1] = true;
            }

            if (Char.IsUpper(rightDiagonalFigure.type)
                && rightDiagonalFigure.type != 'x'
                && rightDiagonalFigure.type != '.') {
                    canMoveMap[figure.x + 1, figure.y + 1] = true;
            }
        }

        if (figure.type == 'P') {
            leftDiagonalFigure = GetFigure(figure.x - 1, figure.y - 1);
            rightDiagonalFigure = GetFigure(figure.x - 1, figure.y + 1);


            if (figure.x == 8 && board[figure.x - 1, figure.y] == '.'
                && board[figure.x - 2, figure.y] == '.') {
                    canMoveMap[figure.x - 1, figure.y] = true;
                    canMoveMap[figure.x - 2, figure.y] = true;
            }
            else if (figure.x != 8 && board[figure.x - 1, figure.y] == '.') {
                canMoveMap[figure.x - 1, figure.y] = true;
            }

            if (!Char.IsUpper(leftDiagonalFigure.type) 
                && leftDiagonalFigure.type != 'x'
                && leftDiagonalFigure.type != '.') {
                    canMoveMap[figure.x - 1, figure.y - 1] = true;
            }

            if (!Char.IsUpper(rightDiagonalFigure.type) 
                && rightDiagonalFigure.type != 'x'
                && rightDiagonalFigure.type != '.') {
                    canMoveMap[figure.x - 1, figure.y + 1] = true;
            }
        }
        return canMoveMap;
    }
}

}
