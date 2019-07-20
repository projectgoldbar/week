using UnityEngine;
using UnityEngine.UI;

public class ChoiceEvolve : MonoBehaviour
{
    public Evolve evolve;
    public Manager manager;

    public EvolveText EvolveAnimObject;
   

    private void Awake()
    {
        manager = FindObjectOfType<Manager>();
        EvolveAnimObject = FindObjectOfType<EvolveText>();
    }

    private void OnEnable()
    {
        GetComponent<Image>().sprite = evolve.sprite;
    }

    public void Evolve()
    {
        FindObjectOfType<EvolveSystem>().evolveFunc[evolve.idx]();
        var x = evolve.lvUpdescription[evolve.lv];
        evolve.lv++;
        //transform.parent.gameObject.SetActive(false);
        manager.evolUi.SetActive(false);
        //manager.playerController.SetActive(true);
        manager.GameResume();

        var player = manager.playerData;


      

        EvolveAnimObject.text.GetComponent<RectTransform>().anchoredPosition =
        Camera.main.WorldToScreenPoint(player.transform.position + (Vector3.up * 1.5f));


        EvolveAnimObject.text.text = x;
        //EvolveAnimObject.text.text = "요기요";
        EvolveAnimObject.Anim.Play("TextMove");
        FindObjectOfType<StageManager>().tarGetPointer.gameObject.SetActive(true);
    }
}