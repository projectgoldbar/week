using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform target;
    public GameObject Enemy;

    public float distance;
    public int angle;
    public float checkDuration;

    private void Start()
    {
        StartCoroutine(Checking(checkDuration));
    }

    private IEnumerator Checking(float duration)
    {
        while (true)
        {
            var f = Vector3.Distance(target.position, transform.position);
            Debug.Log(f);
            if (f < distance)
            {
                TrapOn();
                yield break;
            }
            yield return new WaitForSeconds(duration);
        }
    }

    private void TrapOn()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject.Instantiate(Enemy, transform.position, Quaternion.identity);
        }
    }
}