using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameScene : MonoBehaviour
{
    public MiddleAllPanel middleAllPanel;

    public StatPanel statPanel;
    public SkinPanel skinPanel;
    public StorePanel storePanel;
    public BoxPanel boxPanel;

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
        boxPanel.ClosePanel();
    }

    public void SkinButton()
    {
        statPanel.ClosePanel();
        skinPanel.OpenPanel();
        storePanel.ClosePanel();
        boxPanel.ClosePanel();
    }

    public void BoxButton()
    {
        statPanel.ClosePanel();
        skinPanel.ClosePanel();
        storePanel.ClosePanel();
        boxPanel.OpenPanel();
    }

    public void StoreButton()
    {
        statPanel.ClosePanel();
        skinPanel.ClosePanel();
        storePanel.OpenPanel();
        boxPanel.ClosePanel();
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