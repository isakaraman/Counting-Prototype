using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTheObject : MonoBehaviour
{
    private GameObject leafPos;
    void Start()
    {
        leafPos = GameObject.Find("Leaf Pos");
    }

    public void LeafDetected()
    {

        StartCoroutine(TransformLeafs());
    }

    IEnumerator TransformLeafs()
    {
        // Find all objects with the name "Leaf" in the scene.
        GameObject[] leaves = GameObject.FindGameObjectsWithTag("Leaf");

        // Iterate through each "Leaf" and destroy them with a one-second delay.
        foreach (GameObject leaf in leaves)
        {
            leaf.name = "Leaf1";
            leaf.gameObject.SetActive(true);
            leaf.gameObject.transform.SetParent(leafPos.transform);
            leaf.gameObject.transform.position = new Vector3(leafPos.transform.position.x,
         leafPos.transform.position.y, leafPos.transform.position.z);
            yield return new WaitForSeconds(0.2f); // Wait for one second before the next destruction.
        }
    }
}
