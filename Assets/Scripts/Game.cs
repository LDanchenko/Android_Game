using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class Game : MonoBehaviour {

[Header("Текст, отвечающий за отображение денег")] //надпись в инспекторе
public Text scoreText; 
[Header("Магазин")]  //кнопки в магазине - сделали как лист товаров
public List<Item> shopItems = new List<Item>(); // Сделали отдельным классов
[Header("Текст на кнопках товаров")]
public Text[] shopItemsText; // Текст на кнопках
[Header("Кнопки товаров")]
public Button[] shopButtons; //Массив кнопок
[Header("Панелька магазина")]
public GameObject shopPanel; //Панель магазина

private int score;
private int scoreIncrease = 1;

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
		if (shopItems[index].itsBonus && score >= cost) //если товар нажатой кнопки это бонус и хватает денег
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
			score -= shopItems[index].cost; //вычитаем цену из денег
			if (shopItems[index].needCostMultiplier) shopItems[index].cost *= shopItems[index].costMultiplier; //если товару нужно умножить цену, то умножаем на множитель
			shopItems[index].levelOfItem++; //однимаем уровень предмета до 1;
		}
		else print ("Не хватает денег!"); 
		updateCosts(); //Обновили текст с ценами
	}


	public void updateCosts() //метод для обновления текста с ценами
	{
		for (int i=0; i< shopItems.Count; i++) //Цикл выполняется, пока переменная меньше кол-ва товаров
		{
			if (shopItems[i].itsBonus) //если товар  - бонус
			{
				int cost = shopItems[i].cost*shopItems[shopItems[i].itemIndex].bonusCounter; //расчитывваем цену в зависимости от колличества рабочих
				shopItemsText[i].text = shopItems[i].name + "\n" + cost + "$"; //обновляем текст кнопки с расчитаной ценой
			}
			else shopItemsText[i].text = shopItems[i].name + "\n" + shopItems[i].cost + "$"; //текст кнопки с обычной ценой
		}
	}


	IEnumerator BonusPerSec() //чтобы купить одного рабочего и получать +1 доллар каждую секунду
		{
			while  (true) //бесконечный цикл
			{
				for (int i = 0; i < shopItems.Count; i++) {
				score += (shopItems[i].bonusCounter * shopItems[i].bonusPerSec); //добавляем к игровой валюте бонус рабочих (например)
				yield return new WaitForSeconds(1);	
				}
			}
			
		}

		IEnumerator BonusTimer (float time, int index) //бонсный таймер
		{
				shopButtons[index].interactable = false; //кнопка будет неактивна пока бонус не закончится 
				shopItems[shopItems[index].itemIndex].bonusPerSec *=2; //удваиваем бонус рабочих в секунду
				yield return new WaitForSeconds(time); //задержка - сколько длится код;
				shopItems[shopItems[index].itemIndex].bonusPerSec *=2; //возвращаем бонус в нормальное состояние
				shopButtons[index].interactable = true;//включилась кнопка 
		}

		public void OnClick (){

		score += scoreIncrease;

		}

		public void showPan_ShowAndHide(){
	
			shopPanel.SetActive(!shopPanel.activeSelf);
		
		}

		[Serializable]
		public class Item //класс товара
		{
			[Tooltip("Название используется на кнопках")]
			public String name;
			[Tooltip("Цена товара")]
			public int cost;
			[Tooltip("Бонус, который добавляется к бонусу при клике")]
			public int bonusIncrease;
			[HideInInspector]
			public int levelOfItem;
			[Space]
			[Tooltip("Нужен ли множитель для цены")]
			public bool needCostMultiplier;
			[Tooltip("Множитель для цены")]
			public int costMultiplier;
			[Space]
			[Tooltip("Этот товар дает бонус в секунду?")]
			public bool itsItemPerSec;
			[Tooltip("Бонус, который дается в секунду")]
			public int bonusPerSec;
			[HideInInspector]
			public int bonusCounter;
			[Space]
			[Tooltip("Это временный бонус?")]
			public bool itsBonus;
			[Tooltip("Множитель товара, который управляется бонусом (Умножается переменная bonusPerSec)")]
			public int itemMultiplier;
			[Tooltip("Индекс товара, который будет управляться бонусом (Умножается переменная bonusPerSec этого товара)")]
			public int itemIndex;
			[Tooltip("Длительность бонуса")]
			public float timeofBonus;

		}


}

		/*
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

}
*/


