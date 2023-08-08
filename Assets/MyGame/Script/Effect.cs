using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    bool isVisible = false;
    private float disappearanceDelay = 0.2f;

    private void Start()
    {
        // Make the object initially invisible
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // Check if the mouse button is pressed (left mouse button in this case)
        if (Input.GetMouseButtonDown(0))
        {
            // Show the object when the mouse button is pressed
            isVisible = true;
            gameObject.SetActive(true);
        }
        // Check if the mouse button is released (left mouse button in this case)
        else if (Input.GetMouseButtonUp(0))
        {
            // Hide the object when the mouse button is released
            isVisible = false;
            gameObject.SetActive(false);
        }
        else 
            StartCoroutine(DisappearAfterDelay());
    }
    private IEnumerator DisappearAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(disappearanceDelay);

        // Hide the object after the delay
        gameObject.SetActive(false);
    }
}