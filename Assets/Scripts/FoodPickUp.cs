using UnityEngine;
using Random = UnityEngine.Random;

public class FoodPickUp : MonoBehaviour
{
    public BoxCollider groundArea;

    //
    private void Start()
    {
        RandomizeFood();
    }

    //
    private void RandomizeFood()
    {
        Bounds bounds = groundArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        x = Mathf.Round(x);
        z = Mathf.Round(z);

        transform.position = new Vector3(x, 0.25f, z);

        gameObject.SetActive(true);
    }

    //
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RandomizeFood();
        }
    }
}