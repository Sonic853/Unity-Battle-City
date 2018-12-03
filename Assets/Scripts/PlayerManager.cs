using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	// 属性
	public int lifeValue = 2;
	public int playerScore = 0;
	public bool isDead = false;
	public bool isDefeat = false;
	private bool GoMenu = false;
	// 引用
	public GameObject born;
	public Text playerScoreText;
	public Text playerLifeValueText;
	public GameObject GameOverUI;

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
		if(isDefeat)return;
		if(isDead){
			isDead = false;
			Invoke("Recover",0.6f);
		}
		playerScoreText.text = playerScore.ToString();
		playerLifeValueText.text = lifeValue.ToString();
	}

	void FixedUpdate(){
		if(isDefeat){
			GameOverUI.SetActive(true);
			if(GameOverUI.transform.position.y<0){
				GameOverUI.transform.Translate(Vector3.up*3*Time.fixedDeltaTime,Space.World);
			}else if(!GoMenu){
				Invoke("BacktoMenu",2.5f);
				GoMenu=true;
			}else{
				return;
			}
		}
	}

	private void BacktoMenu(){
		SceneManager.LoadScene(0);
	}

	private void Recover(){
		int hp = lifeValue;
		if(--hp<0){
			// game fail
			isDefeat = true;
		}else{
			lifeValue--;
			GameObject go=Instantiate(born,new Vector3(-2,-6,0),Quaternion.identity);
			go.GetComponent<Born>().isPlayer = true;
		}
	}
}
