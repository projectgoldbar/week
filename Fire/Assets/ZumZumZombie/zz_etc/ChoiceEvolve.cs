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
        evolve.lv++;
        //transform.parent.gameObject.SetActive(false);
        manager.evolUi.SetActive(false);

        manager.playerController.SetActive(true);
        manager.GameResume();
    }
}