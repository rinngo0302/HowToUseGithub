using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Seed Prices")]
    public int blueberrySeedPrice = 5;
    public int lemonSeedPrice = 10;
    public int appleSeedPrice = 15;
    public int bananaSeedPrice = 20;
    public int grapeSeedPrice = 25;
    public int durianSeedPrice = 30;
    public int orangeSeedPrice = 35;
    public int kiwiSeedPrice = 40;
    public int starfruitSeedPrice = 45;
    public int pearSeedPrice = 50;
    public int goldSeedPrice = 100;

    [Header("UI Elements")]
    public TMP_Text pointsText;
    public TMP_Text hungerText;
    public TMP_Text blueberrySeedPriceText;
    public TMP_Text lemonSeedPriceText;
    public TMP_Text appleSeedPriceText;
    public TMP_Text bananaSeedPriceText;
    public TMP_Text grapeSeedPriceText;
    public TMP_Text durianSeedPriceText;
    public TMP_Text orangeSeedPriceText;
    public TMP_Text kiwiSeedPriceText;
    public TMP_Text starfruitSeedPriceText;
    public TMP_Text pearSeedPriceText;
    public TMP_Text goldSeedPriceText;

    [Header("Shop UI")]
    public GameObject shopPanel; // Reference to the shop UI panel
    public Button buyBlueberryButton;
    public Button buyLemonButton;
    public Button buyAppleButton;
    public Button buyBananaButton;
    public Button buyGrapeButton;
    public Button buyDurianButton;
    public Button buyOrangeButton;
    public Button buyKiwiButton;
    public Button buyStarfruitButton;
    public Button buyPearButton;
    public Button buyGoldButton;
    public Button sellAllButton;
    public Button closeButton; // Close button for the shop UI

    [Header("Interaction Settings")]
    public Vector3 interactionBoxSize = new Vector3(2, 2, 2);
    public Image hungerBarFill;
    [SerializeField] private HungerSystem _hungerSystem;    // Hunger System

    [Header("Player Inventory Reference")]
    public TileInteractable playerInventory;

    public int totalPoints = 0; // Points act as money
    public int totalHunger = 100;

    private bool isPlayerInRange = false;
    private string interactionMessage = "Press 'E' to interact";

    void Start()
    {
        // Hide the shop UI when the game starts
        shopPanel.SetActive(false);

        UpdatePointsUI();
        UpdateHungerUI();
        UpdateSeedPricesUI();

        _hungerSystem.Hunger = totalHunger;

        // Initialize button listeners
        InitializeButtonListeners();
    }

    void Update()
    {
        CheckPlayerInRange();

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShopUI();
        }
    }

    void CheckPlayerInRange()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, interactionBoxSize / 2);
        isPlayerInRange = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                isPlayerInRange = true;
                break;
            }
        }
    }

    public void ToggleShopUI()
    {
        shopPanel.SetActive(!shopPanel.activeSelf); // Toggle shop UI visibility

        // Enable/disable mouse cursor and lock state based on UI visibility
        if (shopPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            Cursor.visible = true; // Show cursor
            UpdateSeedPricesUI(); // Update UI when shop is opened
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor
            Cursor.visible = false; // Hide cursor
        }
    }

    private void InitializeButtonListeners()
    {
        // Assign button click listeners for buying seeds
        buyBlueberryButton.onClick.AddListener(() => BuySeed("Blueberry"));
        buyLemonButton.onClick.AddListener(() => BuySeed("Lemon"));
        buyAppleButton.onClick.AddListener(() => BuySeed("Apple"));
        buyBananaButton.onClick.AddListener(() => BuySeed("Banana"));
        buyGrapeButton.onClick.AddListener(() => BuySeed("Grape"));
        buyDurianButton.onClick.AddListener(() => BuySeed("Durian"));
        buyOrangeButton.onClick.AddListener(() => BuySeed("Orange"));
        buyKiwiButton.onClick.AddListener(() => BuySeed("Kiwi"));
        buyStarfruitButton.onClick.AddListener(() => BuySeed("Starfruit"));
        buyPearButton.onClick.AddListener(() => BuySeed("Pear"));
        buyGoldButton.onClick.AddListener(() => BuySeed("Gold"));

        // Assign button click listener for selling all plants
        sellAllButton.onClick.AddListener(SellFullyGrownPlants);

        // Assign button click listener for closing the shop UI
        closeButton.onClick.AddListener(CloseShopUI);
    }

    public void BuySeed(string seedType)
    {
        int price = 0;
        switch (seedType)
        {
            case "Blueberry":
                price = blueberrySeedPrice;
                break;
            case "Lemon":
                price = lemonSeedPrice;
                break;
            case "Apple":
                price = appleSeedPrice;
                break;
            case "Banana":
                price = bananaSeedPrice;
                break;
            case "Grape":
                price = grapeSeedPrice;
                break;
            case "Durian":
                price = durianSeedPrice;
                break;
            case "Orange":
                price = orangeSeedPrice;
                break;
            case "Kiwi":
                price = kiwiSeedPrice;
                break;
            case "Starfruit":
                price = starfruitSeedPrice;
                break;
            case "Pear":
                price = pearSeedPrice;
                break;
            case "Gold":
                price = goldSeedPrice;
                break;
            default:
                Debug.LogError("Invalid seed type.");
                return;
        }

        if (totalPoints >= price)
        {
            totalPoints -= price; // Deduct points (money)
            UpdateSeedCount(seedType, 1);
            UpdatePointsUI();
            Debug.Log($"Purchased 1 {seedType} seed for {price} points.");
        }
        else
        {
            Debug.Log("Not enough points to buy this seed.");
        }
    }

    private void UpdateSeedCount(string seedType, int amount)
    {
        switch (seedType)
        {
            case "Blueberry":
                playerInventory.blueberrySeedCount += amount;
                Debug.Log($"Blueberry Seed Count: {playerInventory.blueberrySeedCount}");
                break;
            case "Lemon":
                playerInventory.lemonSeedCount += amount;
                Debug.Log($"Lemon Seed Count: {playerInventory.lemonSeedCount}");
                break;
            case "Apple":
                playerInventory.appleSeedCount += amount;
                Debug.Log($"Apple Seed Count: {playerInventory.appleSeedCount}");
                break;
            case "Banana":
                playerInventory.bananaSeedCount += amount;
                Debug.Log($"Banana Seed Count: {playerInventory.bananaSeedCount}");
                break;
            case "Grape":
                playerInventory.grapeSeedCount += amount;
                Debug.Log($"Grape Seed Count: {playerInventory.grapeSeedCount}");
                break;
            case "Durian":
                playerInventory.durianSeedCount += amount;
                Debug.Log($"Durian Seed Count: {playerInventory.durianSeedCount}");
                break;
            case "Orange":
                playerInventory.orangeSeedCount += amount;
                Debug.Log($"Orange Seed Count: {playerInventory.orangeSeedCount}");
                break;
            case "Kiwi":
                playerInventory.kiwiSeedCount += amount;
                Debug.Log($"Kiwi Seed Count: {playerInventory.kiwiSeedCount}");
                break;
            case "Starfruit":
                playerInventory.starfruitSeedCount += amount;
                Debug.Log($"Starfruit Seed Count: {playerInventory.starfruitSeedCount}");
                break;
            case "Pear":
                playerInventory.pearSeedCount += amount;
                Debug.Log($"Pear Seed Count: {playerInventory.pearSeedCount}");
                break;
            case "Gold":
                playerInventory.goldSeedCount += amount;
                Debug.Log($"Gold Seed Count: {playerInventory.goldSeedCount}");
                break;
            default:
                Debug.LogError("Invalid seed type.");
                break;
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
        UpdateHungerUI();
        playerInventory.UpdateSeedCountUI(); // Update inventory UI

        Debug.Log("Plants sold and hunger reduced!");
    }

    private void UpdatePointsUI()
    {
        if (pointsText != null)
        {
            pointsText.text = $"${totalPoints}"; // Replace "Points" with "$"
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

        // Update the hunger bar fill amount
        if (hungerBarFill != null)
        {
            hungerBarFill.fillAmount = (float)totalHunger / 100f; // Normalize hunger to a value between 0 and 1
        }
        else
        {
            Debug.LogError("HungerBarFill UI is not assigned.");
        }
    }

    private void UpdateSeedPricesUI()
    {
        if (blueberrySeedPriceText != null)
            blueberrySeedPriceText.text = $"Blueberry Seed: {blueberrySeedPrice} $";
        if (lemonSeedPriceText != null)
            lemonSeedPriceText.text = $"Lemon Seed: {lemonSeedPrice} $";
        if (appleSeedPriceText != null)
            appleSeedPriceText.text = $"Apple Seed: {appleSeedPrice} $";
        if (bananaSeedPriceText != null)
            bananaSeedPriceText.text = $"Banana Seed: {bananaSeedPrice} $";
        if (grapeSeedPriceText != null)
            grapeSeedPriceText.text = $"Grape Seed: {grapeSeedPrice} $";
        if (durianSeedPriceText != null)
            durianSeedPriceText.text = $"Durian Seed: {durianSeedPrice} $";
        if (orangeSeedPriceText != null)
            orangeSeedPriceText.text = $"Orange Seed: {orangeSeedPrice} $";
        if (kiwiSeedPriceText != null)
            kiwiSeedPriceText.text = $"Kiwi Seed: {kiwiSeedPrice} $";
        if (starfruitSeedPriceText != null)
            starfruitSeedPriceText.text = $"Starfruit Seed: {starfruitSeedPrice} $";
        if (pearSeedPriceText != null)
            pearSeedPriceText.text = $"Pear Seed: {pearSeedPrice} $";
        if (goldSeedPriceText != null)
            goldSeedPriceText.text = $"Gold Seed: {goldSeedPrice} $";
    }

    private void CloseShopUI()
    {
        shopPanel.SetActive(false); // Hide the shop UI
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
    }

    void OnGUI()
    {
        if (isPlayerInRange)
        {
            GUI.Label(new Rect(10, 10, 300, 40), interactionMessage);
        }
    }
}