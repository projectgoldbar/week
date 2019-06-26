using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameScene : MonoBehaviour
{
    public MiddleAllPanel middleAllPanel;
    public StatPanel statPanel;
    public SkinPanel skinPanel;
    public StorePanel storePanel;

    public DirectionUIController directionUIController;

    public void Start()
    {
        StatButton();
    }

    public void StatButton()
    {
        statPanel.OpenPanel();
        skinPanel.ClosePanel();
        storePanel.ClosePanel();
    }

    public void SkinButton()
    {
        statPanel.ClosePanel();
        skinPanel.OpenPanel();
        storePanel.ClosePanel();
    }

    public void StoreButton()
    {
        statPanel.ClosePanel();
        skinPanel.ClosePanel();
        storePanel.OpenPanel();
    }

    public void DownPanel()
    {
        middleAllPanel.OpenPanel();
        directionUIController.OffTriangleOutlineforDownPanel();
    }

    public void UpPanel()
    {
        middleAllPanel.ClosePanel();
        directionUIController.OnTriangleOutlineforUpPanel();
    }

    public void UpPanelwhenEnter()
    {
        middleAllPanel.ClosedPanelwhenEnter();
    }
}