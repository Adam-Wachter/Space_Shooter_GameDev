using UnityEngine;


public class DestroyAfterDelay : MonoBehaviour
{
    [Tooltip("Delay in seconds before the object is destroyed.")]
    float _delay = 1.5f;

    private void Start()
    {
        // Destroy the object after the specified delay.
        Destroy(gameObject, _delay);
    }
}