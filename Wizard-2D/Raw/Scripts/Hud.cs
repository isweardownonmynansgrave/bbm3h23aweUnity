using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text lvlsysText;
    public TMP_Text warningText;
    public Image healthBarImg;
    public Image manaBarImg;
    public Image expBarImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gm = GameManager.gm;
        Wizard player = Wizard.player;
        scoreText.text = "Score: " + gm.getScore();
        healthText.text = player.stats.getHealth()+ " / " + player.stats.getMaxHealth();
        manaText.text = player.stats.getCurrentMana() + " / " + player.stats.getMaxMana();
        lvlsysText.text = player.stats.getExperienze() + " / "+player.stats.getFibSum()+" Exp";
        lvlsysText.text +="\nLevel: " + player.stats.getLevel();
        //warningText.text = player.stats.warningBuffer;

        //Gesundheitsanzeige
        float healthPercentage = (float) player.stats.getHealth() / (float) player.stats.getMaxHealth();
        healthBarImg.transform.localScale = new Vector3(healthPercentage, 1,1 );
        //Manaanzeige
        float manaPercentage = (float) player.stats.getCurrentMana() / (float) player.stats.getMaxMana();
        manaBarImg.transform.localScale = new Vector3(manaPercentage, 1,1 );

        //Erfahrungsanzeige WIP
        float expPercentage = (float) player.stats.getExperienze() / (float) player.stats.getFibSum();
        //expBarImg.transform.localScale = new Vector3(expPercentage, 1,1 );

        if (player.stats.getWarning() != "") {
            warningText.text = player.stats.getWarning();
        } else {
            warningText.text = "";
        }
    }
    

}
