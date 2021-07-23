using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

   public enum figures {

        whiteKing = 'K',
        whiteQueen = 'Q',
        whiteRook = 'R',
        whiteBishop = 'B',
        whiteKnight = 'N',
        whitePawn = 'P',
 
        blackKing = 'k',
        blackQueen = 'q',
        blackRook = 'r',
        blackBishop = 'b',
        blackKnight = 'n',
        blackPawn = 'p'
    }

    public bool whiteMove = true;

    private char [,] board = new [, ]{  {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                        {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                        {'x', 'x', 'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r', 'x', 'x'},
                                        {'x', 'x', 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'x', 'x'},
                                        {'x', 'x', '.', 'P', 'P', '.', '.', '.', '.', '.', 'x', 'x'},
                                        {'x', 'x', '.', '.', '.', '.', '.', '.', '.', '.', 'x', 'x'},
                                        {'x', 'x', '.', '.', '.', '.', '.', '.', '.', '.', 'x', 'x'},
                                        {'x', 'x', '.', '.', '.', '.', '.', '.', '.', '.', 'x', 'x'},
                                        {'x', 'x', 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'x', 'x'},
                                        {'x', 'x', 'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R', 'x', 'x'},
                                        {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
                                        {'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'}};

    // private string [,] GetBoard (string [,] board = startBoard) {
    // return board;
    // }

    struct FigureOnCell {
        public Cell cell;
        public Figure figure;
    }
    struct Cell {
        public int X;
        public int Y;
        //string figure = board[X, Y];
    }

    struct Figure {
        public char type;
        public bool white;
        public bool White {
            private set{
                if (Char.IsUpper(type)) {
                white = true;
            } else {
                white = false;
            }}            
            get {
                return white;
            } 
        }
    }

    private FigureOnCell GetFigureOnCell (Figure figure, Cell cell) {
        FigureOnCell figureOnCell = new FigureOnCell {
            cell = cell,
            figure = figure
        };

        return figureOnCell;
    }

    private Cell GetCell (int posX, int posY) {

        Cell cell = new Cell {
            X = posX + 1,
            Y = posY + 1,
        };
        
        return cell;
    }

    private  Figure GetFigure (Cell cell) {
        Figure figure = new Figure {
            type = board[cell.X, cell.Y]
        };
        
        return figure;
    }

    private void MoveFigure (Cell from, Cell to) {

        board[to.X, to.Y] = board[from.X, from.Y];
        board[from.X, from.Y] = '.';
    } 

    private bool [,] GetMovePawnMap(FigureOnCell figureOnCell) {
        
        bool [,] movePawnMap = new bool [12,12];

        Figure figure = figureOnCell.figure;
        Cell leftDiagonalCell;
        Cell rightDiagonalCell;
        Figure leftDiagonalFigure;
        Figure rightDiagonalFigure;

        Cell cell = figureOnCell.cell;

        if (figure.type == 'p') {
            leftDiagonalCell = GetCell(cell.X, cell.Y - 2);
            rightDiagonalCell = GetCell(cell.X + 1, cell.Y + 1);
            leftDiagonalFigure = GetFigure(leftDiagonalCell);
            rightDiagonalFigure = GetFigure(rightDiagonalCell);
            
            Debug.Log(leftDiagonalFigure.type);

            Debug.Log(figure.white);

            Debug.Log(leftDiagonalFigure.white);

            if (cell.X == 3) {
                movePawnMap[cell.X + 1, cell.Y] = true;
                movePawnMap[cell.X + 2, cell.Y] = true;
            }

            else if (cell.X != 3) {
                movePawnMap[cell.X + 1, cell.Y] = true;
            }

            if (figure.white != leftDiagonalFigure.white 
                && leftDiagonalFigure.type != 'x') {

                movePawnMap[cell.X + 1, cell.Y - 1] = true;
            }

            if (figure.white != rightDiagonalFigure.white 
                && rightDiagonalFigure.type != 'x') {
                movePawnMap[cell.X + 1, cell.Y + 1] = true;
            }

        }

        if (figure.type == 'P') {
            leftDiagonalCell = GetCell(cell.X - 1, cell.Y - 1);
            rightDiagonalCell = GetCell(cell.X - 1, cell.Y + 1);
            leftDiagonalFigure = GetFigure(leftDiagonalCell);
            rightDiagonalFigure = GetFigure(rightDiagonalCell);

            if (cell.X == 8) {
                movePawnMap[cell.X - 1, cell.Y] = true;
                movePawnMap[cell.X - 2, cell.Y] = true;
            }
            else if (cell.X != 8) {
                movePawnMap[cell.X - 1, cell.Y] = true;
            }

            if (figure.white != leftDiagonalFigure.white 
                && leftDiagonalFigure.type != 'x') {
                movePawnMap[cell.X - 1, cell.Y - 1] = true;
            }

            if (figure.white != rightDiagonalFigure.white 
                && rightDiagonalFigure.type != 'x') {
                movePawnMap[cell.X - 1, cell.Y + 1] = true;
            }

        }
        return movePawnMap;
    }

    private void Start() {
        var cell = GetCell (2, 3);
        var figure = GetFigure (cell);
        var figureOnCell = GetFigureOnCell (figure, cell);
        var movePawnMap = GetMovePawnMap(figureOnCell);
        
        for (int i = 0; i < 12; i++) {
            Debug.Log($"{board[i, 0]} {board[i, 1]} {board[i, 2]} {board[i, 3]} {board[i, 4]} {board[i, 5]} {board[i, 6]} {board[i, 7]} {board[i, 8]} {board[i, 9]} {board[i, 10]} {board[i, 11]}");
        } 

        for (int i = 0; i < 12; i++) {
            Debug.Log($"{movePawnMap[i, 0]} {movePawnMap[i, 1]} {movePawnMap[i, 2]} {movePawnMap[i, 3]} {movePawnMap[i, 4]} {movePawnMap[i, 5]} {movePawnMap[i, 6]} {movePawnMap[i, 7]} {movePawnMap[i, 8]} {movePawnMap[i, 9]} {movePawnMap[i, 10]} {movePawnMap[i, 11]}");
        }

        Debug.Log(figure.type);        
    }
}
