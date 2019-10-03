using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationRush : MonoBehaviour
{

    public BoxCollider My_collider;
    public BoxCollider destination_collider;

    public CreationRushroot frontBackroot;

    public bool commingdir;

    private GameObject[] RushZombies;
    private void Awake()
    {
        CreateRushZombie();
    }
    private void CreateRushZombie()
    {
        Array.Resize(ref RushZombies, 3);

        for (int i = 0; i < 3; i++)
        {
            var go = GameObject.Instantiate<GameObject>(frontBackroot.RushZombie, transform);

            go.SetActive(false);
            RushZombies[i] = go;
        }
    }
    public IEnumerator Path()
    {
        
        for (int i = 0; i < RushZombies.Length; i++)
        {
            var agent = RushZombies[i].GetComponent<ZombieState.ZombiesComponent>().agent;
            if (agent != null)
            {
                var MyColl = frontBackroot.FindPoint(My_collider);
                agent.destination = MyColl;

                yield return null;
            }
        }
    }


    public void Position_Setting()
    {
        for (int i = 0; i < RushZombies.Length; i++)
        {
            this.RushZombies[i].transform.position = frontBackroot.FindPoint(destination_collider);
            this.RushZombies[i].SetActive(true);
        }
        StartCoroutine(Path());
    }
    private void Update()
    {
        for (int i = 0; i < RushZombies.Length; i++)
        {
            if (RushZombies[i].activeSelf &&
                Vector3.Distance(RushZombies[i].transform.position, transform.position) <= 10.0f)
            {
                RushZombies[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (commingdir)
            {
                if ((transform.position.z - other.transform.position.z) > 6)
                {
                    Debug.Log("Front앞으로 들어옴");
                    return;
                }
            }
            else
            {
                if ((other.transform.position.z - transform.position.z) > 6)
                {
                    Debug.Log("Back앞으로 들어옴");
                    return;
                }
            }
            Position_Setting();
        }
       
    }


   


}
