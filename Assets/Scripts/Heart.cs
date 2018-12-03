using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	private SpriteRenderer sr;
	public Sprite BrokenSprite;
	public GameObject Explode2;
	public AudioClip DieAudio;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die(){
		if (sr.sprite != BrokenSprite){
			sr.sprite = BrokenSprite;
			Instantiate(Explode2,transform.position,transform.rotation);
			PlayerManager.Instance.isDefeat = true;
			AudioSource.PlayClipAtPoint(DieAudio,transform.position);
		}
	}
}
