using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // UI
    public UIManager UI;

    // Prefabs
    public GameObject PlayerPrefab;
    public GameObject LevelPrefab;

    // Game Objects
    private MovingCamera mc;
    private GameObject level;
    private List<QuickPlayer> players;

    // Server
    private Server server;
    public int AcceptingPort;
    public int CommunicationPort;
    private Queue InputEventQueue;

    // Music
    public AudioClip MenuMusic;
    public AudioClip PlayMusic;
    private AudioSource MusicPlayer;

    // State
    private GameState currentState;
    private GameState State
    {
        get
        {
            return currentState;
        }
        set
        {
            switch(value)
            {
                case GameState.Menu:
                    //MusicPlayer.clip = MenuMusic;
                    server.EnableBroadcasts = true;
                    break;
                case GameState.Play:
                    //MusicPlayer.clip = PlayMusic;
                    server.EnableBroadcasts = false;
                    break;
                default:
                    break;
            }
            currentState = value;
        }
    }



	// Use this for initialization
	void Start () {
        InputEventQueue = Queue.Synchronized(new Queue());
        server = new Server(AcceptingPort, CommunicationPort, InputEventQueue);
        players = new List<QuickPlayer>();
        server.Start();
        mc = GetComponent<MovingCamera>();
        InitializeMenu();
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(State)
        {
            case GameState.Menu:
                break;
            case GameState.Play:
                Cycle();
                break;
            case GameState.Pause:
                break;
        }
	}

    private void OnApplicationQuit()
    {
        server.Stop();
    }

    public void DestroyWorld()
    {
        // Destroy Level
        Destroy(level);
        // Destroy Players
        foreach (QuickPlayer p in players)
            Destroy(p.gameObject);
        players = new List<QuickPlayer>();

    }

    public void InitializeMenu()
    {
        DestroyWorld();
        // Spawn prefab
        level = Instantiate(LevelPrefab);

        // set camera position
        Camera.main.transform.position = Vector2.zero;
        // TODO set camera goal point

        State = GameState.Menu;
    }

    public void InitializeGame()
    {
        DestroyWorld();

        // Spawn prefab
        level = Instantiate(LevelPrefab);

        // set camera position
        Camera.main.transform.position = Vector2.zero;
        // TODO set camera goal point
        

        // spawn player
        GameObject newPlayer = Instantiate(PlayerPrefab, new Vector2(0.0f, 3.0f), new Quaternion());
        players.Add(newPlayer.GetComponent<QuickPlayer>());

        State = GameState.Play;
        UI.Screen = UserInterfaceScreens.None;
    }

    public void Cycle()
    {
        // check events

        if (Input.GetKeyDown(KeyCode.Space))
            players[0].Jump();

        while(InputEventQueue.Count > 0)
        {
            Event e = (Event)InputEventQueue.Dequeue();
            switch(e.Action) // TODO handle each event
            {
                case OpCode.StripPot:
                    players[e.PlayerNum].Goal = e.Value / 255.0f;
                    break;
                case OpCode.RotPot:
                    break;
                case OpCode.JumpButton:
                    players[e.PlayerNum].Jump();
                    break;
                case OpCode.FireButton:
                    break;
                case OpCode.StartButton:
                    break;
                case OpCode.ExitButton:
                    break;
            }
        }


    }

    public void Pause()
    {
        // Pause Camera
        mc.isPaused = true;
        // Set Enemies Paused
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponentInChildren<Enemy>().isPaused = true;
        }

        State = GameState.Pause;
    }

    public void Resume()
    {
        // Unpause Camera
        mc.isPaused = false;
        // Unpause Enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponentInChildren<Enemy>().isPaused = false;
        }

        State = GameState.Play;
    }

    public void GoalReached()
    {
        // Set End Text
        UI.SetEndText("You Win!");
        // Show End Panel
        UI.Screen = UserInterfaceScreens.EndMenu;
        State = GameState.Menu;
    }

    public void PlayerDied()
    {
        // Set End Text
        UI.SetEndText("You Died!");
        // Show End Panel
        UI.Screen = UserInterfaceScreens.EndMenu;
        State = GameState.Menu;
    }
}
