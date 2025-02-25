using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TileInteractable : MonoBehaviour
{
    public float interactDistance = 3f; // Distance at which the player can interact with the tile
    private GameObject player;          // Reference to the player
    private bool isPlayerInRange = false;

    // UI References
    public GameObject seedSelectionUI;
    public Button blueberrySeedButton;
    public Button lemonSeedButton;
    public Button appleSeedButton;
    public Button bananaSeedButton;
    public Button grapeSeedButton;
    public Button durianSeedButton;
    public Button orangeSeedButton;
    public Button kiwiSeedButton;
    public Button starfruitSeedButton;
    public Button pearSeedButton;
    public Button goldSeedButton;
    public Button closeUIButton;

    public TMP_Text blueberrySeedCountText;
    public TMP_Text lemonSeedCountText;
    public TMP_Text appleSeedCountText;
    public TMP_Text bananaSeedCountText;
    public TMP_Text grapeSeedCountText;
    public TMP_Text durianSeedCountText;
    public TMP_Text orangeSeedCountText;
    public TMP_Text kiwiSeedCountText;
    public TMP_Text starfruitSeedCountText;
    public TMP_Text pearSeedCountText;
    public TMP_Text goldSeedCountText;

    // Inventory UI
    public GameObject inventoryPanel;       // Inventory panel UI
    public TMP_Text inventoryBlueberryCount;
    public TMP_Text inventoryLemonCount;
    public TMP_Text inventoryAppleCount;
    public TMP_Text inventoryBananaCount;
    public TMP_Text inventoryGrapeCount;
    public TMP_Text inventoryDurianCount;
    public TMP_Text inventoryOrangeCount;
    public TMP_Text inventoryKiwiCount;
    public TMP_Text inventoryStarfruitCount;
    public TMP_Text inventoryPearCount;
    public TMP_Text inventoryGoldCount;

    // Inventory
    public int blueberrySeedCount = 0;
    public int lemonSeedCount = 0;
    public int appleSeedCount = 0;
    public int bananaSeedCount = 0;
    public int grapeSeedCount = 0;
    public int durianSeedCount = 0;
    public int orangeSeedCount = 0;
    public int kiwiSeedCount = 0;
    public int starfruitSeedCount = 0;
    public int pearSeedCount = 0;
    public int goldSeedCount = 0;

    public int blueberryFullyGrownCount = 0;
    public int lemonFullyGrownCount = 0;
    public int appleFullyGrownCount = 0;
    public int bananaFullyGrownCount = 0;
    public int grapeFullyGrownCount = 0;
    public int durianFullyGrownCount = 0;
    public int orangeFullyGrownCount = 0;
    public int kiwiFullyGrownCount = 0;
    public int starfruitFullyGrownCount = 0;
    public int pearFullyGrownCount = 0;
    public int goldFullyGrownCount = 0;

    // Seed selection logic
    private string selectedSeed = "";
    private bool isPlanted = false;    // Track if the tile has been planted
    private bool isGrowing = false;   // Track if the plant is growing
    private bool isHarvestable = false; // Track if the plant is harvestable
    private float growthTimer = 0f;   // Timer to track growth progress

    // Adjustable Growth Settings in Inspector
    [Header("Plant Growth Settings")]
    public GameObject seedStage;      // The seed stage model
    public GameObject saplingStage;   // The sapling stage model
    public GameObject blueberryFullStage;
    public GameObject lemonFullStage;
    public GameObject appleFullStage;
    public GameObject bananaFullStage;
    public GameObject grapeFullStage;
    public GameObject durianFullStage;
    public GameObject orangeFullStage;
    public GameObject kiwiFullStage;
    public GameObject starfruitFullStage;
    public GameObject pearFullStage;
    public GameObject goldFullStage;

    private GameObject currentFullStage;  // Holds the current full-grown model for the selected seed

    [Header("UI Interaction Settings")]
    public string interactionMessage = "Press 'E' to plant seed"; // Default message when player is nearby
    public string plantedMessage = "Seed planted, starting to grow..."; // Message after planting
    public string growingMessage = "Growing..."; // Message while growing
    public string harvestMessage = "Press 'E' to harvest the plant"; // Message when the plant is ready for harvest

    // Define growth durations for different seeds
    [Header("Seed Growth Durations")]
    public float blueberryGrowthDuration = 5f;
    public float lemonGrowthDuration = 10f;
    public float appleGrowthDuration = 12f;
    public float bananaGrowthDuration = 14f;
    public float grapeGrowthDuration = 16f;
    public float durianGrowthDuration = 18f;
    public float orangeGrowthDuration = 20f;
    public float kiwiGrowthDuration = 22f;
    public float starfruitGrowthDuration = 24f;
    public float pearGrowthDuration = 26f;
    public float goldGrowthDuration = 30f;

    private float growthDuration;  // This will be set based on the selected seed

    private string currentMessage = ""; // Current message displayed to the player

    [SerializeField] private InputActionReference interactAction; // Use reference

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // Ensure the seed selection UI is hidden initially
        seedSelectionUI.SetActive(false);

        // Ensure the inventory panel is hidden initially
        inventoryPanel.SetActive(false);

        // Add button listeners
        blueberrySeedButton.onClick.AddListener(() => SelectSeed("Blueberry"));
        lemonSeedButton.onClick.AddListener(() => SelectSeed("Lemon"));
        appleSeedButton.onClick.AddListener(() => SelectSeed("Apple"));
        bananaSeedButton.onClick.AddListener(() => SelectSeed("Banana"));
        grapeSeedButton.onClick.AddListener(() => SelectSeed("Grape"));
        durianSeedButton.onClick.AddListener(() => SelectSeed("Durian"));
        orangeSeedButton.onClick.AddListener(() => SelectSeed("Orange"));
        kiwiSeedButton.onClick.AddListener(() => SelectSeed("Kiwi"));
        starfruitSeedButton.onClick.AddListener(() => SelectSeed("Starfruit"));
        pearSeedButton.onClick.AddListener(() => SelectSeed("Pear"));
        goldSeedButton.onClick.AddListener(() => SelectSeed("Gold"));
        closeUIButton.onClick.AddListener(CloseSeedSelection);

        // Ensure all models are inactive at the start, except the seed model
        if (seedStage != null) seedStage.SetActive(false);
        if (saplingStage != null) saplingStage.SetActive(false);
        if (blueberryFullStage != null) blueberryFullStage.SetActive(false);
        if (lemonFullStage != null) lemonFullStage.SetActive(false);
        if (appleFullStage != null) appleFullStage.SetActive(false);
        if (bananaFullStage != null) bananaFullStage.SetActive(false);
        if (grapeFullStage != null) grapeFullStage.SetActive(false);
        if (durianFullStage != null) durianFullStage.SetActive(false);
        if (orangeFullStage != null) orangeFullStage.SetActive(false);
        if (kiwiFullStage != null) kiwiFullStage.SetActive(false);
        if (starfruitFullStage != null) starfruitFullStage.SetActive(false);
        if (pearFullStage != null) pearFullStage.SetActive(false);
        if (goldFullStage != null) goldFullStage.SetActive(false);

        // Update UI seed counts
        UpdateSeedCountUI();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player object not found!");
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) < interactDistance)
        {
            isPlayerInRange = true;

            // Update the interaction message based on the tile state
            if (!isPlanted)
            {
                currentMessage = interactionMessage; // "Press 'E' to plant seed"
                if (interactAction.action.WasPerformedThisFrame() || Input.GetKey(KeyCode.E))
                {
                    OpenSeedSelectionUI();
                }
            }
            else if (isHarvestable)
            {
                currentMessage = harvestMessage; // "Press 'E' to harvest the plant"
                if (interactAction.action.WasPerformedThisFrame() || Input.GetKey(KeyCode.E))
                {
                    Harvest();
                }
            }
            else if (isGrowing)
            {
                currentMessage = growingMessage; // "Growing..."
            }
        }
        else
        {
            isPlayerInRange = false;
            currentMessage = ""; // Clear message when out of range
        }

        if (isGrowing)
        {
            growthTimer += Time.deltaTime;

            if (growthTimer >= growthDuration)
            {
                growthTimer = growthDuration;
                isGrowing = false;
                isHarvestable = true; // Mark the plant as ready for harvest
                Debug.Log(selectedSeed + " is ready for harvest!");
            }

            UpdateGrowthStage();
        }

        // Toggle inventory on "I" key press
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
            SE.Instance.PlayOneShot(3);
        }
    }

    void OpenSeedSelectionUI()
    {
        if (isPlayerInRange && !isPlanted)
        {
            seedSelectionUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable buttons if out of seeds
            blueberrySeedButton.interactable = blueberrySeedCount > 0;
            lemonSeedButton.interactable = lemonSeedCount > 0;
            appleSeedButton.interactable = appleSeedCount > 0;
            bananaSeedButton.interactable = bananaSeedCount > 0;
            grapeSeedButton.interactable = grapeSeedCount > 0;
            durianSeedButton.interactable = durianSeedCount > 0;
            orangeSeedButton.interactable = orangeSeedCount > 0;
            kiwiSeedButton.interactable = kiwiSeedCount > 0;
            starfruitSeedButton.interactable = starfruitSeedCount > 0;
            pearSeedButton.interactable = pearSeedCount > 0;
            goldSeedButton.interactable = goldSeedCount > 0;
        }
    }

    void SelectSeed(string seed)
    {
        if (seed == "Blueberry" && blueberrySeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = blueberryGrowthDuration;
            currentFullStage = blueberryFullStage;

            // Reduce seed count
            blueberrySeedCount--;
        }
        else if (seed == "Lemon" && lemonSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = lemonGrowthDuration;
            currentFullStage = lemonFullStage;

            // Reduce seed count
            lemonSeedCount--;
        }
        else if (seed == "Apple" && appleSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = appleGrowthDuration;
            currentFullStage = appleFullStage;

            // Reduce seed count
            appleSeedCount--;
        }
        else if (seed == "Banana" && bananaSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = bananaGrowthDuration;
            currentFullStage = bananaFullStage;

            // Reduce seed count
            bananaSeedCount--;
        }
        else if (seed == "Grape" && grapeSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = grapeGrowthDuration;
            currentFullStage = grapeFullStage;

            // Reduce seed count
            grapeSeedCount--;
        }
        else if (seed == "Durian" && durianSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = durianGrowthDuration;
            currentFullStage = durianFullStage;

            // Reduce seed count
            durianSeedCount--;
        }
        else if (seed == "Orange" && orangeSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = orangeGrowthDuration;
            currentFullStage = orangeFullStage;

            // Reduce seed count
            orangeSeedCount--;
        }
        else if (seed == "Kiwi" && kiwiSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = kiwiGrowthDuration;
            currentFullStage = kiwiFullStage;

            // Reduce seed count
            kiwiSeedCount--;
        }
        else if (seed == "Starfruit" && starfruitSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = starfruitGrowthDuration;
            currentFullStage = starfruitFullStage;

            // Reduce seed count
            starfruitSeedCount--;
        }
        else if (seed == "Pear" && pearSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = pearGrowthDuration;
            currentFullStage = pearFullStage;

            // Reduce seed count
            pearSeedCount--;
        }
        else if (seed == "Gold" && goldSeedCount > 0)
        {
            selectedSeed = seed;
            growthDuration = goldGrowthDuration;
            currentFullStage = goldFullStage;

            // Reduce seed count
            goldSeedCount--;
        }

        PlantSeed();
    }

    void PlantSeed()
    {
        if (!string.IsNullOrEmpty(selectedSeed))
        {
            isPlanted = true;
            isGrowing = true;
            growthTimer = 0f;

            Debug.Log("Planted " + selectedSeed + "!");

            CloseSeedSelection();

            if (seedStage != null)
                seedStage.SetActive(true);

            switch (selectedSeed)
            {
                case "Blueberry":
                    currentFullStage = Instantiate(blueberryFullStage, transform.position, Quaternion.identity);
                    break;
                case "Lemon":
                    currentFullStage = Instantiate(lemonFullStage, transform.position, Quaternion.identity);
                    break;
                case "Apple":
                    currentFullStage = Instantiate(appleFullStage, transform.position, Quaternion.identity);
                    break;
                case "Banana":
                    currentFullStage = Instantiate(bananaFullStage, transform.position, Quaternion.identity);
                    break;
                case "Grape":
                    currentFullStage = Instantiate(grapeFullStage, transform.position, Quaternion.identity);
                    break;
                case "Durian":
                    currentFullStage = Instantiate(durianFullStage, transform.position, Quaternion.identity);
                    break;
                case "Orange":
                    currentFullStage = Instantiate(orangeFullStage, transform.position, Quaternion.identity);
                    break;
                case "Kiwi":
                    currentFullStage = Instantiate(kiwiFullStage, transform.position, Quaternion.identity);
                    break;
                case "Starfruit":
                    currentFullStage = Instantiate(starfruitFullStage, transform.position, Quaternion.identity);
                    break;
                case "Pear":
                    currentFullStage = Instantiate(pearFullStage, transform.position, Quaternion.identity);
                    break;
                case "Gold":
                    currentFullStage = Instantiate(goldFullStage, transform.position, Quaternion.identity);
                    break;
                default:
                    Debug.LogError("Not exist fruit is selected");
                    break;
            }
            SE.Instance.PlayOneShot(1);

            // Update UI seed counts
            UpdateSeedCountUI();

        }
    }

    void CloseSeedSelection()
    {
        seedSelectionUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdateGrowthStage()
    {
        float growthPercentage = growthTimer / growthDuration;

        if (growthPercentage < 0.5f)
        {
            SetActiveStage(seedStage, true);
            SetActiveStage(saplingStage, false);
            SetActiveStage(currentFullStage, false);
        }
        else if (growthPercentage >= 0.5f && growthPercentage < 1f)
        {
            SetActiveStage(seedStage, false);
            SetActiveStage(saplingStage, true);
            SetActiveStage(currentFullStage, false);
        }
        else if (growthPercentage >= 1f)
        {
            SetActiveStage(seedStage, false);
            SetActiveStage(saplingStage, false);
            SetActiveStage(currentFullStage, true);
        }
    }

    void SetActiveStage(GameObject stage, bool isActive)
    {
        if (stage != null)
        {
            stage.SetActive(isActive);
        }
    }

    void Harvest()
    {
        Debug.Log("Harvested " + selectedSeed + "!");

        // Increment the fully grown count for the respective seed type
        if (selectedSeed == "Blueberry")
        {
            blueberryFullyGrownCount++;
        }
        else if (selectedSeed == "Lemon")
        {
            lemonFullyGrownCount++;
        }
        else if (selectedSeed == "Apple")
        {
            appleFullyGrownCount++;
        }
        else if (selectedSeed == "Banana")
        {
            bananaFullyGrownCount++;
        }
        else if (selectedSeed == "Grape")
        {
            grapeFullyGrownCount++;
        }
        else if (selectedSeed == "Durian")
        {
            durianFullyGrownCount++;
        }
        else if (selectedSeed == "Orange")
        {
            orangeFullyGrownCount++;
        }
        else if (selectedSeed == "Kiwi")
        {
            kiwiFullyGrownCount++;
        }
        else if (selectedSeed == "Starfruit")
        {
            starfruitFullyGrownCount++;
        }
        else if (selectedSeed == "Pear")
        {
            pearFullyGrownCount++;
        }
        else if (selectedSeed == "Gold")
        {
            goldFullyGrownCount++;
        }

        // Reset plant state
        isPlanted = false;
        isGrowing = false;
        isHarvestable = false;
        growthTimer = 0f;

        SetActiveStage(seedStage, false);
        SetActiveStage(saplingStage, false);
        SetActiveStage(currentFullStage, false);
        Destroy(currentFullStage);

        selectedSeed = "";

        // Update the UI with the latest seed and fully grown counts
        UpdateSeedCountUI();

        SE.Instance.PlayOneShot(2);
    }

    public void UpdateSeedCountUI()
    {
        // Update seed counts for all fruit types

        if (blueberrySeedCountText != null)
        {
            blueberrySeedCountText.text = "Blueberry Seeds: " + blueberrySeedCount;
        }

        if (lemonSeedCountText != null)
        {
            lemonSeedCountText.text = "Lemon Seeds: " + lemonSeedCount;
        }

        if (appleSeedCountText != null)
        {
            appleSeedCountText.text = "Apple Seeds: " + appleSeedCount;
        }

        if (bananaSeedCountText != null)
        {
            bananaSeedCountText.text = "Banana Seeds: " + bananaSeedCount;
        }

        if (grapeSeedCountText != null)
        {
            grapeSeedCountText.text = "Grape Seeds: " + grapeSeedCount;
        }

        if (durianSeedCountText != null)
        {
            durianSeedCountText.text = "Durian Seeds: " + durianSeedCount;
        }

        if (orangeSeedCountText != null)
        {
            orangeSeedCountText.text = "Orange Seeds: " + orangeSeedCount;
        }

        if (kiwiSeedCountText != null)
        {
            kiwiSeedCountText.text = "Kiwi Seeds: " + kiwiSeedCount;
        }

        if (starfruitSeedCountText != null)
        {
            starfruitSeedCountText.text = "Starfruit Seeds: " + starfruitSeedCount;
        }

        if (pearSeedCountText != null)
        {
            pearSeedCountText.text = "Pear Seeds: " + pearSeedCount;
        }

        if (goldSeedCountText != null)
        {
            goldSeedCountText.text = "Gold Seeds: " + goldSeedCount;
        }


        // Update inventory UI with counts (adjust as needed)

        if (inventoryBlueberryCount != null)
        {
            inventoryBlueberryCount.text = $"Blueberry Seeds: {blueberrySeedCount}\nFully Grown: {blueberryFullyGrownCount}";
        }

        if (inventoryLemonCount != null)
        {
            inventoryLemonCount.text = $"Lemon Seeds: {lemonSeedCount}\nFully Grown: {lemonFullyGrownCount}";
        }

        if (inventoryAppleCount != null)
        {
            inventoryAppleCount.text = $"Apple Seeds: {appleSeedCount}\nFully Grown: {appleFullyGrownCount}";
        }

        if (inventoryBananaCount != null)
        {
            inventoryBananaCount.text = $"Banana Seeds: {bananaSeedCount}\nFully Grown: {bananaFullyGrownCount}";
        }

        if (inventoryGrapeCount != null)
        {
            inventoryGrapeCount.text = $"Grape Seeds: {grapeSeedCount}\nFully Grown: {grapeFullyGrownCount}";
        }

        if (inventoryDurianCount != null)
        {
            inventoryDurianCount.text = $"Durian Seeds: {durianSeedCount}\nFully Grown: {durianFullyGrownCount}";
        }

        if (inventoryOrangeCount != null)
        {
            inventoryOrangeCount.text = $"Orange Seeds: {orangeSeedCount}\nFully Grown: {orangeFullyGrownCount}";
        }

        if (inventoryKiwiCount != null)
        {
            inventoryKiwiCount.text = $"Kiwi Seeds: {kiwiSeedCount}\nFully Grown: {kiwiFullyGrownCount}";
        }

        if (inventoryStarfruitCount != null)
        {
            inventoryStarfruitCount.text = $"Starfruit Seeds: {starfruitSeedCount}\nFully Grown: {starfruitFullyGrownCount}";
        }

        if (inventoryPearCount != null)
        {
            inventoryPearCount.text = $"Pear Seeds: {pearSeedCount}\nFully Grown: {pearFullyGrownCount}";
        }

        if (inventoryGoldCount != null)
        {
            inventoryGoldCount.text = $"Gold Seeds: {goldSeedCount}\nFully Grown: {goldFullyGrownCount}";
        }
    }

    void ToggleInventory()
    {
        // Toggle the inventory panel visibility
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    void OnGUI()
    {
        if (!string.IsNullOrEmpty(currentMessage) && isPlayerInRange)
        {
            GUI.skin.label.fontSize = 30;
            GUI.skin.label.normal.textColor = Color.black;
            GUI.Label(new Rect(10, 10, 300, 40), currentMessage);
        }
    }
}
