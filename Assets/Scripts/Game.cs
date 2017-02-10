using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Syste.Collection.Generic;


public class Game : MonoBehaviour {

[Header("Текст, отвечающий за отображение денег")] //надпись в инспекторе
public Text scoreText; 
[Header("Магазин")]  //кнопки в магазине - сделали как лист товаров
public List<Item> shopItems = new List<Item>(); // Сделали отдельным классов
[Header("Текст на кнопках товаров")]
public Text[] shopItemsText; // Текст на кнопках
[Header("Кнопки товаров")]
public Button[] shopBttns; //Массив кнопок
[Header("Панелька магазина")]
public GameObject shopPanel; //Панель магазина

private int score;
private int scoreIncrease;

	public void Start()
	{	//при запуске игры
			updateCosts(); //Обновили текст с ценами при запуске игры
			StartCoroutine(BonusPerSec()); //метод для запуска нумератора (корутины) - для покупки рабочего

	}

	private void Update()
	{ //выполняется каждый кадр
		scoreText.text = "SCORE = " + score + "$"; // вывод денег (очков)
	}


	public void BuyButton (int index) //метод  при нажатии на кнопку покупки товара
	{
		int cost = shopItems[index].cost * shopItems[shopItems[index].itemIndex].bonusCounter; //Расчитали цену в зависимости от колличетсва купленых рабочих
		if (ShopItems[index].itsBonus && score >= costs) //если товар нажатой кнопки это бонус и хватает денег
		{
			if (cost > 0) //цена больше ноля
			{
				score -= cost; //вычитаем цену из денег
				StartCoroutine(BonusTimer(shopItems[index].timeofBonus, index)); //запускаем бонусный таймер
			}
			else print ("Нечего улучшать то!"); //Выводим текст на консоль
		}
		else if (score >= shopItems[index].cost) //если товар не бонус, и денег хватает
		{
			if (shopItems[index].itsItemPerSec) { shopItems[index].bonusCounter++; } //если нанимаем рабочего то прибавили колличество рабочих
			else scoreIncrease += shopItems[index].bonusIncrease; //если бонус то при клике добавляем бонус товара
			score -= ShopItems[index].cost; //вычитаем цену из денег
			if (shopItems[index].needCostMultiplier) shopItem[index].cost*=shopItems[items].costMultiplier; //если товару нужно умножить цену, то умножаем на множитель
			shopItems[index].levelOfItem++; //однимаем уровень предмета до 1;
		}
		else print ("Не хватает денег!"); 
		updateCosts(); //Обновили текст с ценами
	}


	public void updateCosts() //метод для обновления текста с ценами
	{
		for (int i=0; i< shopItems.Count; i++)
		{

		}
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
