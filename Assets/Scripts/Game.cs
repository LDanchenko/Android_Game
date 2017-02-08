using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour {

private int bonus = 1;
private int score;
public Text scoreText;
public GameObject shopPanel;

	public void showPan_ShowAndHide(){
	
	shopPanel.SetActive(!shopPanel.activeSelf);
		
	}

	public void shopButtonAddBonus(int bonus_plus){

		if (score >=20) {
			bonus += bonus_plus;
	}

	else {
		Debug.Log("No money!");
	}
	}

	public void OnClick (){

	score += bonus;
 	scoreText.text = "SCORE = " + score;

	}

}
