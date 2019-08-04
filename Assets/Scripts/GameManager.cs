using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Singletons are bad, but screw that I've already wasted hours after getting distracted while researching better alternatives
    public static GameManager instance;

    public List<Player> playersInTurnOrder;

    public string playerName = "Player";
    public List<string> aiNames = new List<string>() {
        "Alice",
        "Bob",
        "Charlie",
        "Diana"
        };

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            this.enabled = false;
            return;
        }

        instance = this;

        playersInTurnOrder = new List<Player>();
        CreateNewGenericPlayers(true, 1);

        ShuffleTurnOrder();

        StartNextTurn();
    }

    public void CreateNewGenericPlayers(bool human, int numOfAI)
    {
        if (human)
        {
            playersInTurnOrder.Add(new Player(PlayerType.Human, playerName));
        }

        for (int i = 0; i < numOfAI; i++)
        {
            // Alice, Bob, Charlie, Alice 2.0
            playersInTurnOrder.Add(new Player(PlayerType.AI, i % aiNames.Count < aiNames.Count ? aiNames[i % aiNames.Count] : aiNames[i % aiNames.Count] + Mathf.Floor(i / aiNames.Count) + ".0"));
        }
    }

    public void ShuffleTurnOrder()
    {
        // Inefficient!
        playersInTurnOrder = playersInTurnOrder.OrderBy(x => Random.value).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNextTurn()
    {

        Player p = playersInTurnOrder[0];
        p.active = true;
        playersInTurnOrder.RemoveAt(0);
        playersInTurnOrder.Add(p);


        Debug.Log("New turn: " + p.name);

        FindObjectOfType<SpawnTetromino>().SpawnNewTetromino(p.id);
    }

    public void GetCurrentPlayer()
    {

    }

    public void EndTurn(int playerId)
    {
        foreach (Player player in playersInTurnOrder)
        {
            if (player.id == playerId)
            {
                player.active = false;
            }
        }
        StartNextTurn();
    }

    public void GameOver()
    {

        Debug.Log("Game Over!");
    }

    // Only works for one human...technically
    public bool IsHumanPlayerActive()
    {
        foreach (Player player in playersInTurnOrder)
        {
            if (player.active && player.playerType == PlayerType.Human)
                return true;
        }
        return false;
    }
}

public class Player
{
    public PlayerType playerType;
    public string name;
    public int id;
    public bool active = false;

    public Player(PlayerType playerType, string name, int id = 0)
    {
        this.playerType = playerType;
        this.name = name;
        this.id = id;

        if (id == 0)
        {
            int newId = 0;
            bool hasUniqueID = false;
            while (!hasUniqueID)
            {
                newId = Random.Range(1, 10000000);
                hasUniqueID = true;
                foreach (Player p in GameManager.instance.playersInTurnOrder)
                {
                    if (p.id == newId)
                    {
                        hasUniqueID = false;
                        break;
                    }
                }
            }

            this.id = newId;
        }
    }
}

public enum PlayerType
{
    Human,
    AI
}
