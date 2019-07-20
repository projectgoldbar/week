using UnityEngine;
using UnityEngine.UI;

public class EpSlider : MonoBehaviour
{
    public Slider slider;
    public Slider explainSlider;
    public PlayerData playerData;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        FindPlayer();
        RollEpSet();
    }

    private void FindPlayer()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    private void RollEpSet()
    {
        var x = playerData.rollEp;
        var maxEp = playerData.maxEp;
        var y = x / maxEp;
        explainSlider.value = y;
    }

    // Update is called once per frame
    private void Update()
    {
        slider.value = (playerData.ep / playerData.maxEp);
    }
}