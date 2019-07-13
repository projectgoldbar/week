using System.Collections;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform target;

    private Coroutine StopRutine;

    private void Awake()
    {
        target = GameObject.FindObjectOfType<PlayerData>().transform;
    }

    public float offset = 80.0f;

    public bool up = false;
    public bool isShaked;

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!isShaked)
        {
            transform.position = new Vector3(target.position.x, offset, target.position.z + 2f);
        }
        else
        {
            float x = Random.Range(-1f, 1f) * 0.7f;
            float y = Random.Range(-1f, 1f) * 0.7f;

            transform.position = new Vector3(target.position.x + x, offset, target.position.z + y);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CameraShake(0.5f);
        }
    }

    public void CameraShake(float duration)
    {
        StartCoroutine(CameraShakeC(duration));
    }

    private IEnumerator CameraShakeC(float duration)
    {
        isShaked = true;
        yield return new WaitForSeconds(duration);
        isShaked = false;
    }
}