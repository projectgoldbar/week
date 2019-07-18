using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    public Slider slider;
    public PlayerData playerData;
    public Image bloodImg;
    public Text hpText;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    // Update is called once per frame
    private void Update()
    {
        var x = playerData.hp / playerData.maxhp;
        hpText.text = Mathf.Round(playerData.hp).ToString() + " / " + playerData.maxhp.ToString();
        slider.value = x;

    }
}