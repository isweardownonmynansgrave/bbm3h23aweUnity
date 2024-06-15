using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public GameObject steinPrefab;
    public GameObject diamantPrefab;
    Sprite[] spriteImage;
    public int reihen; //4 - Editor Voreinstellung
    public int spalten; //5 - Unity Voreinstellung
    int anzahlSteine;
    int punkte = 0;
    public int versuche; //60% der Anzahl von Steinen
    int dias; //20% der Anzahl von Steinen
    GameObject[] arr;

    void Start()
    {
        gm = this; //singleton Referenz

        //Kennzahlen & Array initialisieren
        anzahlSteine = reihen * spalten; 
        versuche = (int) (anzahlSteine * 0.6);
        dias = (int) (anzahlSteine * 0.2);
        arr = new GameObject[anzahlSteine];

        //Stein sprites - Lade alle Sprites aus dem "Steine" Ordner in den spriteImage Array
        spriteImage = Resources.LoadAll<Sprite>("Steine");

        //Steine generieren
        float x = -4f;
        float y = 3f;
        int steinCount = 0;
        int randomSteinSprite = 0;
        for (int i = 0; i < reihen; i++) {
            for (int j = 0; j < spalten; j++) {
                //Stein Klon anlegen
                arr[steinCount] = Instantiate(steinPrefab, new Vector3(x,y,0), Quaternion.identity); 
                
                //Zufälligen Sprite für den Stein auswählen
                randomSteinSprite = Random.Range(0,4);
                arr[steinCount].GetComponent<SpriteRenderer>().sprite = spriteImage[randomSteinSprite];
                
                //Koordinaten zum Anlegen verschieben und Zähler erhöhen
                x = x + 2;
                steinCount++;
            }
            x = -4;
            y = y - 2;
        }
        DiasVerteilen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DiasVerteilen () {
        int randomDia = 0;
        for (int i = 0; i < this.dias; i++) {
            //Zufälligen Index für Steinarray anlegen
            randomDia = Random.Range(0,(anzahlSteine-1));
            
            //Diamanten zuweisen
            arr[randomDia].GetComponent<Stein>().setHatDia(true);
        }
    }

    public void lotterie (Stein s) {
        this.versuche--;
        if (s.getHatDia()) {
            this.punkte++;
            this.dias--;
            Instantiate(diamantPrefab,s.transform.position,Quaternion.identity);
        }
    }

    //Getter
    public int getVersuche () {
        return this.versuche;
    }
    public int getPunkte () {
        return this.punkte;
    }
    public int getDias() {
        return this.dias;
    }
}
