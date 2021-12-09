using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SendScore : MonoBehaviour
{
    public GameObject nameInput;
    public GameObject sendScoreButton;
    public GameObject closeButton;

    public TextMeshProUGUI scoreText;

    private int numOfButtons = 3;
    private int selectedButton;
    private bool previousAxisState;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nameInput);
        selectedButton = 1;
        previousAxisState = false;
        scoreText.text = "SCORE: " + FindObjectOfType<Board>().score;
    }

    void Update()
    {
        if (GetNegAxisDown("Vertical"))
        {
            selectedButton += 1;
            if (selectedButton > numOfButtons)
            {
                selectedButton = 1;
            }

            EventSystem.current.SetSelectedGameObject(null);

            switch (selectedButton)
            {
                case 1:
                    EventSystem.current.SetSelectedGameObject(nameInput);
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(sendScoreButton);
                    break;
                case 3:
                    EventSystem.current.SetSelectedGameObject(closeButton);
                    break;
            }
        }
    }

    public void RestartTetris ()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowPlace(uint place)
    {
        //called after delay
        if(place == uint.MaxValue)
        {
            //network error
        }
        else
        {
            //show place on leaderboard
        }
    }

    public void SendScoreToLeaderboard ()
    {
        string name = nameInput.GetComponent<TMP_InputField>().text;

        LeaderBoard.SendScore(this,new LeaderBoard.Score(FindObjectOfType<Board>().score, 
            name),i=> ShowPlace(i));
        RestartTetris();
    }

    private bool GetNegAxisDown(string axisName)
    {
        bool isAxisPressed = Input.GetAxis(axisName) < 0;

        if (isAxisPressed && previousAxisState)
        {
            return false;
        }

        previousAxisState = isAxisPressed;

        return isAxisPressed;
    }

}
