using UnityEngine;

public class ParallaxManager : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallaxMultiplier = 0.2f;

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void LateUpdate()
    {
        Vector3 cameraPos = cameraTransform.position;
        transform.position = startPosition + new Vector3(cameraPos.x * parallaxMultiplier, cameraPos.y * parallaxMultiplier, 0f);
    }
}
