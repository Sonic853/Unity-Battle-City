using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {
	//0.Home 1.Wall 2.Barriar 3.Wather 4.Grass 5.AirBarriar 6.Born
	public GameObject[] itemList;
	private List<Vector3> itemPosiList = new List<Vector3>();
	private int[] EnemyWhere = new int[] {-6,0,6};
	private bool Created=true;
	private float CreatetimeVal;

	private void Awake(){
		InitMap();
	}

	private void InitMap(){
		CreateItem(itemList[0],new Vector3(0,-6,0),Quaternion.identity);
		CreateItem(itemList[1],new Vector3(-1,-6,0),Quaternion.identity);
		CreateItem(itemList[1],new Vector3(1,-6,0),Quaternion.identity);
		for(int i=-1;i<2;i++){
			CreateItem(itemList[1],new Vector3(i,-5,0),Quaternion.identity);
		}
		for(int i=-7;i<8;i++){
			CreateItem(itemList[5],new Vector3(i,7,0),Quaternion.identity);
			CreateItem(itemList[5],new Vector3(i,-7,0),Quaternion.identity);
		}
		for(int i=-6;i<7;i++){
			CreateItem(itemList[5],new Vector3(7,i,0),Quaternion.identity);
			CreateItem(itemList[5],new Vector3(-7,i,0),Quaternion.identity);
		}
		for(int i=0;i<60;i++){
			CreateItem(itemList[Random.Range(1,5)],CreateRandomPosi(),Quaternion.identity);
		}
		GameObject ThePlayer=Instantiate(itemList[6],new Vector3(-2,-6,0),Quaternion.identity);
		ThePlayer.GetComponent<Born>().isPlayer = true;
	}

	private void Update(){
		if((GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Born").Length)<4){
			if(CreatetimeVal<=0){
				Created = false;
			}
			if(!Created){
				Created = true;
				Invoke("CreateEnemy",0.8f);
				CreatetimeVal = 0.9f;
			}
		}else if((GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Born").Length)>=4){
			Created = false;
		}
		if(CreatetimeVal>0)CreatetimeVal -= Time.deltaTime;
	}

	private void CreateItem(GameObject createCOBJ,Vector3 createPosi,Quaternion createRota){
		GameObject itemGo=Instantiate(createCOBJ,createPosi,createRota);
		itemGo.transform.SetParent(gameObject.transform);
		itemPosiList.Add(createPosi);
	}

	private Vector3 CreateRandomPosi(){
		while(true){
			Vector3 createPosi=new Vector3(Random.Range(-6,7),Random.Range(-5,6),0);
			if(!HasThePosi(createPosi))return createPosi;
		}
	}

	private bool HasThePosi(Vector3 createPosi){
		for(int i=0;i<itemPosiList.Count;i++){
			if(createPosi==itemPosiList[i])return true;
		}
		return false;
	}

	private void CreateEnemy(){
		CreateItem(itemList[6],new Vector3(EnemyWhere[Random.Range(0,EnemyWhere.Length)],6,0),Quaternion.identity);
	}
}
