using UnityEngine;
using TMPro;

public class SellPoint : MonoBehaviour
{
    [Header("Selling Settings")]
    public int blueberrySellValue = 1;
    public int lemonSellValue = 10;
    public int appleSellValue = 12;
    public int bananaSellValue = 14;
    public int grapeSellValue = 16;
    public int durianSellValue = 18;
    public int orangeSellValue = 20;
    public int kiwiSellValue = 22;
    public int starfruitSellValue = 24;
    public int pearSellValue = 26;
    public int goldSellValue = 30;

    [Header("Hunger Reduction Settings")]
    public int blueberryHungerValue = 1; 
    public int lemonHungerValue = 2;    
    public int appleHungerValue = 3;    
    public int bananaHungerValue = 4;   
    public int grapeHungerValue = 5;    
    public int durianHungerValue = 6;   
    public int orangeHungerValue = 7;   
    public int kiwiHungerValue = 8;     
    public int starfruitHungerValue = 9; 
    public int pearHungerValue = 10;    
    public int goldHungerValue = 15;

    [Header("UI Elements")]
    public TMP_Text pointsText;
    public TMP_Text hungerText;
    [SerializeField] private MoneyMgr _moneyMgr;            // Money Manager
    [SerializeField] private HungerSystem _hungerSystem;    // Hunger System

    public int totalPoints = 0;
    public int totalHunger = 100; 

    [Header("Player Inventory Reference")]
    public TileInteractable playerInventory;

    private bool isPlayerInRange = false;
    private string interactionMessage = "Press 'E' to sell your fully grown plants and reduce hunger!";

    void Start()
    {
        UpdatePointsUI();
        UpdateHungerUI();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SellFullyGrownPlants();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void SellFullyGrownPlants()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory is not assigned.");
            return;
        }

        // Sell logic for all plant types:
        totalPoints += playerInventory.blueberryFullyGrownCount * blueberrySellValue;
        totalPoints += playerInventory.lemonFullyGrownCount * lemonSellValue;
        totalPoints += playerInventory.appleFullyGrownCount * appleSellValue;
        totalPoints += playerInventory.bananaFullyGrownCount * bananaSellValue;
        totalPoints += playerInventory.grapeFullyGrownCount * grapeSellValue;
        totalPoints += playerInventory.durianFullyGrownCount * durianSellValue;
        totalPoints += playerInventory.orangeFullyGrownCount * orangeSellValue;
        totalPoints += playerInventory.kiwiFullyGrownCount * kiwiSellValue;
        totalPoints += playerInventory.starfruitFullyGrownCount * starfruitSellValue;
        totalPoints += playerInventory.pearFullyGrownCount * pearSellValue;
        totalPoints += playerInventory.goldFullyGrownCount * goldSellValue;

        // Hunger reduction logic for all plant types:
        totalHunger -= playerInventory.blueberryFullyGrownCount * blueberryHungerValue;
        totalHunger -= playerInventory.lemonFullyGrownCount * lemonHungerValue;
        totalHunger -= playerInventory.appleFullyGrownCount * appleHungerValue;
        totalHunger -= playerInventory.bananaFullyGrownCount * bananaHungerValue;
        totalHunger -= playerInventory.grapeFullyGrownCount * grapeHungerValue;
        totalHunger -= playerInventory.durianFullyGrownCount * durianHungerValue;
        totalHunger -= playerInventory.orangeFullyGrownCount * orangeHungerValue;
        totalHunger -= playerInventory.kiwiFullyGrownCount * kiwiHungerValue;
        totalHunger -= playerInventory.starfruitFullyGrownCount * starfruitHungerValue;
        totalHunger -= playerInventory.pearFullyGrownCount * pearHungerValue;
        totalHunger -= playerInventory.goldFullyGrownCount * goldHungerValue;

        // Ensure hunger doesn't go below 0
        totalHunger = Mathf.Max(totalHunger, 0);

        _hungerSystem.Hunger = totalHunger;

        // Reset fully grown counts in the player's inventory:
        playerInventory.blueberryFullyGrownCount = 0;
        playerInventory.lemonFullyGrownCount = 0;
        playerInventory.appleFullyGrownCount = 0;
        playerInventory.bananaFullyGrownCount = 0;
        playerInventory.grapeFullyGrownCount = 0;
        playerInventory.durianFullyGrownCount = 0;
        playerInventory.orangeFullyGrownCount = 0;
        playerInventory.kiwiFullyGrownCount = 0;
        playerInventory.starfruitFullyGrownCount = 0;
        playerInventory.pearFullyGrownCount = 0;
        playerInventory.goldFullyGrownCount = 0;

        // Money Mgr
        _moneyMgr.MinusMoney(playerInventory.blueberryFullyGrownCount * blueberrySellValue);
        _moneyMgr.MinusMoney(playerInventory.lemonFullyGrownCount * lemonSellValue);
        _moneyMgr.MinusMoney(playerInventory.appleFullyGrownCount * appleSellValue);
        _moneyMgr.MinusMoney(playerInventory.bananaFullyGrownCount * bananaSellValue);
        _moneyMgr.MinusMoney(playerInventory.grapeFullyGrownCount * grapeSellValue);
        _moneyMgr.MinusMoney(playerInventory.durianFullyGrownCount * durianSellValue);
        _moneyMgr.MinusMoney(playerInventory.orangeFullyGrownCount * orangeSellValue);
        _moneyMgr.MinusMoney(playerInventory.kiwiFullyGrownCount * kiwiSellValue);
        _moneyMgr.MinusMoney(playerInventory.starfruitFullyGrownCount * starfruitSellValue);
        _moneyMgr.MinusMoney(playerInventory.pearFullyGrownCount * pearSellValue);
        _moneyMgr.MinusMoney(playerInventory.goldFullyGrownCount * goldSellValue);

        UpdatePointsUI();
        UpdateHungerUI();
        playerInventory.UpdateSeedCountUI(); // Update inventory UI

        Debug.Log("Plants sold and hunger reduced!"); // Simplified log message
    }

    private void UpdatePointsUI()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + totalPoints;
        }
        else
        {
            Debug.LogError("PointsText UI is not assigned.");
        }
    }

    private void UpdateHungerUI()
    {
        if (hungerText != null)
        {
            hungerText.text = "Hunger: " + totalHunger;
        }
        else
        {
            Debug.LogError("HungerText UI is not assigned.");
        }
    }

    void OnGUI()
    {
        if (isPlayerInRange)
        {
            GUI.Label(new Rect(10, 10, 300, 40), interactionMessage);
        }
    }
}