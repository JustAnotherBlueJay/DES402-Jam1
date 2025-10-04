using UnityEngine;
using UnityEngine.UIElements;

public class InstanceManager : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    private PlayerController playerScript;

    [SerializeField] private GameObject camera;
    private CameraInstance cameraScript;

    [SerializeField] private GameObject parallaxSky;
    [SerializeField] private GameObject parallaxMountain;
    private ParallaxManager parallaxScriptSky;
    private ParallaxManager parallaxScriptMountain;

    [SerializeField] private GameObject dialogueCanvas;
    private DialogueManager dialogueCanvasScript;

    [SerializeField] private TitleScreen titleScreen;

    private Music_Manager musicManager;

    public enum GameState
    {
        TitleScreen,
        InGame
    }
    public GameState gameState;


    [Header("Instance Information")]
    //position that each instance of the game should spawn at
    [SerializeField] private Vector3[] instancePositions;
    //where the camera should render on the screen
    [SerializeField] private Rect[] cameraViewportRects;

    private DES_GameManager gameManager = null;

    //has this instance been registered with the game manager
    public bool IsRegistered;

    private void Awake()
    {
        playerScript = player.GetComponent<PlayerController>();
        cameraScript = camera.GetComponent<CameraInstance>();
        parallaxScriptSky = parallaxSky.GetComponent<ParallaxManager>();
        parallaxScriptMountain = parallaxMountain.GetComponent<ParallaxManager>();
        dialogueCanvasScript = dialogueCanvas.GetComponent<DialogueManager>();
        musicManager = Music_Manager.instance;
    }

    //register itself with the gamemanger and apply instance data to the relevant objects
    public void RegisterSelf(int instanceNumber)
    {
        IsRegistered = true;

        transform.position = instancePositions[instanceNumber];

        playerScript.ApplyInstanceData(instanceNumber);
        cameraScript.ApplyInstanceData(instanceNumber);
        parallaxScriptSky.ApplyInstanceData(instanceNumber);
        parallaxScriptMountain.ApplyInstanceData(instanceNumber);
        dialogueCanvasScript.ApplyInstanceData(instanceNumber);
        titleScreen.ApplyInstanceData(instanceNumber);

    }


    //returns the camera details for that instance
    public Rect GetCameraViewportRect(int instanceNumber)
    {
        return cameraViewportRects[instanceNumber];
    }

    public void TransitionToGame()
    {
        gameState = GameState.InGame;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        playerScript.enabled = true;

        musicManager.StartMusic();

    }
}
