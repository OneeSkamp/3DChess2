using System.Collections.Generic;
using UnityEngine;

namespace test {
public class TestSelector : MonoBehaviour {
    public GameObject blueBacklight;
    public GameObject blackPawn; 
    public GameObject blackKing; 
    public GameObject blackRook; 
    public GameObject blackKnight; 
    public GameObject blackBishop; 
    public GameObject blackQueen; 
    public GameObject whitePawn; 
    public GameObject whiteKing;
    public GameObject whiteRook; 
    public GameObject whiteKnight; 
    public GameObject whiteBishop; 
    public GameObject whiteQueen;
    private ChessControls chessAction;
    public int x;
    public int y;
    private Ray ray;
    private RaycastHit hit;

    public List<GameObject> possibleMoves = new List<GameObject>();
    public GameObject [,]  figuresMap = new GameObject[8,8];

    private void OnEnable() {
        chessAction.Enable();    
    }

    private void OnDisable() {    
        chessAction.Disable();    
    }

    private void Awake() {
        chessAction = new ChessControls();
        chessAction.Chess.Click.performed += ctx => MouseClick();
    }

    private void CleaningPossibleMoves() {
        foreach (GameObject moveCell in possibleMoves) {
            Destroy(moveCell);
        }
    }

    private void CreatingPossibleMoves(bool [,] moveMap) {
        float xPos = 3.5f;
        float yPos = 3.5f;
        for (int i = 0; i < moveMap.GetLength(0) - 4; i++) {
            for (int j = 0; j < moveMap.GetLength(1) - 4; j++) {
                if (moveMap[i + 2, j + 2]) {
                    possibleMoves.Add(Instantiate(blueBacklight,new Vector3(xPos,0.5f,yPos),Quaternion.identity));
                }
                yPos-=1f;
            }
            yPos=3.5f;
            xPos-=1f;
        }
    } 

    private void MoveFigure(int toX , int toY) {
        
        
        if(figuresMap[toX,toY]!=null){
            Destroy(figuresMap[toX,toY]);
        }
        figuresMap[BoardManager.activeFigure.x-2,BoardManager.activeFigure.y-2].transform.position=new Vector3(3.5f-toX,0.5f,3.5f-toY);
        figuresMap[toX,toY] = figuresMap[BoardManager.activeFigure.x-2,BoardManager.activeFigure.y-2];
        figuresMap[BoardManager.activeFigure.x-2,BoardManager.activeFigure.y-2]=null;
        BoardManager.MoveFigure(BoardManager.activeFigure, toX+2, toY+2);
        CleaningPossibleMoves();
    }

    private void CreatingFiguresOnBoard(char [,] board) {
            float xPos=3.5f;
            float yPos=3.5f;
        for (int i = 0; i < board.GetLength(0) - 4; i++) {

            for (int j = 0; j < board.GetLength(1) - 4; j++) {
                //var figure = figuresMap[i, j]; 
                switch (board[i + 2,j + 2]) {
                    case 'p':
                        figuresMap[i, j] = Instantiate(blackPawn,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'q':
                        figuresMap[i, j] = Instantiate(blackQueen,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'k':
                        figuresMap[i, j] = Instantiate(blackKing,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'n':
                        figuresMap[i, j] = Instantiate(blackKnight,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'r':
                        figuresMap[i, j] = Instantiate(blackRook,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'b':
                        figuresMap[i, j] = Instantiate(blackBishop,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'P':
                        figuresMap[i, j] = Instantiate(whitePawn,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'Q':
                        figuresMap[i, j] = Instantiate(whiteQueen,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'K':
                        figuresMap[i, j] = Instantiate(whiteKing,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'N':
                        figuresMap[i, j] = Instantiate(whiteKnight,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'R':
                        figuresMap[i, j] = Instantiate(whiteRook,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                    case 'B':
                        figuresMap[i, j] = Instantiate(whiteBishop,
                        new Vector3(xPos,0.5f,yPos),Quaternion.identity);
                        break;
                }
                yPos-=1f;
            }
            yPos=3.5f;
            xPos-=1f;
        }
    }

    private void MouseClick() {
        ray = Camera.main.ScreenPointToRay(chessAction.Chess.MousePosition.ReadValue<Vector2>());
        if(Physics.Raycast(ray, out hit)) {
            x = Mathf.Abs((int)(hit.point.x-4));
            y = Mathf.Abs((int)(hit.point.z-4));

            if(BoardManager.canMoveMap[x+2,y+2] == true){
                MoveFigure(x,y);
                CleaningPossibleMoves();    
            }
            else{
                CleaningPossibleMoves();
                BoardManager.CleaningCanMoveMap();
                BoardManager.activeFigure = BoardManager.GetActiveFigure(BoardManager.GetFigure(x + 2, y + 2));
                if(BoardManager.activeFigure!=null){
                    BoardManager.canMoveMap = BoardManager.GetMovePawnMap(BoardManager.activeFigure); 
                }
                CreatingPossibleMoves(BoardManager.canMoveMap);
            }
            //Debug.Log($"{Mathf.Abs((int)(hit.point.x-4))}   {Mathf.Abs((int)(hit.point.z-4))}");
        }
    }

    private void Start() {
        CreatingFiguresOnBoard(BoardManager.board);
        
    }
}
}