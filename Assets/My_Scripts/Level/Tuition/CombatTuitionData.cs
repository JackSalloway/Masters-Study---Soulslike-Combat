using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class CombatTuitionData : MonoBehaviour
{
    public bool combatTutorialActive = false; // Variable to tell if a tutorial is active
    public string optionsText; // Options text will always be the same for large tutorials
    public GameObject combatTutorialPrefab; // Variable assigned to the combat tutorial game object prefab
    public Transform canvasTransform; // Variable assigned to the UI canvas

    [Header("Combat Tutorial Progression Values")]
    public bool playerEnteredArena = false; // Variable to tell when the player has entered the arena for the first time
    public bool playerAvoidedAttack = false; // Variable to tell when the player has avoided an attack for the first time
    public bool playerSpawnedInterface = false; // Variable to tell when the player has spawned the interface for the first time

    // Variables used to store the hierarchy path of each UI game object relevant to the TutorialTriggers parent object
    [Header("Hierarchy Path Strings")]
    [SerializeField] private string headerHierarchy = "Header_Wrapper/Header_Text";
    [SerializeField] private string shortDescriptionHierarchy = "Short_Description_Wrapper/Short_Description_Text";
    [SerializeField] private string extraDescriptionHierarchy = "Extra_Description_Wrapper/Extra_Description_Text";
    [SerializeField] private string optionsHierarchy = "Options_Wrapper/Options_Text";

    // Variable values for 0th phase tutorial
    [Header("Phase One Values")]
    [SerializeField] private bool firstHasSpawned = false; // Variable used to track if this tutorial has spawned once already
    [SerializeField] private string titleOne;
    [SerializeField] private string shortOne;
    [SerializeField] private string videoOne;
    [SerializeField] private string extraOne;

    // Variable values for 1st phase tutorial
    [Header("Phase Two Values")]
    [SerializeField] private bool secondHasSpawned = false; // Variable used to track if this tutorial has spawned once already
    [SerializeField] private string titleTwo;
    [SerializeField] private string shortTwo;
    [SerializeField] private string videoTwo;
    [SerializeField] private string extraTwo;

    // Variable values for 2nd phase tutorial
    [Header("Phase Three Values")]
    [SerializeField] private bool thirdHasSpawned = false; // Variable used to track if this tutorial has spawned once already
    [SerializeField] private string titleThree;
    [SerializeField] private string shortThree;
    [SerializeField] private string videoThree;
    [SerializeField] private string extraThree;

    [SerializeField] private GameObject combatTutorial; // Variable assigned to the newly instantiated combat tutorial

    private void Update()
    {   
        // Check if there is a combat tutorial active
        if (combatTutorialActive)
        {
            // Check if the player presses the E key
            if (Input.GetKeyDown(KeyCode.E)) DeleteCombatTutorial();
            return; // Prevent any more code being ran
        }

        // When player enters the arena for the first time, spawn the first tutorial
        if (playerEnteredArena && !firstHasSpawned)
        {
            SpawnCombatTutorial(titleOne, shortOne, videoOne, extraOne); // Instantiate first phase tutorial
            firstHasSpawned = true; // Update firstHasSpawned variable to prevent it spawning again
        }

        // When the player avoids an attack for the first time, spawn the second tutorial
        if (playerAvoidedAttack && !secondHasSpawned)
        {
            SpawnCombatTutorial(titleTwo, shortTwo, videoTwo, extraTwo); // Instantiate second phase tutorial
            secondHasSpawned = true; // Update secondHasSpawned variable to prevent it spawning again
        }

        // When the player spawns the typing interface for the first time, spawn the third tutorial
        if (playerSpawnedInterface && !thirdHasSpawned)
        {
            SpawnCombatTutorial(titleThree, shortThree, videoThree, extraThree); // Instantiate third phase tutorial
            thirdHasSpawned = true; // Update thirdHasSpawned variable to prevent it spawning again
        }
    }

    public void SpawnCombatTutorial(string titleText, string shortText, string videoPath, string extraText)
    {
        // Instantiate tutorial
        combatTutorial = Instantiate(combatTutorialPrefab, canvasTransform);
        
        // Set tutorial values
        SetTextValue(headerHierarchy, titleText);
        SetTextValue(shortDescriptionHierarchy, shortText);
        SetVideoClip(videoPath);
        SetTextValue(extraDescriptionHierarchy, extraText);
        SetTextValue(optionsHierarchy, optionsText);
        
        // Set combatTutorialActive variable to true to prevent Update method resuming
        combatTutorialActive = true;

        // Stop time
        Time.timeScale = 0f;
    }

    // Method to be called when the player presses E to dismiss the tutorial
    public void DeleteCombatTutorial()
    {
        Destroy(combatTutorial); // Delete combat tutorial
        combatTutorialActive = false; // Set combatTutorialActive to false to resume update method
        Time.timeScale = 1f; // Resume time
    }

    public void SetTextValue(string hierarchyPath, string desiredText)
    {
        Transform textObject = combatTutorial.transform.Find(hierarchyPath);
        TextMeshProUGUI headerText = textObject.GetComponent<TextMeshProUGUI>();
        headerText.text = desiredText;
    }

    public void SetVideoClip(string videoPath)
    {
        VideoClip videoClip = Resources.Load<VideoClip>(videoPath);

        Transform videoObject = combatTutorial.transform.Find("Media_Wrapper/Video_Player");
        VideoPlayer videoPlayer = videoObject.GetComponent<VideoPlayer>();

        videoPlayer.clip = videoClip;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }
}
