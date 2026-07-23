using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed;
    public Vector2 offset;
    public Transform target;
    Vector2 internalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        internalPos = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = offset;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(target.position, transform.parent.position) < speed * Time.deltaTime)
        {
            internalPos = new Vector3(target.position.x, target.position.y, transform.parent.position.z);
        }
        else
        {
            Vector2 dir = new Vector2(target.position.x - transform.parent.position.x, target.position.y - transform.parent.position.y).normalized;
            internalPos += dir * speed * Time.deltaTime;
        }

        transform.parent.position = new Vector3(Mathf.FloorToInt(internalPos.x * 16f) / 16f, Mathf.FloorToInt(internalPos.y * 16f) / 16f, transform.parent.position.z);
    }
}
