using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZoneGate : MonoBehaviour
{
    public Animator DoorAnim;

    public GameObject[] Zombies;

    public GameObject[] Doors;

    private void OnTriggerEnter(Collider other)
    {
        //DoorAnim.Play("DoorOpen");
        LayerNameChange();
        StartCoroutine(ZombiesGo());
    }
    public void LayerNameChange()
    {
        Doors[0].layer = 11;
        Doors[1].layer = 11;
    }


    private IEnumerator ZombiesGo()
    {
        for (int i = 0; i < Zombies.Length; i++)
        {
            Zombies[i].SetActive(true);
            yield return null;
        }
        //GetComponent<BoxCollider>().enabled = false;
    }
}
