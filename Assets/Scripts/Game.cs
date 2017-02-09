using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Game : MonoBehaviour {

private int bonus = 1;
private int score;
public Text scoreText;
private int workersCount;
[Header("Магазин")] //в инспекторе будет отображатся под надписью "Магазин"

public GameObject shopPanel;
public Text[] ShopBttnText;
public int[] shopCosts;
public int[] shopBonus;

	public void Start()
	{	//при запуске игры
				StartCoroutine(BonusPerSec()); //метод для запуска нумератора (корутины)

	}

	public void showPan_ShowAndHide(){
	
	shopPanel.SetActive(!shopPanel.activeSelf);
		
	}

	public void shopButtonAddBonus(int index){

		if (score >= shopCosts[index]) {
			bonus += shopBonus[index];
			score -= shopCosts[index];
			shopCosts[index] *= 2;
			ShopBttnText[index].text = "КУПИТЬ УЛУЧШЕНИЕ\n" + shopCosts[index] + "$";
	}

	else {
		Debug.Log("No money!");
	}
	}

	public void OnClick (){

	score += bonus;

	}

	private void Update(){ //выполняется каждый кадр
		scoreText.text = "SCORE = " + score + "$";
	}

	
	public void HireWorker (int index)
	{
		if (score >= shopCosts[index])
		{
			workersCount ++;
			score -= shopCosts[index];
		}
	}

	IEnumerator BonusPerSec() //чтобы купить одного рабочего и получать +1 доллар каждую секунду
		{
			while  (true)
			{
				score += workersCount * 1;
				yield return new WaitForSeconds(1);	
			}
			
		}

}
