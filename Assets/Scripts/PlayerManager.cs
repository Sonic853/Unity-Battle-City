using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	// 属性
	public int lifeValue = 3;
	public int playerScore = 0;
	public bool isDead = false;
	public bool isDefeat;
	// 引用
	public GameObject born;

	// 单例
	private static PlayerManager instance;

	public static PlayerManager Instance{
		get{
			return instance;
		}
		set{
			instance = value;
		}
	}

	private void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isDead){
			isDead=false;
			Invoke("Recover",0.6f);
		}
	}

	private void Recover(){
		if(lifeValue<=0){
			// game fail
		}else{
			lifeValue--;
			GameObject go=Instantiate(born,new Vector3(-2,-6,0),Quaternion.identity);
			go.GetComponent<Born>().isPlayer = true;
		}
	}
}
