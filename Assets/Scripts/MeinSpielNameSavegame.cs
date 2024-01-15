using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeinSpielNameSavegame
{
    // Start is called before the first frame update
    public int lastLevel;
    public bool[] beatenLevels;
    public bool skipTutorial;
    public float lastXPosition;
    public float lastYPosition;

}
