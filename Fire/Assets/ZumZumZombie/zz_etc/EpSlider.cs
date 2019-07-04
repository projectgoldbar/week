using UnityEngine;
using UnityEngine.UI;

public class EpSlider : MonoBehaviour
{
    public Slider slider;
    public PlayerData playerData;

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
        slider.value = ((playerData.ep + playerData.enduranceData) / playerData.maxEp);
    }
}