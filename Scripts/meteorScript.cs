using UnityEngine;

public class meteorScript : MonoBehaviour
{
    [SerializeField] private float minMeteorSpeed;
    [SerializeField] private float maxMeteorSpeed;

    private float speed;
    private Rigidbody2D rb;

    void Start()
    {
        speed = Random.Range(minMeteorSpeed, maxMeteorSpeed);
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        Vector3 movement = new Vector3(-speed, -speed) * Time.deltaTime;

        newPosition += movement;
        transform.position = newPosition;

        if (transform.position.y <= -5f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.gameObject.name);
        if (hitInfo.gameObject.name == "Player")
            Application.Quit();
    }
}
