using UnityEngine;

public class meteorSpawnerScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject meteorPrefab;

    [Space]
    [SerializeField] private float minTimer;
    [SerializeField] private float maxTimer;

    private float timer;
    private float spawnerTimer;

    void Start()
    {
        timer = 0f;
        spawnerTimer = Random.Range(minTimer, maxTimer);
    }

    void Update()
    {
        Vector2 newPosition = mainCamera.transform.position;
        newPosition.y += 7.15f;

        transform.position = newPosition;
        if (timer >= spawnerTimer)
        {
            spawnerTimer = Random.Range(minTimer, maxTimer) + timer;
            for (int i = 0; i < Random.Range(3, 7); i++)
            {
                Vector3 meteorPosition = transform.position;
                meteorPosition.x += Random.Range(0, 8);

                var meteorRotation = transform.eulerAngles;

                if (meteorPosition.x <= transform.position.x)
                {
                    meteorRotation.z = Random.Range(45, 90);
                }
                else
                {
                    meteorRotation.z = Random.Range(0, 45);
                }

                GameObject meteor = Instantiate(meteorPrefab, meteorPosition, transform.rotation);
            }
        }

        timer += Time.deltaTime;
    }
}
