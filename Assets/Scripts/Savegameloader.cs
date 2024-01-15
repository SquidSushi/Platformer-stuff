using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savegameloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SavegameManager.Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavegameManager.loadedSavegame.lastLevel++;
            SavegameManager.Save();
        }
    }
}
