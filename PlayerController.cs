using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float walkSpeed = 5.0f;

    private float rotationX = 0.0f;
    [SerializeField] CharacterController controller;

    [SerializeField] private TMP_Text monologueText;
    [SerializeField] private TMP_Text titleText;

    private bool isGameStart;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(FirstMonologue());
    }
    IEnumerator FirstMonologue()
    {
        
        monologueText.SetText("Collect all the leaves in the garden to make your day happy..");
        titleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        monologueText.SetText("");
        isGameStart = true;
        titleText.gameObject.SetActive(false);

    }
    void Update()
    {
        if (isGameStart)
        {
            // Mouse Input
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = -Input.GetAxis("Mouse Y") * sensitivity;

            rotationX += mouseY;
            rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f);

            transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
            controller.transform.rotation *= Quaternion.Euler(0.0f, mouseX, 0.0f);

            // Movement Input
            float moveDirectionX = Input.GetAxis("Horizontal");
            float moveDirectionZ = Input.GetAxis("Vertical");

            Vector3 moveDirection = controller.transform.right * moveDirectionX + controller.transform.forward * moveDirectionZ;
            moveDirection = moveDirection.normalized * walkSpeed;

            // Apply gravity
            moveDirection.y -= 9.8f * Time.deltaTime;

            // Apply movement to the controller
            controller.Move(moveDirection * Time.deltaTime);

            // Unlock cursor on Escape key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed = 13;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walkSpeed = 5;
            }
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
