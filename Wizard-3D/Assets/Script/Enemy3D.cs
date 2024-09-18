using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D : MonoBehaviour
{
    float health = 30f;
    public float movementSpeed = 3f;
    bool isFrozen = false;
    Vector3 targetVector = Vector3.zero;

    void Start()
    {
        Debug.Log("Enemy spawned");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFrozen){
            NPCMoveToTarget();
        }
    }

    void OnMouseOver()
    {
        //Debug.Log("mouseOver");
        isFrozen = true;
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.CompareTag("Feuerball")) {
            EmotionalDamage(50f);
            Debug.Log("hit");
        }
    }

    void NPCMoveToTarget()
    {
        //Zielrichtung bestimmen
        targetVector = Wizard3D.player.transform.position - transform.position + new Vector3(0, 0.5f, 0);
        
        //Abschluss und Berechnung
        transform.position += Time.deltaTime * movementSpeed * targetVector.normalized;
    }

    public void EmotionalDamage(float damage)
    {
        //Eeeemoootionnaaaall Daaaaaammaage
        health -= damage;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
