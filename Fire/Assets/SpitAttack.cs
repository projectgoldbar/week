using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitAttack : MonoBehaviour
{
    public Rigidbody rid;
    public float Power = 100;
    // Start is called before the first frame update


    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Power);

        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (targetScreenPos.x > Screen.width + 1.0f || targetScreenPos.x < -1.0f || targetScreenPos.y > Screen.height + 1.0f || targetScreenPos.y < -1.0f)
        {
            //pool로 집어넣음.
            spitPoolManager.Instance.SetSpitObj(gameObject);
        }
    }
}
