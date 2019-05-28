using UnityEngine;

public class Road : MonoBehaviour
{
    private float speed = 0f;
    private LevelManager levelManager;

    private void Awake()
    {
        var a = FindObjectOfType<InGameData>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (transform.position.z <= -80f)
        {
            transform.position = new Vector3(-10f, 0, 70.00f);
        }

        transform.Translate(transform.forward * -speed * Time.deltaTime);
    }
}