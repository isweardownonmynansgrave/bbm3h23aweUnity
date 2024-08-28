using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    GameManager gm;

    void Awake()
    {
        gm = GameManager.gm;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.state == "pause") {
            gm.PauseMenu();
      }
    }
}
