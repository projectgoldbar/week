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

    public OptionPanel optionPanel;

    public DirectionUIController directionUIController;

    public void Start()
    {
        StatButton();
    }

    //중앙 패널 버튼들 ㄱ
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

    // ㄴ 중앙 패널 버튼들

    // 다른 버튼들 ㄱ
    public void OptionPanelOpen()
    {
        optionPanel.OpenPanel();
    }

    public void OptionPanelClose()
    {
        optionPanel.ClosePanel();
    }

    public void LeaderBoardOpen()
    {
        //리더보드 창 열기
        Debug.Log("리더보드 창 열기");
    }

    public void ExitGameOpen()
    {
        //게임 나가기 창 열기
        Debug.Log("게임 나가기 창 열기");
    }

    public void ExitGameClose()
    {
        //게임 나가기 창 닫기
        Debug.Log("게임 나가기 창 닫기");
    }

    public void StartGame()
    {
        //게임 시작하기
        LeaveLobby();
    }

    // ㄴ 다른 버튼들
}