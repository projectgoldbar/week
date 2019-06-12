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
    }
}
