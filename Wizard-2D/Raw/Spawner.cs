using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    float timer;
    public GameObject[] enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0){
            GameObject toSpawn = enemyPrefab[Random.Range(0,3)];
            Vector3 posi = new Vector3(Random.Range(-5f,5f),Random.Range(-5f,5f),0);
            Instantiate(toSpawn, posi, Quaternion.identity);
            timer = 3f;
        }

        //Necromancer spawnt weitere gegner
        //Gegner, der zur letzten (bei spawn) position des Spielers läuft
        //Optional: Ihr hab einen enemy, aber reicht einer?
    }
}
