using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Server server;

    public int AcceptingPort;
    public int CommunicationPort;

	// Use this for initialization
	void Start () {
        server = new Server();
        server.AcceptingPort = AcceptingPort;
        server.CommunicationPort = CommunicationPort;
        server.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnApplicationQuit()
    {
        server.Stop();
    }
}
