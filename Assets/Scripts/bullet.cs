using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public float MoveSpeed = 9;
	public bool isPlayerBullect = true;
	public bool isPlayer2Bullect = false;
	public GameObject Explode1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(transform.up*MoveSpeed*Time.deltaTime,Space.World);
	}
	private void OnTriggerEnter2D(Collider2D collision){
		switch (collision.tag){
			case "Tank":
				if(!isPlayerBullect){
					collision.SendMessage("Die");
					Destroy(gameObject);
				}
				break;
			case "Heart":
				collision.SendMessage("Die");
				Destroy(gameObject);
				break;
			case "Enemy":
				if(isPlayerBullect||isPlayer2Bullect){
					collision.SendMessage("Die");
					Destroy(gameObject);
				}
				break;
			case "Wall":
				Destroy(collision.gameObject);
				Instantiate(Explode1,transform.position,transform.rotation);
				Destroy(gameObject);
				break;
			case "Barriar":
				Instantiate(Explode1,transform.position,transform.rotation);
				Destroy(gameObject);
				break;
			case "Bullet":
				if(this.tag != collision.tag){
					Destroy(collision.gameObject);
					Destroy(gameObject);
				}
				break;
			default:
				break;
		}
	}
}
