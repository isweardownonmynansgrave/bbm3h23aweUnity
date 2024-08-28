using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public GameObject targetPrefab;
	float timeLeft = 4f;
	int randomTarget;
	//Array mit verschiedenen Target Sprites
	//Animation Komponente einbauen und befüllen?
	AnimationClip[] targetAnimation;
	Animator targetAnimator;
	AnimatorOverrideController animOverCon;
	
	
    // Start is called before the first frame update
    void Start()
    {
		//Setup des zufälligen Targets
		targetAnimation = new AnimationClip[3];
		//targetAnimation = Resources.LoadAll("Anims"); <- Resources funktioniert nicht mit Clips, nur mit GameObjects
		Debug.Log("targetAnimation Length: "+targetAnimation.Length);
		targetAnimator = gameObject.GetComponent<Animator>();
		animOverCon = new AnimatorOverrideController(targetAnimator.runtimeAnimatorController);

		//Einsetzen der zufälligen Animation
		randomTarget = Random.Range(0,1);
		animOverCon["IDLE"] = targetAnimation[randomTarget];
    }

    // Update is called once per frame
    void Update()
    {
		
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
    }
	void OnCollisionEnter2D(Collision2D col)
	{
		GameManager gm = GameManager.gm;
		string tag = col.gameObject.tag;
		if (tag == "Fireball"){
			NewTarget();
			Destroy(gameObject);
			Debug.Log("Time left added to Exp: "+ (int) timeLeft);
			gm.addScore((int) timeLeft);
			Wizard p = Wizard.player;
			p.stats.GainXp((int) (timeLeft * 2));
		}

	}
	void NewTarget()
	{
		//Koordinaten generieren und neues Targetobjekt instanziieren
		float x = Random.Range(-6, 6);
		float y = Random.Range(-5, 5);
		GameObject nt = Instantiate(targetPrefab, new Vector3(x,y,0), Quaternion.identity);

		//Animator und Controller für neues Target vorbereiten
		targetAnimator = nt.GetComponent<Animator>();
		animOverCon = new AnimatorOverrideController(targetAnimator.runtimeAnimatorController);

		//Einsetzen der zufälligen Animation
		randomTarget = Random.Range(0,1);
		animOverCon["IDLE"] = targetAnimation[randomTarget];
		Debug.Log("randomTarget: "+randomTarget);
	}
}