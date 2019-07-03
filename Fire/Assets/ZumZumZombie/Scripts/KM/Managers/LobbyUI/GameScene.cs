using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class GameScene : MonoBehaviour
{
    public MiddleAllPanel middleAllPanel;

    public StatPanel statPanel;
    public SkinPanel skinPanel;
    public StorePanel storePanel;
    public BoxPanel boxPanel;

    public OptionPanel optionPanel;
    public ExitGamePanel exitGamePanel;

    public DirectionUIController directionUIController;

    public void Awake()
    {
        StatButton();
        //SceneManager.sceneLoaded += OnSceneLoaded;
        fadeOutImageObj = FindObjectOfType<FadeOutUI>().gameObject;
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
        exitGamePanel.OpenPanel();
    }

    public void ExitGameClose()
    {
        exitGamePanel.ClosePanel();
    }

    public void ExitGame()
    {
        exitGamePanel.ExitGame();
    }

    public void StartGame()
    {
        //게임 시작하기
        LeaveLobby();
    }

    // ㄴ 다른 버튼들
    public float leaveDuration = 4f;

    private float enterDuration = 3f;

    private float toIn = 0.0f;
    private float toOut = 255.0f;

    public GameObject fadeOutImageObj;
    private bool isCompletedFadeOut = false;
    private bool isCompletedFadeIn = false;

    private Color beforeFAdeOutColor;
    private Color nowColor;

    public LobbyBase_Controller lobbyBase_Controller;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeOutImageObj = FindObjectOfType<FadeOutUI>().gameObject;
    }

    private void LeaveLobby()
    {
        if (fadeOutImageObj.GetComponent<Image>().color.a != toIn / 255f && fadeOutImageObj.GetComponent<Image>().color.a != toOut / 255f)
        {
            Debug.Log(" LeaveLobby> " + LeanTween.tweensRunning + " // Don't ReLeaveLobby");
            return;
        }
        LeaveLobbyProcess();
    }

    private void LeaveLobbyProcess()
    {
        directionUIController.OnClickedforLeaveLobby();
        StartFadeImageAlpha(toIn);
        lobbyBase_Controller.lobbyPlayerController.RunningAnim();
        StartFadeOut();
        UpPanel();
    }

    public void EnterLobby()
    {
        if (fadeOutImageObj.GetComponent<Image>().color.a != toIn / 255f && fadeOutImageObj.GetComponent<Image>().color.a != toOut / 255f)
        {
            Debug.Log("EnterLobby>  " + LeanTween.tweensRunning + "// Don't ReEnterLobby");
            return;
        }

        EnterLobbyProcess();
    }

    private void EnterLobbyProcess()
    {
        lobbyBase_Controller.lobbyPlayerController.ResetLobbyPlayer();
        lobbyBase_Controller.doorController.OpenDoorTween(enterDuration);
        StartFadeImageAlpha(toOut);
        StartFadeIn();
        UpPanelwhenEnter();
    }

    private void StartFadeImageAlpha(float startTo)
    {
        beforeFAdeOutColor.a = startTo;
        fadeOutImageObj.GetComponent<Image>().color = beforeFAdeOutColor;
    }

    private void StartFadeOut()
    {
        fadeOutImageObj.GetComponent<Image>().raycastTarget = true;

        isCompletedFadeOut = false;
        var d = LeanTween.value(toIn, toOut, leaveDuration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeOutTweenComplete);
    }

    private void StartFadeIn()
    {
        fadeOutImageObj.GetComponent<Image>().raycastTarget = false;

        isCompletedFadeIn = false;
        var d = LeanTween.value(toOut, toIn, enterDuration);
        d.setOnUpdate(x => { ValueUpdateFade(x); });
        d.setOnComplete(FadeInTweenComplete);
    }

    private void ValueUpdateFade(float value)
    {
        nowColor.a = value / 255f;
        fadeOutImageObj.GetComponent<Image>().color = nowColor;
    }

    private void FadeOutTweenComplete()
    {
        FindObjectOfType<SceneMan>().OpenGame();
        isCompletedFadeOut = true;
    }

    private void FadeInTweenComplete()
    {
        isCompletedFadeIn = true;
    }
}