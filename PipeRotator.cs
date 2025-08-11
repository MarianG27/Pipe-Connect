using UnityEngine;

public class PipeRotator : MonoBehaviour
{
    void Start()
    {
        // Posibile rotații în jurul axei Z
        float[] possibleRotations = { 0f, 90f, 180f, -90f };

        // Alege una aleator
        int index = Random.Range(0, possibleRotations.Length);
        float chosenRotation = possibleRotations[index];

        // Aplică rotația
        transform.rotation = Quaternion.Euler(0, 0, chosenRotation);
    }

    void OnMouseDown()
    {
        // Poți păstra această funcție dacă vrei să rotești țeava la clic
        transform.Rotate(0, 0, -90);
    }
}
