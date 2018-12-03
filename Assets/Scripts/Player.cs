using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// 属性
	public float moveSpeed=3;
	private Vector3 BulletEulerAngles;
	private Vector3 BulletEulerPosition=new Vector3(0,0.55f,0);
	private float timeVal;
	private float DefendTimeVal=4;//吃道具后无敌10秒
	private bool isDefended=true;
	// 引用
	private SpriteRenderer sr;
	public Sprite[] tankSprite;//上左下右
	public GameObject BulletPrefab;
	public GameObject Boom;
	public GameObject DefendEffectPrefab;
	public AudioClip FireAudio;
	public AudioClip DieAudio;
	public AudioSource moveAudio;
	public AudioClip[] tankAudio;

	private void Awake() {
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// 无敌
		if(isDefended){
			DefendEffectPrefab.SetActive(true);
			DefendTimeVal -= Time.deltaTime;
			if(DefendTimeVal<=0){
				isDefended=false;
				DefendEffectPrefab.SetActive(false);
			}
		}
	}
	void FixedUpdate() {
		if(PlayerManager.Instance.isDefeat){
			if(moveAudio.isPlaying)moveAudio.Stop();
			return;
		}
		Move();
		// 攻击
		if(GameObject.Find("Player1Bullets").transform.childCount < 5&&timeVal>=0.1f){
			Attack();
		}else{
			timeVal += Time.deltaTime;
		}
	}
	// 坦克攻击
	private void Attack(){
		if(Input.GetKeyDown(KeyCode.Space)){
			AudioSource.PlayClipAtPoint(FireAudio,transform.position);
			GameObject prefabInstance = Instantiate(BulletPrefab,transform.position+BulletEulerPosition,Quaternion.Euler(transform.eulerAngles+BulletEulerAngles));
			prefabInstance.transform.parent = GameObject.Find("Player1Bullets").transform;
			timeVal = 0;
		}
	}
	// 坦克移动
	private void Move(){
		float h = Input.GetAxisRaw("Horizontal");// -1 或 1 、0
		float v = Input.GetAxisRaw("Vertical");// -1 或 1 、0
		if(h<0){
			sr.sprite=tankSprite[1];
			BulletEulerAngles = new Vector3(0,0,90);
			BulletEulerPosition = new Vector3(-0.55f,0,0);
			transform.Translate(Vector3.right*h*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			if(v!=0)return;
		}else if(h>0){
			sr.sprite=tankSprite[3];
			BulletEulerAngles = new Vector3(0,0,-90);
			BulletEulerPosition = new Vector3(0.55f,0,0);
			transform.Translate(Vector3.right*h*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			if(v!=0)return;
		}else if(v<0){
			sr.sprite=tankSprite[2];
			BulletEulerAngles = new Vector3(0,0,180);
			BulletEulerPosition = new Vector3(0,-0.55f,0);
			transform.Translate(Vector3.up*v*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}else if(v>0){
			sr.sprite=tankSprite[0];
			BulletEulerAngles = new Vector3(0,0,0);
			BulletEulerPosition = new Vector3(0,0.55f,0);
			transform.Translate(Vector3.up*v*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}else if(h==0&&v==0){
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		}
		if(Mathf.Abs(v)>0.05f||Mathf.Abs(h)>0.05f){
			moveAudio.clip=tankAudio[1];
			if(!moveAudio.isPlaying)moveAudio.Play();
		}else{
			moveAudio.clip=tankAudio[0];
			if(!moveAudio.isPlaying)moveAudio.Play();
		}
	}
	private void Die(){
		if(isDefended)return;
		PlayerManager.Instance.isDead=true;
		// boom!
		Instantiate(Boom,transform.position,transform.rotation);
		AudioSource.PlayClipAtPoint(DieAudio,transform.position);
		// You Dead
		Destroy(gameObject);
	}
}
