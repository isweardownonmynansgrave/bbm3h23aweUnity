using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    public TMP_Text versucheText;
    public TMP_Text punkteText;
    public TMP_Text diasText;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gm = GameManager.gm;
        versucheText.text = "Versuche: " + gm.getVersuche();
        punkteText.text = "Punkte: " + gm.getPunkte();
        diasText.text = "Diamanten übrig: " + gm.getDias();
    }
}
