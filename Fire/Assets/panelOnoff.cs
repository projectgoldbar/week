using UnityEngine;

public enum PanelName { Upgrade, Equip, spl }

public class panelOnoff : MonoBehaviour
{
    public PanelName panelName = PanelName.Upgrade;

    public GameObject[] panels;

    public void Onoff()
    {
        switch (panelName)
        {
            case PanelName.Upgrade:
                if (OnTarget(0).activeSelf)return;

                    OnTarget(0).SetActive(true);
                break;

            case PanelName.Equip:
                if (OnTarget(1).activeSelf) return;

                OnTarget(1).SetActive(true);
                break;

            case PanelName.spl:
                if (OnTarget(2).activeSelf) return;

                    OnTarget(2).SetActive(true);
                break;
        }
    }

    public void ProcessClick(int n)
    {
        panelName = (PanelName)n;
        Onoff();
    }

    private GameObject OnTarget(int num)
    {
        foreach (var item in panels)
        {
            if (item.activeSelf) item.SetActive(false);
            else continue;
        }
        return panels[num];
    }
}