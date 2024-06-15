using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stein : MonoBehaviour
{
    GameManager gm; //GM Referenz
    bool hatDia = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() 
    {
        if (Input.GetMouseButtonDown(0) && gm.getVersuche() > 0){ 
            gm.lotterie(this);
            Debug.Log("Klick "+this.hatDia); //Debug
            Destroy(gameObject);
        }
    }

    //Getter & setter
    public void setHatDia (bool a) {
        this.hatDia = a;
    }
    public bool getHatDia () {
        return this.hatDia;
    }
}
