using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private string [,] board = new [, ]{{"x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x"},
                                        {"x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x"},
                                        {"x", "x", "r", "n", "b", "q", "k", "b", "n", "r", "x", "x"},
                                        {"x", "x", "p", "p", "p", "p", "p", "p", "p", "p", "x", "x"},
                                        {"x", "x", ".", ".", ".", ".", ".", ".", ".", ".", "x", "x"},
                                        {"x", "x", ".", ".", ".", ".", ".", ".", ".", ".", "x", "x"},
                                        {"x", "x", ".", ".", ".", ".", ".", ".", ".", ".", "x", "x"},
                                        {"x", "x", ".", ".", ".", ".", ".", ".", ".", ".", "x", "x"},
                                        {"x", "x", "P", "P", "P", "P", "P", "P", "P", "P", "x", "x"},
                                        {"x", "x", "R", "N", "B", "Q", "K", "B", "N", "R", "x", "x"},
                                        {"x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x"},
                                        {"x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x"}};

    // private string [,] GetBoard (string [,] board = startBoard) {
    //     return board;
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
        public string type;
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
        board[from.X, from.Y] = ".";
    } 

    private bool [,] GetMovePawnMap(FigureOnCell figureOnCell) {
        
        bool [,] moveMap = new bool [12,12];
        string figure = figureOnCell.figure.type;
        Cell cell = figureOnCell.cell;
        
        if (figure == "p" && cell.X == 3) {
            moveMap[cell.X + 1, cell.Y] = true;
            moveMap[cell.X + 2, cell.Y] = true;
        } 
        else if (figure == "p" && cell.X != 2) {
            moveMap[cell.X + 1, cell.Y] = true;
        }

        if (figure == "P" && cell.X == 8) {
            moveMap[cell.X - 1, cell.Y] = true;
            moveMap[cell.X - 2, cell.Y] = true;
        }

        else if (figure == "P" && cell.X != 8) {
            moveMap[cell.X - 1, cell.Y] = true;
        }

        return moveMap;
    }

    private void Start() {

        var cell = GetCell (7, 1);
        var figure = GetFigure (cell);
        var figureOnCell = GetFigureOnCell (figure, cell);
        var moveMap = GetMovePawnMap(figureOnCell);
        
        for (int i = 0; i < 12; i++) {
            Debug.Log($"{board[0, i]} {board[1, i]} {board[2, i]} {board[3, i]} {board[4, i]} {board[5, i]} {board[6, i]} {board[7, i]} {board[8, i]} {board[9, i]} {board[10, i]} {board[11, i]}");
        } 

        for (int i = 0; i < 12; i++) {
            Debug.Log($"{moveMap[0, i]} {moveMap[1, i]} {moveMap[2, i]} {moveMap[3, i]} {moveMap[4, i]} {moveMap[5, i]} {moveMap[6, i]} {moveMap[7, i]} {moveMap[8, i]} {moveMap[9, i]} {moveMap[10, i]} {moveMap[11, i]}");
        }        
    }
}
