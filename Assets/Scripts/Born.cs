using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Born : MonoBehaviour {
	// 属性
	public bool isPlayer;
	private int[] enemyList = new int[] {0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,2};
	// 引用
	public GameObject PlayerPrefab;
	public GameObject[] enemyPrefabList;

	// Use this for initialization
	void Start () {
		Invoke("BornTank",0.8f);
		Destroy(gameObject,0.8f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void BornTank(){
		if(isPlayer){
			Instantiate(PlayerPrefab,transform.position,Quaternion.identity);
		}else{
			enemyList = enemyList.OrderBy(c => System.Guid.NewGuid()).ToArray<int>();
			int num = Random.Range(0, enemyList.Length);
			Instantiate(enemyPrefabList[enemyList[num]],transform.position,Quaternion.identity);
		}
	}
}
