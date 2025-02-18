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

    [Header("UI Elements")]
    public TMP_Text pointsText;

    private int totalPoints = 0;

    [Header("Player Inventory Reference")]
    public TileInteractable playerInventory;

    private bool isPlayerInRange = false;
    private string interactionMessage = "Press 'E' to sell your fully grown plants!";

    void Start()
    {
        UpdatePointsUI();
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

        UpdatePointsUI();
        playerInventory.UpdateSeedCountUI(); // Update inventory UI

        Debug.Log("Plants sold!"); // Simplified log message
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

    void OnGUI()
    {
        if (isPlayerInRange)
        {
            GUI.Label(new Rect(10, 10, 300, 40), interactionMessage);
        }
    }
}