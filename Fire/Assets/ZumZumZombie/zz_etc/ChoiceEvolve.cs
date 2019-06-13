using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceEvolve : MonoBehaviour
{
    public Evolve evolve;
    public Manager manager;

    private void Awake()
    {
        manager = FindObjectOfType<Manager>();
    }

    private void OnEnable()
    {
        GetComponent<Image>().sprite = evolve.sprite;
    }

    public void Evolve()
    {
        FindObjectOfType<EvolveSystem>().evolveFunc[evolve.idx]();
        //transform.parent.gameObject.SetActive(false);
        manager.evolUi.SetActive(false);
        Time.timeScale = 1;
        manager.sw.Start();
    }
}