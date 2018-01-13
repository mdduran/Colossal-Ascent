using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public OpCode Action;
    public byte Value;
    public int PlayerNum;

    public Event(OpCode action, byte value, int playerNum)
    {
        this.Action = action;
        this.Value = value;
        this.PlayerNum = playerNum;
    }
}
