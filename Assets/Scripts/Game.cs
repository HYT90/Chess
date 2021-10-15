using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    private GameObject[,] position = new GameObject[8,8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    void Start()
    {
        playerWhite = new GameObject[]{
            Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0), Create("white_bishop", 5, 0), Create("white_knight", 6, 0),
            Create("white_rook", 7, 0), Create("white_pawn", 0, 1),Create("white_pawn", 1, 1),Create("white_pawn", 2, 1),Create("white_pawn", 3, 1),Create("white_pawn", 4, 1),Create("white_pawn", 5, 1),Create("white_pawn", 6, 1),Create("white_pawn", 7, 1)
        };

        playerBlack = new GameObject[]{
            Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7), Create("black_queen", 3, 7), Create("black_king", 4, 7), Create("black_bishop", 5, 7), Create("black_knight", 6, 7),
            Create("black_rook", 7, 7), Create("black_pawn", 0, 6),Create("black_pawn", 1, 6),Create("black_pawn", 2, 6),Create("black_pawn", 3, 6),Create("black_pawn", 4, 6),Create("black_pawn", 5, 6),Create("black_pawn", 6, 6),Create("black_pawn", 7, 6)
        };

        for(int i = 0; i<16; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1f), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        position[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        position[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return position[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if(x < 0 || y < 0 || x >= position.GetLength(0) || y >= position.GetLength(1))
        {
            return false;
        }
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if(currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";        
        }
    }

    public void Update()
    {
       if(gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Winner(string player)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = "Player " + player + " is the winner!!";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
