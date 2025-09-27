using UnityEngine;

public class CameraInstance : MonoBehaviour
{
    //the instance number of this camera (0-3)
    private int myInstanceNumber;

    [SerializeField] private InstanceManager myInstanceManager;

    private Camera myCamera;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();
    }


    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;

        myCamera.rect = myInstanceManager.GetCameraViewportRect(myInstanceNumber);
    }
}
