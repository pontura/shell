﻿using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action ResetApp = delegate { };
    public static System.Action HideOldScreens = delegate { };
    public static System.Action<string> GotoTo = delegate { };
    public static System.Action<string> GotoBackTo = delegate { };
    public static System.Action Back = delegate { };

    public static System.Action<string, string, System.Action> PlaySoundTillReady = delegate { };
    public static System.Action<string, string, bool> PlaySound = delegate { };
    public static System.Action<string, float> ChangeVolume = delegate { };

    public static System.Action<string> Log = delegate { };

}
   
