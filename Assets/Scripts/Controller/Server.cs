using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Server
{
    public Queue EventQueue;
    public int AcceptingPort;
    public int CommunicationPort;
    public int MaxClients;
    public List<Client> Clients;
    public bool Running;
    public int BroadcastFrequency;
    public bool EnableBroadcasts;

    private Thread thread;
    private Thread broadcastThread;

    public Server(int AcceptingPort, int CommunicationPort, Queue EventQueue)
    {
        this.AcceptingPort = AcceptingPort;
        this.CommunicationPort = CommunicationPort;
        this.EventQueue = EventQueue;
        BroadcastFrequency = 15;
    }

    public void Start()
    {
        Clients = new List<Client>();

        // Start thread
        thread = new Thread(Run);
        thread.Start();
        broadcastThread = new Thread(Broadcast);
        broadcastThread.Start();

        Running = true;

        Debug.Log("Starting Server on port: " + AcceptingPort);
        Broadcast();
    }

    public void Stop()
    {
        Running = false;
        foreach (Client c in Clients)
            c.Stop();
        thread.Join();
    }

    public void Run()
    {
        UdpClient listener = new UdpClient(AcceptingPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, AcceptingPort);

        try
        {
            while (Running)
            {
                groupEP = new IPEndPoint(IPAddress.Any, AcceptingPort);
                byte[] bytes;
                if (listener.Available > 0)
                {
                    bytes = listener.Receive(ref groupEP);
                    if(bytes[0] == (byte) OpCode.Connect) 
                    {
                        Debug.Log(groupEP + " Connected. " + bytesToString(bytes));
                        CreateClient(groupEP);
                        bytes[0] = (byte)OpCode.Accept;
                        listener.Send(bytes, 1, groupEP);
                    }                   
                }
                else
                    Thread.Sleep(50);

            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
        finally
        {
            listener.Close();
        }
    }

    public void CreateClient(IPEndPoint ep)
    {
        Clients.Add(new Client(0, ep, CommunicationPort, EventQueue));
    }

    public void Broadcast()
    {
        
        try
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);

            IPAddress broadcast = IPAddress.Parse("192.168.43.255");
            IPEndPoint ep = new IPEndPoint(broadcast, 4210);

            while(Running) 
            {
                if(EnableBroadcasts)
                {
                    Debug.Log("Broadcasting...");
                    s.SendTo(OperationParser.Broadcast(AcceptingPort), ep);
                }

                Thread.Sleep(BroadcastFrequency * 1000);
            }

            s.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

    public static string bytesToString(byte[] bytes)
    {
        string s = "";
        foreach(byte b in bytes)
        {
            s += b.ToString() + " ";
        }
        return s;
    }
}
