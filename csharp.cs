using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDealerGame : MonoBehaviour
{
    [System.Serializable]
    public class Car
    {
        public string carName;
        public float buyPrice;
        public float sellPrice;
    }

    public InputField carNameInput;
    public InputField buyPriceInput;
    public InputField sellPriceInput;
    public Button addCarButton;
    public Button sellCarButton;
    public Dropdown carDropdown;
    public Text moneyText;

    private float playerMoney = 10000f;
    private List<Car> carInventory = new List<Car>();

    void Start()
    {
        addCarButton.onClick.AddListener(AddCar);
        sellCarButton.onClick.AddListener(SellCar);
        UpdateUI();
    }

    void AddCar()
    {
        string name = carNameInput.text;
        if (!float.TryParse(buyPriceInput.text, out float buyPrice) ||
            !float.TryParse(sellPriceInput.text, out float sellPrice) ||
            name == "") {
            Debug.Log("Invalid input.");
            return;
        }

        if (playerMoney < buyPrice)
        {
            Debug.Log("Not enough money!");
            return;
        }

        Car newCar = new Car { carName = name, buyPrice = buyPrice, sellPrice = sellPrice };
        carInventory.Add(newCar);
        playerMoney -= buyPrice;
        UpdateUI();
    }

    void SellCar()
    {
        int index = carDropdown.value;
        if (index >= 0 && index < carInventory.Count)
        {
            playerMoney += carInventory[index].sellPrice;
            carInventory.RemoveAt(index);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        moneyText.text = "Money: $" + playerMoney.ToString("F2");

        carDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var car in carInventory)
        {
            options.Add($"{car.carName} - Sell ${car.sellPrice}");
        }
        carDropdown.AddOptions(options);
    }
}
