using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
	public static Wizard player;
	public static PlayerStats stats;
	public GameObject fireballPrefab;
	public float movementSpeed = 4.0f;
	float cooldown = 0f;
	float cooldownHealth = 0f;
	float fbRotate = 0;

	private Animator animator;
    // Start is called before the first frame update
	void Awake()
	{
		stats = new PlayerStats();
	}
    void Start()
    {
		player = this;
        animator = GetComponent<Animator>();
		
    }

    // Update is called once per frame
    void Update()
    {
		animator.SetBool("Walking", false);
        Vector3 movement = Vector3.zero;

		//HP Regeneration
 		cooldownHealth -= Time.deltaTime;
		if (cooldownHealth <= 0) {
			stats.regenerateHealth();
			cooldownHealth = 2;
			}

		//Movement - Horizontal
		if (Input.GetKey("a") || Input.GetKey("d"))
		{
			if (Input.GetKey("a") && Input.GetKey("d"))
			{
				movement += Vector3.zero; //A und D negieren sich gegenseitig
			} 
			else
			{
				if (Input.GetKey("d")) 
				{
					movement += Vector3.right;
					fbRotate = 0; //Fireball rechts
					animator.SetBool("Walking", true);
					player.GetComponent<SpriteRenderer>().flipX = false;
				} 
				if (Input.GetKey("a")) 
				{
					movement += Vector3.left;
					fbRotate = 180; //Fireball links
					animator.SetBool("Walking", true);
					player.GetComponent<SpriteRenderer>().flipX = true;
				}
			}
		}
		//Movement - Vertikal
		if (Input.GetKey("w") || Input.GetKey("s"))
		{
			if (Input.GetKey("w") && Input.GetKey("s"))
			{
				movement += Vector3.zero; //W und S negieren sich gegenseitig
			} 
			else
			{
				if (Input.GetKey("w")) 
				{
					movement += Vector3.up;
					fbRotate = 90; //Fireball hoch
					animator.SetBool("Walking", true);
				} 
				if (Input.GetKey("s")) 
				{
					movement += Vector3.down;
					fbRotate = 270; //Fireball runter
					animator.SetBool("Walking", true);
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
		{
			shootFireball();
		}
	cooldown -= Time.deltaTime;
    transform.position += movement.normalized * Time.deltaTime * movementSpeed;
    }


	void shootFireball () {
		if(stats.getCurrentMana() >= 10){ //Pruefe ob Mana mind. 10, um Feuerball casten zu koennen
			GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
			fireball.transform.Rotate(0,0,fbRotate);
			animator.SetBool("Attack", true);
			cooldown = 2;
			stats.setCurrentMana(stats.getCurrentMana()-10);
			stats.warningBuffer = "";
		}else {
			//Alternative Kein Mana
			stats.warningBuffer = "Mana zu niedrig!";
		}
	}
	//Wird vom Animation Event beim WizardAttack aufgerufen
	void endAttack()
	{
		animator.SetBool("Attack", false);
	}
}
