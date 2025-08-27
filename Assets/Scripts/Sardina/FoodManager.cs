using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField]
    private GameEvents webEvents;

    [SerializeField]
    private GameObject foodPrefab;

    [SerializeField]
    private Transform topAreaLeft;

    [SerializeField]
    private Transform topAreaRight;

    [SerializeField]
    private Transform midAreaLeft;

    [SerializeField]
    private Transform midAreaRight;

    [SerializeField]
    private Transform bottomAreaLeft;

    [SerializeField]
    private Transform bottomAreaRight;

    [SerializeField]
    [Range(0.0f, 1.0f)] 
    private float bottomAreaProbability;

    private void OnEnable()
    {
        webEvents.AddListener(WebEventsCallback);
    }

    private void OnDisable()
    {
        webEvents.RemoveListener(WebEventsCallback);
    }

    private void WebEventsCallback(object data)
    {
        if (data is SendFoodEvent) SpawnFood();
    }

    private void SpawnFood()
    {
        Vector2 spawnPoint = Vector2.zero;

        float randomHeight = Random.value;
        float midThreshold = ((1.0f - bottomAreaProbability) / 2) + bottomAreaProbability;
        if (randomHeight < bottomAreaProbability)
        {
            spawnPoint.x = Random.Range(bottomAreaLeft.position.x, bottomAreaRight.position.x);
            spawnPoint.y = bottomAreaLeft.position.y;
        }
        else if (randomHeight < midThreshold)
        {
            spawnPoint.x = Random.Range(midAreaLeft.position.x, midAreaRight.position.x);
            spawnPoint.y = midAreaLeft.position.y;
        }
        else
        {
            spawnPoint.x = Random.Range(topAreaLeft.position.x, topAreaRight.position.x);
            spawnPoint.y = topAreaLeft.position.y;
        }

        Instantiate(foodPrefab, spawnPoint, Quaternion.identity);
    }
}
