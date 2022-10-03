using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float followSpeed;
    [SerializeField] private float deadZone;
    [SerializeField] private float yOffset;

    private bool centralizing = false;

    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        float distance = Vector3.Distance(transform.position, newPosition);

        if (target.position.y < -0.8f)
        {
            newPosition.y = -1.5f;
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
            return;
        }

        if (distance >= deadZone || centralizing)
        {
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
            if (distance < 0.1f)
                centralizing = false;
            else
                centralizing = true;
        }
    }
}
