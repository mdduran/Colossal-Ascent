using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct color
{
    byte red;
    byte green;
    byte blue;
}

public class OperationParser {
    public byte[] controllerData;
    public int port;
    

    // TODO: Send the event to the game manager
    public Event Recieve(byte[] data, int playerNumber)
    {
        //parse the data
        OpCode operation = (OpCode)data[0];
        Event newEvent;
        switch (operation)
        {
            case OpCode.Connect:
                newEvent = new Event(OpCode.Connect, 0, playerNumber);
                break;
            case OpCode.Disconnect:
                newEvent = new Event(OpCode.Disconnect, 0, playerNumber);
                break;
            case OpCode.StripPot:
                newEvent = new Event(OpCode.StripPot, data[1], playerNumber);
                break;
            case OpCode.RotPot:
                newEvent = new Event(OpCode.RotPot, data[1], playerNumber);
                break;
            case OpCode.ExitButton:
                newEvent = new Event(OpCode.ExitButton, data[1], playerNumber);
                break;
            case OpCode.StartButton:
                newEvent = new Event(OpCode.StartButton, data[1], playerNumber);
                break;
            case OpCode.FireButton:
                newEvent = new Event(OpCode.FireButton, data[1], playerNumber);
                break;
            case OpCode.JumpButton:
                newEvent = new Event(OpCode.JumpButton, data[1], playerNumber);
                break;
            case OpCode.KeepAlive:
                newEvent = new Event(OpCode.KeepAlive, 0, playerNumber);
                break;
            default:
                newEvent = new Event(OpCode.Error, 0, playerNumber);
                break;
        }
        return newEvent;
    }

    public byte[] Broadcast(int port)
    {
        //Add the opcode with the port
        byte[] data = new byte[5];
        data[0] = (byte)OpCode.Broadcast;
        byte[] portBytes = BitConverter.GetBytes(port);
        Array.Copy(portBytes, 0, data, 1, 4);
        return data;
    }

    /*SimpleSend*/
    public byte[] SimpleSend(OpCode currentOp)
    {
        byte[] data = new byte[1];
        data[0] = (byte)currentOp;
        return data;
    }

    /*Set LED Strip*/
    public byte[] SetLEDStrip(byte startPixel, byte endPixel, color[] colors)
    {
        byte[] data = new byte[18]; //TODO Do the proper math


        return data;

    }

    /*Set LED Pixel*/
    public byte[] SetLEDPixel(byte pixelIndex, color newColor)
    {
        byte[] data = new byte[5];
        //DO stuff
        return data;
    }

    
	
}
