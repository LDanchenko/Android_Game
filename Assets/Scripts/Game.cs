using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Game : MonoBehaviour {

private int bonus = 1;
private int score;
public Text scoreText;
private int workersCount,workersBonus = 1;
[Header("Магазин")] //в инспекторе будет отображатся под надписью "Магазин"

public GameObject shopPanel;
public Button[] shopButtons;
public Text[] ShopBttnText;
public int[] shopCosts;
public float[] BonusTime;
public int[] shopBonus;

	public void Start()
	{	//при запуске игры
			StartCoroutine(BonusPerSec()); //метод для запуска нумератора (корутины) - для покупки рабочего

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

	public void StartBonusTimer(int index){ //стартуем карутину для бонуса
		
		int costs = 2 * workersCount; 
		ShopBttnText[2].text = "КУПИТЬ ПИВО\n" + costs + "$";
		if (score >= costs) {
		StartCoroutine(bonusTimer(BonusTime[index], index));
		}
		score -= costs;
	}

	IEnumerator BonusPerSec() //чтобы купить одного рабочего и получать +1 доллар каждую секунду
		{
			while  (true)
			{
				score += (workersCount * workersBonus);
				yield return new WaitForSeconds(1);	
			}
			
		}

		IEnumerator bonusTimer (float time, int index) //бонус пиво - индекс кнопки
		{
				shopButtons[index].interactable = false; //кнопка будет неактивна пока бонус не закончится 
				if (index == 0 && workersCount > 0){ //если кнопка 0 и рабочие есть метод такой то
					//пять секунд рабочий дает по два бакса
					workersBonus *= 2; // добавили бонус 
					yield return new WaitForSeconds(time); //задержка - сколько длится код;
					workersBonus /= 2; //востановился бонус

				}
					shopButtons[index].interactable = true;//включилась кнопка 


		}

}
