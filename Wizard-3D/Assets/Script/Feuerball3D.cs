using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feuerball3D : MonoBehaviour
{
    public float bulletSpeed;
    public Vector3 movement = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 5f;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * bulletSpeed * movement;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            c.GetComponent<Enemy3D>().EmotionalDamage(50);
            Wizard3D.stats.GainXp(1);
        }
    }
}
