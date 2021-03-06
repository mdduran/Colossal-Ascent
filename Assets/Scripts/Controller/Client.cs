﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Client
{
    public IPEndPoint EndPoint;
    public UdpClient Listener;
    public int Port;
    public int PlayerNum;

    public Queue Events;

    public Thread Self;
    public bool Running;

    public Client(int PlayerNum, IPEndPoint EndPoint, int Port, Queue EventsQueue)
    {
        this.EndPoint = EndPoint;
        Debug.Log(EndPoint + "");
        this.Port = Port;
        this.PlayerNum = PlayerNum;
        Events = EventsQueue;

        Running = true;
        Self = new Thread(Run);
        Self.Start();
    }

    public void Stop()
    {
        Running = false;
    }

    public void Run()
    {
        Listener = new UdpClient(Port);

        try
        {
            while (Running)
            {
                byte[] bytes;
                if (Listener.Available > 0)
                {
                    bytes = Listener.Receive(ref EndPoint);
                    // Parse Bytes into event
                    Event e = OperationParser.Recieve(bytes, 0); // TODO multi controller player num
                    //Debug.Log(e.Action + " " + e.Value);
                    Events.Enqueue(e);
                }
                //else
                //    Thread.Sleep(10);

            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
        finally
        {
            Listener.Close();
            Listener = null;
        }
    }

    public void Send(byte[] bytes)
    {
        if(Listener != null)
            Listener.Send(bytes, bytes.Length, EndPoint);
    }

    public void LED() // TODO add to GameManager so we can change LEDs in game
    {
        //bytes[0] = (byte)OpCode.SetLEDPixel;
        ByteColor color = new ByteColor();
        color.red = 0;
        color.blue = 0xFF;
        color.green = 0;
        byte[] colorChange = new byte[5];
        colorChange[0] = (byte)OpCode.SetLEDPixel;
        byte[] pixelColor = new byte[4];
        pixelColor = OperationParser.SetLEDPixel(0, color);
        Array.Copy(pixelColor, 0, colorChange, 1, pixelColor.Length);
        Thread.Sleep(100);
        Send(colorChange);
        byte[] updateByte = new byte[1];
        updateByte[0] = (byte)OpCode.UpdateLEDs;
        Send(updateByte);
    }

    public void SliderToLed(Event e)
    {
        if (e.Action == OpCode.StripPot)
        {
            byte[] colorChange = new byte[5];
            byte hue = e.Value;

            Color newColor = Color.HSVToRGB((float)hue / 255, 1, 1);
            ByteColor byteColor;

            byteColor.red = BitConverter.GetBytes(newColor.r)[0];
            byteColor.green = BitConverter.GetBytes(newColor.g)[0];
            byteColor.blue = BitConverter.GetBytes(newColor.b)[0];
            colorChange[0] = (byte)OpCode.SetLEDPixel;

            byte[] newBytes = new byte[4];
            newBytes = OperationParser.SetLEDPixel(0, byteColor);
            Array.Copy(newBytes, 0, colorChange, 1, 4);
            Send(colorChange);
            byte[] updateByte = new byte[1];
            updateByte[0] = (byte)OpCode.UpdateLEDs;
            Send(updateByte);
        }
    }

}