using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	Vector3 movement;
	public float bulletSpeed = 4.0f; //Geschwindigkeit Fireball
    // Start is called before the first frame update
    void Start()
    {
		movement = Vector3.zero;
        Destroy(gameObject,3);
		float r = transform.rotation.eulerAngles.z;
		switch (r)
		{
			case 0:
				movement = Vector3.right;
				break;
			case 45:
				movement = Vector3.right;
				movement += Vector3.up;
				break;
			case 90: 
				movement = Vector3.up;
				break;
			case 135:
				movement = Vector3.up;
				movement += Vector3.left;
				break;
			case 180:
				movement = Vector3.left;
				break;
			case 225:
				movement = Vector3.left;
				movement += Vector3.down;
				break;
			case 270:
				movement = Vector3.down;
				break;
			case 315:
				movement = Vector3.down;
				movement += Vector3.right;
				break;
		}
    }

    // Update is called once per frame
    void Update()
    {
		transform.position += movement.normalized * Time.deltaTime * bulletSpeed;
    }

	void OnCollisionEnter2D () {
		if (tag != "Player"){
			Destroy(gameObject);
		}
	}
}
