using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZoneGate : MonoBehaviour
{
    public Animator DoorAnim;

    public Animator BuildingAnim;

    public GameObject[] Zombies;

    public GameObject[] Doors;

    private void OnTriggerEnter(Collider other)
    {
        DoorAnim.Play("DoorClose");
        BuildingAnim.Play("DoorClose");
        LayerNameChange();
        StartCoroutine(ZombiesGo());
    }
    public void LayerNameChange()
    {
        Doors[0].layer = 11;
        Doors[1].layer = 11;
    }


    private void Update()
    {
        if (tutorialzombieTracking.ZombieSturnCounting >= 8)
        {
            DoorAnim.Play("DoorOpen");
            BuildingAnim.Play("DoorOpen");
        }
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
