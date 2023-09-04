using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerRaycastHit : MonoBehaviour
{
    public float maxDetectionDistance = 10f;
    public LayerMask detectionLayer; // Specify the layers you want to detect.

    private Camera cam;

    [SerializeField] private Transform leafPos;
    [SerializeField] private TMP_Text leafText;
    [SerializeField] private TMP_Text pressEText;
    [SerializeField] private TMP_Text monologueText;
    private int Count = 0;

    private bool isBoxOn;
    void Start()
    {
        // Get the main camera in the scene.
        cam = Camera.main;
    }

    void Update()
    {
        // Find all objects in the specified layer.
        Collider[] detectedObjects = Physics.OverlapSphere(cam.transform.position, maxDetectionDistance, detectionLayer);

        foreach (Collider objCollider in detectedObjects)
        {
            GameObject detectedObject = objCollider.gameObject;

            // Check if the object is within the camera's view.
            if (IsObjectInView(detectedObject, cam))
            {
                // Check if the object is within the maximum detection distance.
                if (Vector3.Distance(cam.transform.position, detectedObject.transform.position) <= maxDetectionDistance)
                {
                    {
                        if (detectedObject.name == "Leaf")
                        {
                            pressEText.SetText("Press E");
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                // Check if the detected object's name is "Leaf."
                                if (detectedObject.name == "Leaf")
                                {
                                    
                                    Count += 1;
                                    leafText.text = "Leaves: " + Count + "/36";
                                    detectedObject.transform.position = new Vector3(106, 1.5f, -132);
                                    if (Count==36)
                                    {
                                        monologueText.SetText("Now throw it all in the rubbish bin.");
                                        isBoxOn = true;
                                    }
                                }
                                pressEText.SetText("");
                            }
                        }
                        if (detectedObject.name == "Box" && isBoxOn==true)
                        {
                            pressEText.SetText("Press E");
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                // Check if the detected object's name is "Leaf."
                                if (detectedObject.name == "Box")
                                {
                                    monologueText.SetText("");
                                    detectedObject.GetComponent<TransformTheObject>().LeafDetected();
                                   isBoxOn = false;
                                }

                                pressEText.SetText("");
                            }
                        }
                        if (detectedObject.name==null)
                        {
                            pressEText.SetText("");
                        }
                        
                    }
                    // You can also perform actions like interact with the object or trigger events.
                }
            }
        }
    }

    // Function to check if an object is within the camera's view.
    private bool IsObjectInView(GameObject obj, Camera camera)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null)
        {
            Bounds objectBounds = renderer.bounds;
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, objectBounds);
        }

        return false;
    }
}
