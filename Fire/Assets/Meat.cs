using UnityEngine;

public class Meat : MonoBehaviour
{
    public int meatSection;
    private SectorManager sectorManager;
    private Vector3 RotationSpeed = new Vector3(0, 100f, 0);

    private void Awake()
    {
        sectorManager = FindObjectOfType<SectorManager>();
    }

    private void Update()
    {
        transform.Rotate(RotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnEnable()
    {
        //sectorManager.sectors[meatSection].currentMeat++;
    }

    private void OnDisable()
    {
        //sectorManager.sectors[meatSection].currentMeat--;
    }
}