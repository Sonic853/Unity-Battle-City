using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	private int choice=0;
	// 引用
	public GameObject Select;
	public Transform[] MenuList;
	// Use this for initialization
	void Start(){
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.W)){
			choice--;
			if(choice<0){
				choice=MenuList.Length-1;
				Select.transform.position=MenuList[choice].position;
			}else{
				Select.transform.position=MenuList[choice].position;
			}
		}else if(Input.GetKeyDown(KeyCode.S)){
			choice++;
			if(choice>=MenuList.Length){
				choice=0;
				Select.transform.position=MenuList[choice].position;
			}else{
				Select.transform.position=MenuList[choice].position;
			}
		}else if(Input.GetKeyDown(KeyCode.Space)){
			if(choice==0){
				SceneManager.LoadScene(1);
			}
		}
	}
}
