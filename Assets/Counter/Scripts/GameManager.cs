using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject playerPrefab;
    List<PlayerBall> players = new List<PlayerBall>();
    int currentPlayerIndex = 0;
    int nRounds = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void ResetGame()
    {
        players.Clear();
    }

    public void InitGame()
    {
        HUD.Instance.HiddenEndRoundPanel();

        currentPlayerIndex = 0;
        if(players.Count == 0)
        {
            EndGame();
        }

        players[currentPlayerIndex].gameObject.SetActive(true);
        players[currentPlayerIndex].StartBall();

        //Show data panel
        HUD.Instance.SetCurrentPlayer(players[currentPlayerIndex].GetPlayerName(), players[currentPlayerIndex].GetPlayerPoints().ToString());
    }

    public void PlayerDropBall()
    {
        HUD.Instance.HiddeCurrentPlayerPanel();
    }

    public void ResumePlayer(bool isCompleted)
    {
        if(isCompleted)
        {
            players[currentPlayerIndex].AddPoint();
            players[currentPlayerIndex].DeactivePlayer();
        }
        else
        {
            players[currentPlayerIndex].DeactivePlayer();
        }

        HUD.Instance.HiddeChallengePanel();
        NextPlayer();
    }

    void NextRound()
    {
        currentPlayerIndex = 0;
        HUD.Instance.ShowEndRoundPanel(nRounds.ToString());
    }

    public void NextPlayer()
    {
        currentPlayerIndex++;
        if(currentPlayerIndex == players.Count)//last player
        {
            nRounds++;
            NextRound();
        }
        else
        {
            //Show panel
            HUD.Instance.SetCurrentPlayer(players[currentPlayerIndex].GetPlayerName(), players[currentPlayerIndex].GetPlayerPoints().ToString());
            players[currentPlayerIndex].gameObject.SetActive(true);
            players[currentPlayerIndex].StartBall();
        }
    }

    public void EndGame()
    {
        players.Sort(SortByPoints);

        int score = 1;

        foreach (PlayerBall player in players)
        {
            HUD.Instance.ResumePanelItem(score.ToString(), player.GetPlayerName(), player.GetPlayerPoints().ToString());
            score++;
        }

        HUD.Instance.ResumePanelTotal(nRounds.ToString());

        HUD.Instance.ShowResumePanel();
    }

    public int SortByPoints(PlayerBall a, PlayerBall b)
    {
        return b.GetPlayerPoints().CompareTo(a.GetPlayerPoints());
    }

    public void AddPoints()
    {
        players[currentPlayerIndex].AddPoints();
    }

    public void CreatePlayer(string playerName)
    {
        PlayerBall newPlayer = Instantiate(playerPrefab).GetComponent<PlayerBall>();
        newPlayer.SetPlayerName(playerName);
        newPlayer.DeactivePlayer();

        players.Add(newPlayer);
    }

    public string GetCurrentPlayerName()
    {
        return players[currentPlayerIndex].GetPlayerName();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
