using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD Instance;
    [Header("Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject challengePanel;
    [SerializeField] GameObject currentPlayerDataPanel;
    [SerializeField] GameObject endRoundPanel;
    [SerializeField] GameObject resumePanel;
    [SerializeField] GameObject scoreTable;

    [Header("Labels")]
    [SerializeField] TextMeshProUGUI nameList;
    [SerializeField] TextMeshProUGUI challengeTitle;
    [SerializeField] TextMeshProUGUI currentPlayerName;
    [SerializeField] TextMeshProUGUI currentPlayerPoints;
    [SerializeField] TextMeshProUGUI finalTotalRounds;
    [SerializeField] TextMeshProUGUI currentTotalRounds;

    [Header("Inputs")]
    [SerializeField] TMP_InputField inputName;
    [SerializeField] GameObject labelTextPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nameList.text = "";
    }

    public void AddNewPlayer()
    {
        string playerName = inputName.text;
        Debug.Log(playerName);
        GameManager.Instance.CreatePlayer(playerName);
        nameList.text += playerName + "\n";
        inputName.text = "";
    }

    public void PlayBTN()
    {
        GameManager.Instance.InitGame();
        HiddeMainMenuPanel();
    }

    public void SetCurrentPlayer(string playerName, string playerPoints)
    {
        currentPlayerName.text = "CurrentPlayer: " + playerName;
        currentPlayerPoints.text = "Points: " + playerPoints;
        currentPlayerDataPanel.SetActive(true);
    }

    public void HiddenEndRoundPanel()
    {
        endRoundPanel.SetActive(false);
    }

    public void HiddeCurrentPlayerPanel()
    {
        currentPlayerDataPanel.SetActive(false);
    }

    public void HiddeMainMenuPanel()
    {
        mainMenuPanel.SetActive(false);
    }

    public void HiddeChallengePanel()
    {
        challengePanel.SetActive(false);
    }

    public void ShowChallengePanel(bool isTruth)
    {
        challengePanel.SetActive(true);

        string playerName = GameManager.Instance.GetCurrentPlayerName();

        if (isTruth)
        {
            challengeTitle.text = playerName + " tell me the Truth!";
        }
        else
        {
            challengeTitle.text = playerName + " it's time for the Challenge";
        }

        challengePanel.SetActive(true);
    }

    public void ShowEndRoundPanel(string totalPlayedRounds)
    {
        currentTotalRounds.text = $"Played rounds: {totalPlayedRounds}";
        endRoundPanel.SetActive(true);
    }

    #region GameOver

    public void ShowResumePanel()
    {
        resumePanel.SetActive(true);
    }

    public void ResumePanelItem(string order, string name, string points)
    {
        GameObject go = Instantiate(labelTextPrefab);

        go.GetComponent<TextMeshProUGUI>().text = $"{order}° - {name}: {points}";

        go.transform.parent = scoreTable.transform;
    }

    public void ResumePanelTotal(string totalPlayedRounds)
    {
        finalTotalRounds.text = $"Total played rounds: {totalPlayedRounds}";
    }

    #endregion

}
