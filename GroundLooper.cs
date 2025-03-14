using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    public GameObject land01;
    public GameObject land02;
    public GameObject land03;
    public float moveSpeed = 5f;
    public float resetZ = -30f; // When ground moves past this Z, it resets
    public float groundLength = 1443f; // Distance from -30 to 1413

    void Update()
    {
        MoveGround(land01);
        MoveGround(land02);
        MoveGround(land03);
        
        CheckAndRespawn(land01, land02, land03);
        CheckAndRespawn(land02, land03, land01);
        CheckAndRespawn(land03, land01, land02);
    }

    void MoveGround(GameObject land)
    {
        if (land != null)
        {
            land.transform.position += Vector3.back * moveSpeed * Time.deltaTime; // Move along Z-axis
            Debug.Log($"Moving {land.name} to {land.transform.position}");
        }
        else
        {
            Debug.LogWarning("Land GameObject is not assigned.");
        }
    }

    void CheckAndRespawn(GameObject land, GameObject otherLand, GameObject anotherLand)
    {
        if (land != null && otherLand != null && anotherLand != null)
        {
            if (land.transform.position.z <= resetZ)
            {
                float newZ = Mathf.Max(otherLand.transform.position.z, anotherLand.transform.position.z) + groundLength;
                land.transform.position = new Vector3(land.transform.position.x, land.transform.position.y, newZ);
                Debug.Log($"Respawning {land.name} to {land.transform.position}");
            }
        }
        else
        {
            Debug.LogWarning("One or more Land GameObjects are not assigned.");
        }
    }
}
