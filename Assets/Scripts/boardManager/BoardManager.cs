using System;
using UnityEngine;

namespace boardManager {
public class BoardManager {

    public bool whiteMove = true;
    public bool isActive = false;
    public Figure activeFigure;
    public Figure king;
    public bool [,] canMoveMap = new bool [12,12];
    public bool [,] checkKingMap = new bool [12,12];
    public char [,] board = new [,]{{'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                    {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                    {'x', 'x', 'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r', 'x', 'x'},
                                    {'x', 'x', 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'x', 'x'},
                                    {'x', 'x', '.', '.', '.', '.', '.', '.', '.', '.', 'x', 'x'},
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

    public Figure GetFigure (int xPos, int yPos) {
        Figure figure = new Figure {
            x = xPos,
            y = yPos,
            type = board[xPos, yPos],           
        };
        
        return figure;
    }

    public Figure GetActiveFigure(Figure figure) {
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

    public void CleaningBoolMap(bool [,] map) {
        for (int i = 0; i < map.GetLength(0); i++) {
          
            for (int j = 0; j < map.GetLength(1); j++) {
                map[i,j] = false;
            }
        }
    }
    
    public void MoveFigure (Figure from, int toX, int toY) {
        if (canMoveMap[toX, toY]) {
            board[toX, toY] = board[from.x, from.y];
            board[from.x, from.y] = '.';
            whiteMove = !whiteMove;
            Debug.Log(whiteMove);
            CleaningBoolMap(canMoveMap); 
            CleaningBoolMap(checkKingMap);
            GetCheckKingMap();
        }
        FindPawnForChange();
    } 

    public bool [,] GetMovePawnMap(Figure figure, bool checkKing) {

        Figure leftDiagonalFigure;
        Figure rightDiagonalFigure; 

        if (figure.type == 'p') {
            leftDiagonalFigure = GetFigure(figure.x + 1, figure.y - 1);
            rightDiagonalFigure = GetFigure(figure.x + 1, figure.y + 1);
            if (!checkKing) {
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
            } else {
                canMoveMap[figure.x + 1, figure.y - 1] = true;
                canMoveMap[figure.x + 1, figure.y + 1] = true;
            }

        }

        if (figure.type == 'P') {
            leftDiagonalFigure = GetFigure(figure.x - 1, figure.y - 1);
            rightDiagonalFigure = GetFigure(figure.x - 1, figure.y + 1);
            if (!checkKing) {
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
            } else {
                canMoveMap[figure.x - 1, figure.y - 1] = true;
                canMoveMap[figure.x - 1, figure.y + 1] = true;
            }
        }
        return canMoveMap;
    }

    public bool [,] GetMoveKnightMap() {
        if (activeFigure.type == 'N' || activeFigure.type == 'n') {
            MoveKnight(activeFigure, -2, 1);
            MoveKnight(activeFigure, -2, -1);
            MoveKnight(activeFigure, 2, 1);
            MoveKnight(activeFigure, 2, -1);
            MoveKnight(activeFigure, -1, 2);
            MoveKnight(activeFigure, -1, -2);
            MoveKnight(activeFigure, 1, 2);
            MoveKnight(activeFigure, 1, -2);
        }
        return canMoveMap;
    }

    public bool [,] GetMoveRookMap() {
        if (activeFigure.type == 'R' || activeFigure.type == 'r') {
            MoveRook(activeFigure, 1, 1);
            MoveRook(activeFigure, -1, -1); 
        }
        return canMoveMap;
    }

    public bool [,] GetMoveBishopMap() {
        if (activeFigure.type == 'B' || activeFigure.type == 'b') {
            MoveBishop(activeFigure, 1, 1);
            MoveBishop(activeFigure, 1, -1);
            MoveBishop(activeFigure, -1, 1);
            MoveBishop(activeFigure, -1, -1);
        }
        return canMoveMap;
    }

    public bool [,] GetMoveQueenMap() {
        if (activeFigure.type == 'Q' || activeFigure.type == 'q') {
            MoveBishop(activeFigure, 1, 1);
            MoveBishop(activeFigure, 1, -1);
            MoveBishop(activeFigure, -1, 1);
            MoveBishop(activeFigure, -1, -1);
            MoveRook(activeFigure, 1, 1);
            MoveRook(activeFigure, -1, -1);  
        }
        return canMoveMap;
    }

    public bool [,] GetMoveKingMap() {
        if (activeFigure.type == 'K' || activeFigure.type == 'k') {
            MoveKing(activeFigure, 1, 0);
            MoveKing(activeFigure, 0, 1);
            MoveKing(activeFigure, -1, 0);
            MoveKing(activeFigure, 0, -1);
            MoveKing(activeFigure, 1, 1);
            MoveKing(activeFigure, 1, -1);
            MoveKing(activeFigure, -1, 1);
            MoveKing(activeFigure, -1, -1);
        }
        return canMoveMap;
    }
    
    public void MoveKnight(Figure figure, int toX, int toY) {
        bool colorFigure = Char.IsUpper(figure.type);
        bool colorFigureOnCanMove = Char.IsUpper(board[figure.x + toX, figure.y + toY]);
        if (board[figure.x + toX, figure.y + toY] == '.' 
            || board[figure.x + toX, figure.y + toY] != 'x'
            && colorFigureOnCanMove != colorFigure) {
                canMoveMap[figure.x + toX, figure.y + toY] = true;
        }
    }

    public void MoveRook(Figure figure, int toX, int toY) {
        bool colorFigure = Char.IsUpper(figure.type);
        for (int i = 1; i < 8; i++) {
            bool colorFigureOnCanMove = Char.IsUpper(board[figure.x + i * toX, figure.y]);
            if (board[figure.x + i * toX, figure.y] == 'x') {
                break;
            } else if (board[figure.x + i * toX, figure.y] == '.') {
                canMoveMap[figure.x + i * toX, figure.y] = true;
            } else if (colorFigureOnCanMove != colorFigure) {
                canMoveMap[figure.x + i * toX, figure.y] = true;
                break;
            } else if (colorFigureOnCanMove == colorFigure) {
                break;
            }
        }

        for (int i = 1; i < 8; i++) {
            bool colorFigureOnCanMove = Char.IsUpper(board[figure.x, figure.y + i * toY]);
            if (board[figure.x, figure.y + i * toY] == 'x') {
                break;
            } else if (board[figure.x, figure.y + i * toY] == '.') {
                canMoveMap[figure.x, figure.y + i * toY] = true;
            } else if (colorFigureOnCanMove != colorFigure) {
                canMoveMap[figure.x, figure.y + i * toY] = true;
                break;
            } else if (colorFigureOnCanMove == colorFigure) {
                break;
            }
        }
    }

    public void MoveBishop(Figure figure, int toX, int toY) {
        bool colorFigure = Char.IsUpper(figure.type);
        for (int i = 1; i < 8; i++) {
            bool colorFigureOnCanMove = Char.IsUpper(board[figure.x + i * toX, figure.y + i * toY]);
            if (board[figure.x + i * toX, figure.y + i * toY] == 'x') {
                break;
            } else if (board[figure.x + i * toX, figure.y + i * toY] == '.') {
                canMoveMap[figure.x + i * toX, figure.y + i * toY] = true;
            } else if (colorFigureOnCanMove != colorFigure) {
                canMoveMap[figure.x + i * toX, figure.y + i * toY] = true;
                break;
            } else if (colorFigureOnCanMove == colorFigure) {
                break;
            }
        }
    }

    public void MoveKing(Figure figure, int toX, int toY) {
        bool colorFigure = Char.IsUpper(figure.type);
        bool colorFigureOnCanMove = Char.IsUpper(board[figure.x + toX, figure.y + toY]);
        if (board[figure.x + toX, figure.y + toY] == '.') {
            canMoveMap[figure.x + toX, figure.y + toY] = true;
        } else if (colorFigureOnCanMove != colorFigure
                    && board[figure.x + toX, figure.y + toY] != 'x') {
            canMoveMap[figure.x + toX, figure.y + toY] = true;
        }
    }

    public void GetCheckKingMap() {
        bool figureColor;
        if (whiteMove) {
            figureColor = false;
        } else {
            figureColor = true;
        }
        for (int i = 0; i < 12; i++) {
            for (int j = 0; j < 12; j++) {
                Figure figure = GetFigure(i, j);
                if (figure.type == 'K' && !figureColor) {
                    king = GetFigure(i,j);
                }
                if (figure.type == 'k' && figureColor) {
                    king = GetFigure(i,j);
                }
                if (figure.type != 'x' && figure.type != '.' && Char.IsUpper(figure.type) == figureColor) {
                    activeFigure = figure;
                    GetMoveBishopMap();
                    GetMoveKnightMap();
                    GetMoveQueenMap();
                    GetMoveRookMap();
                    GetMovePawnMap(figure, true);
                }                       
            }
        }
        
        checkKingMap = (bool[,])canMoveMap.Clone();
        //CheckKing();
        CleaningBoolMap(canMoveMap);
        
        for (int i = 2; i < 10; i++) {
            //Debug.Log($"{checkKingMap[i,2]} {checkKingMap[i,3]} {checkKingMap[i,4]} {checkKingMap[i,5]} {checkKingMap[i,6]} {checkKingMap[i,7]}{checkKingMap[i,8]}{checkKingMap[i,9]}{checkKingMap[i,10]}");
        }
    }
    public bool CheckKing() {
        if (checkKingMap[king.x, king.y]) {
            Debug.Log("??????");
            
            return true;
        }
        return false;
    }
    public void FindPawnForChange() {
        for(int i = 2; i < 8; i++) {
            if (board[2, i] == 'P') {
                ChangePawn(GetFigure(2, i), 'Q'); 
            }
            if (board[9, i] == 'P') {
                ChangePawn(GetFigure(2, i), 'q'); 
            }
        }
    }

    public void ChangePawn(Figure pawn, char changeFigureType) {
        if (pawn.type == 'P') {
            board[pawn.x, pawn.y] = changeFigureType;
        }
    }
}
}
