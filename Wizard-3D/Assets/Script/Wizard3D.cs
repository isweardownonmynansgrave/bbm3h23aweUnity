using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wizard3D : MonoBehaviour
{
    //
    public static Wizard3D player;
    public static PlayerStats stats;
    Animator animator;
    public float movementSpeed;

    //Attacke
    public Object fireballPrefab;
    float cooldown;
    Vector3 lastDirection;
    Vector3 bulletPointPosition;

    void Start()
    {
        
    }

    void Awake()
    {
        movementSpeed = 4f;
        stats = new PlayerStats();
        player = this;
        cooldown = 0f;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("attack", false);
        animator.SetBool("walk", false);
        animator.SetBool("walkLeft", false);
        animator.SetBool("walkRight", false);
        animator.SetBool("walkBack", false);
        Movement();

        //Angriff und Freeze
        if (Input.GetMouseButton(0) && cooldown <= 0)
		{
			ShootFireball();
            cooldown = 3f;
		}
        if(Input.GetMouseButton(1) && stats.getCurrentMana() > 0) {
            FreezeAttack();
        }
        if(Input.GetMouseButtonUp(1)){
            animator.SetBool("freeze", false);
        }
        if(cooldown > 0){
            cooldown -= Time.deltaTime;
        }

    }

    public void Movement()
    {
        //Reset
        Vector3 movement = Vector3.zero;

        //Input Abfrage
        if(Input.GetKey("w") || Input.GetKey("s")){
            if(Input.GetKey("w") && Input.GetKey("s")){
                //Bleibt zero
            } else {
                animator.SetBool("walk", true);
                if (Input.GetKey("w")){
                    movement += Vector3.forward;
                }
                if (Input.GetKey("s")){
                    movement += Vector3.back;
                }
            }
        }
        if(Input.GetKey("a") || Input.GetKey("d")){
            if(Input.GetKey("a") && Input.GetKey("d")){
                //Bleibt zero
            } else {
                animator.SetBool("walk", true);
                if (Input.GetKey("a")){
                    movement += Vector3.left;
                }
                if (Input.GetKey("d")){
                    movement += Vector3.right;
                }
            }
        }
        
        
        if (movement.magnitude > 0)
        {
            lastDirection = movement;
            Rotate();
            transform.position += movement.normalized * Time.deltaTime * movementSpeed;
            bulletPointPosition = gameObject.transform.Find("root/pelvis/Weapon/bulletPoint").transform.position;
        } 
    }

    void Rotate()
    {
        float angle = Vector3.Angle(Vector3.forward, lastDirection);

        if (lastDirection.x < 0)
        {
            angle = angle*-1;
        }
        transform.rotation = Quaternion.Euler(0,angle,0);
    }
    public void ShootFireball()
    {
        Object fb = Instantiate(fireballPrefab, bulletPointPosition + lastDirection + Vector3.forward, Quaternion.identity);
        fb.GetComponent<Feuerball3D>().movement = lastDirection;
        animator.SetBool("attack", true);
        stats.setCurrentMana(stats.getCurrentMana()-10);
    }
    void FreezeAttack()
    {
        Debug.Log("Freeze triggered");
        animator.SetBool("freeze", true);
    }
}
