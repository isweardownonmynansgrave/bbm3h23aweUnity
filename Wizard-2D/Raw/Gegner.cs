using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gegner : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = Wizard.player.transform.position;
        Vector3 distanceVector = playerPosition - transform.position;
        distanceVector = distanceVector.normalized;
        transform.position += distanceVector * Time.deltaTime;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") {
            Wizard.stats.setHealth(Wizard.stats.getHealth()-10);
            Destroy(gameObject);
            return;
        }
        if (col.gameObject.tag == "Fireball") {
            Destroy(col.gameObject);
            return;
        }
    }
}
