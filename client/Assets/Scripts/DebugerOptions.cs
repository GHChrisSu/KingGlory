using OT.Foundation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugerOptions : MonoBehaviour {
    public bool EnableLog = false;
    // Use this for initialization
    void Awake () {
        Debuger.EnableLog = EnableLog;
        Debuger.EnableSave = true;
        Debuger.Log("AppMain", "Start", Debuger.LogFileDir);
    }
	
}
