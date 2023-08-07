using UnityEngine;

// Destroy owning GameObject if any collider with a specific tag is trespassing
public class DestroyOnTrigger : MonoBehaviour
{
    public string tagToLookFor;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            Destroy(other.gameObject);
        }
    }
}
