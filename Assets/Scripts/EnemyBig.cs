using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBig : Enemy {
	// 属性
	public int TankHP=4;
	// 引用


	// Use this for initialization
	void Start () {
		TankHP=4;
		Change();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	protected new void Die(){
		if(TankHP>1){
			TankHP-=1;
			Change();
		}else{
			PlayerManager.Instance.playerScore++;
			// boom!
			Instantiate(Boom,transform.position,transform.rotation);
			AudioSource.PlayClipAtPoint(DieAudio,transform.position);
			// Enemy Dead
			Destroy(gameObject);
		}
	}
	private void Change(){
		switch (TankHP){
			case 4:
				TankSkin[0]=tankSprite[12];
				TankSkin[1]=tankSprite[13];
				TankSkin[2]=tankSprite[14];
				TankSkin[3]=tankSprite[15];
			break;
			case 3:
				TankSkin[0]=tankSprite[8];
				TankSkin[1]=tankSprite[9];
				TankSkin[2]=tankSprite[10];
				TankSkin[3]=tankSprite[11];
				ChangeNow(12,13,14,15,8,9,10,11);
			break;
			case 2:
				TankSkin[0]=tankSprite[4];
				TankSkin[1]=tankSprite[5];
				TankSkin[2]=tankSprite[6];
				TankSkin[3]=tankSprite[7];
				ChangeNow(8,9,10,11,4,5,6,7);
			break;
			case 1:
				TankSkin[0]=tankSprite[0];
				TankSkin[1]=tankSprite[1];
				TankSkin[2]=tankSprite[2];
				TankSkin[3]=tankSprite[3];
				ChangeNow(4,5,6,7,0,1,2,3);
			break;
			default:
			break;
		}
	}
	private void ChangeNow(int a,int b,int c,int d,int e,int f,int g,int h){
		if(sr.sprite == tankSprite[a]){
			sr.sprite = tankSprite[e];
		}else if(sr.sprite == tankSprite[b]){
			sr.sprite = tankSprite[f];
		}else if(sr.sprite == tankSprite[c]){
			sr.sprite = tankSprite[g];
		}else if(sr.sprite == tankSprite[d]){
			sr.sprite = tankSprite[h];
		}
	}
}
