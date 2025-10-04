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


    [SerializeField] private UnityEngine.UI.Image background;

    [Header("Instance Information")]
    //position that each instance of the game should spawn at
    [SerializeField] private Vector3[] instancePositions;
    //colour each instance of the palyer should be
    [SerializeField] private Color[] playerColors;
    //where the camera should render on the screen
    [SerializeField] private Rect[] cameraViewportRects;
    //what colour the background is
    [SerializeField] private Color[] backgroundColors;

    private DES_GameManager gameManager = null;

    //has this instance been registered with the game manager
    public bool IsRegistered;

    private void Awake()
    {
        playerScript = player.GetComponent<PlayerController>();
        cameraScript = camera.GetComponent<CameraInstance>();
        parallaxScriptSky = parallaxSky.GetComponent<ParallaxManager>();
        parallaxScriptMountain = parallaxMountain.GetComponent<ParallaxManager>();
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

        background.color = backgroundColors[instanceNumber];
    }

    //returns the color that player instance should be
    public Color GetPlayerColor(int instanceNumber)
    {
        return playerColors[instanceNumber];
    }

    //returns the camera details for that instance
    public Rect GetCameraViewportRect(int instanceNumber)
    {
        return cameraViewportRects[instanceNumber];
    }
}
