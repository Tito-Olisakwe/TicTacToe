using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class TicTacToeGameManager : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public Button[] gridButtons;
    public GameObject[] strikeouts;
    public Button resetButton;

    private string currentPlayer = "X";
    private string[] gridState = new string[9];
    private bool isPlayingWithAI = true; // Set to true if playing with AI

    void Start()
    {
        ResetGame();

        // Add listeners for the grid buttons
        foreach (Button button in gridButtons)
        {
            button.onClick.AddListener(() => OnGridButtonClick(button));
        }

        // Add listener for the reset button
        resetButton.onClick.AddListener(ResetGame);
    }

    void OnGridButtonClick(Button button)
    {
        int index = System.Array.IndexOf(gridButtons, button);
        if (gridState[index] == "")
        {
            MakeMove(index);
            if (isPlayingWithAI && currentPlayer == "O")
            {
                StartCoroutine(DelayAIMove());
            }
        }
    }

    IEnumerator DelayAIMove()
    {
        yield return new WaitForSeconds(1f);
        MakeAIMove();
    }

    void MakeMove(int index)
    {
        gridState[index] = currentPlayer;
        gridButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = currentPlayer;

        if (CheckWinner())
        {
            displayText.text = currentPlayer + " Wins!";
            DisableGridButtons();
            resetButton.gameObject.SetActive(true);
            return;
        }

        if (IsGridFull())
        {
            displayText.text = "It's a Tie!";
            resetButton.gameObject.SetActive(true);
            return;
        }

        currentPlayer = currentPlayer == "X" ? "O" : "X";
        displayText.text = "Player " + currentPlayer + "'s Turn";
    }

    void MakeAIMove()
    {
        List<int> availableMoves = new List<int>();
        for (int i = 0; i < gridState.Length; i++)
        {
            if (gridState[i] == "")
            {
                availableMoves.Add(i);
            }
        }

        if (availableMoves.Count > 0)
        {
            int randomMove = availableMoves[Random.Range(0, availableMoves.Count)];
            MakeMove(randomMove);
        }
    }

    bool CheckWinner()
    {
        int[,] winningCombinations = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
            {0, 4, 8}, {2, 4, 6}
        };

        int[] strikeoutMap = { 0, 1, 2, 3, 4, 5, 6, 7 };

        for (int i = 0; i < winningCombinations.GetLength(0); i++)
        {
            int a = winningCombinations[i, 0];
            int b = winningCombinations[i, 1];
            int c = winningCombinations[i, 2];

            if (gridState[a] != "" && gridState[a] == gridState[b] && gridState[a] == gridState[c])
            {
                strikeouts[strikeoutMap[i]].SetActive(true);
                return true;
            }
        }
        return false;
    }

    bool IsGridFull()
    {
        foreach (string state in gridState)
        {
            if (state == "")
            {
                return false;
            }
        }
        return true;
    }

    void DisableGridButtons()
    {
        foreach (Button button in gridButtons)
        {
            button.interactable = false;
        }
    }

    void ResetGame()
    {
        currentPlayer = "X";
        displayText.text = "Player " + currentPlayer + "'s Turn";

        for (int i = 0; i < gridState.Length; i++)
        {
            gridState[i] = "";
            gridButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            gridButtons[i].interactable = true;
        }

        foreach (GameObject strikeout in strikeouts)
        {
            strikeout.SetActive(false);
        }

        resetButton.gameObject.SetActive(false);

        if (isPlayingWithAI && currentPlayer == "O")
        {
            MakeAIMove();
        }
    }
}
