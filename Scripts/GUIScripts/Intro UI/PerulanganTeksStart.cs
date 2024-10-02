using UnityEngine;
using System.Collections;

public class PerulanganStartTeks : MonoBehaviour
{
    public GameObject targetObject;   // The GameObject to loop activation
    public float activeDuration = 2f; // Time for which the GameObject remains active
    public float inactiveDuration = 1f; // Time for which the GameObject remains inactive
    public Animator animator;          // Reference to the Animator component

    public IEnumerator LoopActivation()
    {
        while (true)
        {
            // Activate the object
            targetObject.SetActive(true);

            // Trigger animation if available
            if (animator != null)
            {
                animator.SetTrigger("Loop");
            }

            // Wait for the active duration
            yield return new WaitForSeconds(activeDuration);

            // Deactivate the object
            targetObject.SetActive(false);

            // Wait for the inactive duration
            yield return new WaitForSeconds(inactiveDuration);
        }
    }
}
