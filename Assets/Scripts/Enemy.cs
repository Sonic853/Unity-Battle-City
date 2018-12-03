using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// 属性
	public float moveSpeed=3;
	protected Vector3 BulletEulerAngles;
	private Vector3 BulletEulerPosition=new Vector3(0,0.55f,0);
	// 引用
	public GameObject BulletPrefab;
	public Sprite[] tankSprite;
	public Sprite[] TankSkin = new Sprite[4];
	public GameObject Boom;
	public SpriteRenderer sr;
	public float moveTime=0;
	public int[] moveWhereList = new int[] {0,1,1,1,2,2,3,3,3,4,4,4,4};
	public int moveWhere=0;
	public float attackTime=0;
	public AudioClip DieAudio;



	// Use this for initialization
	void Start () {
		for(int j = 0; j < TankSkin.Length; j++){
        	TankSkin[j] = tankSprite[j];
        }
		attackTime = (float)(Random.Range(0,501)*0.01);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (moveTime > 0){
			Move(moveWhere);
			moveTime -= Time.deltaTime;
		}else{
			moveWhereList = moveWhereList.OrderBy(c => System.Guid.NewGuid()).ToArray<int>();
			moveWhere = moveWhereList[Random.Range(0, moveWhereList.Length)];
			moveTime = (float)(Random.Range(0,151)*0.01);
		}
		if(attackTime > 0){
			attackTime -= Time.deltaTime;
		}else{
			Attack();
			attackTime = (float)(Random.Range(10,391)*0.01);
		}
	}
	protected void Move(int where){
			float h = 0;
			float v = 0;
		if (where==0){
			h = 0;
			v = 0;
		}else if(where==1){
			h = 1;
			v = 0;
		}else if(where==2){
			h = 0;
			v = 1;
		}else if(where==3){
			h = -1;
			v = 0;
		}else if(where==4){
			h = 0;
			v = -1;
		}else{
			h = 0;
			v = 0;
		}
		if(h<0){
			sr.sprite=TankSkin[1];
			BulletEulerAngles = new Vector3(0,0,90);
			BulletEulerPosition = new Vector3(-0.55f,0,0);
			transform.Translate(Vector3.right*h*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			if(v!=0){
				return;
			}
		}else if(h>0){
			sr.sprite=TankSkin[3];
			BulletEulerAngles = new Vector3(0,0,-90);
			BulletEulerPosition = new Vector3(0.55f,0,0);
			transform.Translate(Vector3.right*h*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			if(v!=0){
				return;
			}
		}else if(v<0){
			sr.sprite=TankSkin[2];
			BulletEulerAngles = new Vector3(0,0,180);
			BulletEulerPosition = new Vector3(0,-0.55f,0);
			transform.Translate(Vector3.up*v*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}else if(v>0){
			sr.sprite=TankSkin[0];
			BulletEulerAngles = new Vector3(0,0,0);
			BulletEulerPosition = new Vector3(0,0.55f,0);
			transform.Translate(Vector3.up*v*moveSpeed*Time.fixedDeltaTime,Space.World);
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}else if(h==0&&v==0){
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		}
	}
	// 坦克攻击
	private void Attack(){
		GameObject prefabInstance = Instantiate(BulletPrefab,transform.position+BulletEulerPosition,Quaternion.Euler(transform.eulerAngles+BulletEulerAngles));
		prefabInstance.transform.parent = GameObject.Find("Bullets").transform;
	}
	protected void Die(){
		PlayerManager.Instance.playerScore++;
		// boom!
		Instantiate(Boom,transform.position,transform.rotation);
		AudioSource.PlayClipAtPoint(DieAudio,transform.position);
		// Enemy Dead
		Destroy(gameObject);
	}
}
