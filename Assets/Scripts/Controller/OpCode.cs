﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpCode
{
    Error,
    Broadcast,
    Connect,
    Disconnect,
    Accept,
    Reject,
    StripPot,
    RotPot,
    FireButton,
    JumpButton,
    ExitButton,
    StartButton,
    SetLEDStrip,
    SetLEDPixel,
    ClearLEDs,
    UpdateLEDs,
    KeepAlive
}

