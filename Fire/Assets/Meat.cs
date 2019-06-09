using UnityEngine;

public class Meat : MonoBehaviour
{
    public int meatSection;
    private SectorManager sectorManager;

    private void Awake()
    {
        sectorManager = FindObjectOfType<SectorManager>();
    }

    private void OnEnable()
    {
        sectorManager.sectors[meatSection].currentMeat++;
    }

    private void OnDisable()
    {
        sectorManager.sectors[meatSection].currentMeat--;
    }
}