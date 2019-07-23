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
    public AchivementPanel achivementPanel;
    public GotoReviewPanel gotoReviewPanel;

    public DirectionUIController directionUIController;

    public float leaveDuration = 4f;
    private float enterDuration = 1.5f;

    private float toIn = 0.0f;
    private float toOut = 255.0f;

    public GameObject fadeOutImageObj;
    private bool isCompletedFadeOut = false;
    private bool isCompletedFadeIn = false;

    private Color beforeFAdeOutColor;
    private Color nowColor;

    public LobbyBase_Controller lobbyBase_Controller;

    private bool stateStat = false;
    private bool stateSkin = false;
    private bool stateBox = false;
    private bool stateStore = false;

    public void Awake()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        fadeOutImageObj = FindObjectOfType<FadeOutUI>().gameObject;
    }

    private void Start()
    {
        SoundManager.Instance.PlaySoundBGM("LOBBYBGM");
        // statPanel.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        //StatButton();
        //  statPanel.gameObject.SetActive(true);
    }

    //중앙 패널 버튼들 ㄱ
    public void StatButton()
    {
        //stateStat = false;
        stateSkin = false;
        stateBox = false;
        stateStore = false;

        if (stateStat == true)
        {
            UpPanelwhenSameButton();
            stateStat = false;

            return;
        }
        if (stateStat == false)
        {
            DownPanel();

            statPanel.OpenPanel();
            skinPanel.ClosePanel();
            storePanel.ClosePanel();
            boxPanel.ClosePanel();

            stateStat = true;
        }
    }

    public void SkinButton()
    {
        stateStat = false;
        //stateSkin = false;
        stateBox = false;
        stateStore = false;

        if (stateSkin == true)
        {
            UpPanelwhenSameButton();
            stateSkin = false;

            return;
        }

        if (stateSkin == false)
        {
            DownPanel();

            statPanel.ClosePanel();
            skinPanel.OpenPanel();
            storePanel.ClosePanel();
            boxPanel.ClosePanel();

            stateSkin = true;
        }
    }

    public void BoxButton()
    {
        stateStat = false;
        stateSkin = false;
        //stateBox = false;
        stateStore = false;

        if (stateBox == true)
        {
            UpPanelwhenSameButton();
            stateBox = false;

            return;
        }

        if (stateBox == false)
        {
            DownPanel();

            statPanel.ClosePanel();
            skinPanel.ClosePanel();
            storePanel.ClosePanel();
            boxPanel.OpenPanel();

            stateBox = true;
        }
    }

    

    public void StoreButton()
    {
        stateStat = false;
        stateSkin = false;
        stateBox = false;
        //stateStore = false;

        if (stateStore == true)
        {
            UpPanelwhenSameButton();
            stateStore = false;

            return;
        }

        if (stateStore == false)
        {
            DownPanel();

            statPanel.ClosePanel();
            skinPanel.ClosePanel();
            storePanel.OpenPanel();
            boxPanel.ClosePanel();

            stateStore = true;
        }
    }

    public void DownPanel()
    {
        middleAllPanel.OpenPanel();
        directionUIController.OffTriangleandOutlineforDownPanel();
        directionUIController.doorOutLine.OpenMiddlePanel();
        lobbyBase_Controller.StartObjController.OpenMiddlePanel();
    }

    public void UpPanel()
    {
        StatButton();

        middleAllPanel.ClosePanel();
        directionUIController.OnTriangleOutlineforUpPanel();
        directionUIController.doorOutLine.CloseMiddlePanel();
        lobbyBase_Controller.StartObjController.CloseMiddlePanel();
    }

    private void UpPanelwhenSameButton()
    {
        middleAllPanel.ClosePanel();
        directionUIController.OnTriangleOutlineforUpPanel();
        directionUIController.doorOutLine.CloseMiddlePanel();
        lobbyBase_Controller.StartObjController.CloseMiddlePanel();
    }

    public void UpPanelwhenEnter()
    {
        middleAllPanel.ClosedPanelwhenEnter();
    }

    public void OptionPanelOpen()
    {
        optionPanel.OpenPanel();
    }

    public void OptionPanelClose()
    {
        optionPanel.ClosePanel();
    }

    public void AchivementPanelOpen()
    {
        achivementPanel.OpenPanel();
    }

    public void AchivementPanelClose()
    {
        achivementPanel.ClosePanel();
    }

    public void LeaderBoardOpen()
    {
        GooglePlayGPGS.Instance.GoogleLederBoardUI();
    }

    public void AchivementBoardOpen()
    {
        GooglePlayGPGS.Instance.AchievementsUI();
    }

    public void Review()
    {
        Application.OpenURL("market://details?id=com.sum.zombie");
        UserDataManager.Instance.userData.gotoReview = true;
    }

    public void ExitGameOpen()
    {
        exitGamePanel.OpenPanel();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGameOpen();
        }
    }


    public void ExitGameClose()
    {
        exitGamePanel.ClosePanel();
    }

    public void ExitGame()
    {
        exitGamePanel.ExitGame();
    }

    public void GotoReviewPanelOpen()
    {
        gotoReviewPanel.OpenPanel();
    }

    public void GotoReviewPanelClose()
    {
        gotoReviewPanel.ClosePanel();
    }

    public void StartGame()
    {
        //게임 시작하기
        LeaveLobby();
    }

    public void MoneyReadButton()
    {
        GooglePlayGPGS.Instance.MoneyRead();
    }

    public void AdOffReadButton()
    {
        GooglePlayGPGS.Instance.AdOffRead();
    }

    public void GoldBonusReadButton()
    {
        GooglePlayGPGS.Instance.GoldBonusRead();
    }

    public void PakageReadButton()
    {
        GooglePlayGPGS.Instance.PakageRead();
    }

    public void GoogleRead()
    {
        GooglePlayGPGS.Instance.LoadButtonClick();
    }

    public static void GoogleSave()
    {
        GooglePlayGPGS.Instance.SaveButtonClick();
    }

    // ㄴ 다른 버튼들

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeOutImageObj = FindObjectOfType<FadeOutUI>().gameObject;
    }

    private void LeaveLobby()
    {
        if (fadeOutImageObj.GetComponent<Image>().color.a != toIn / 255f && fadeOutImageObj.GetComponent<Image>().color.a != toOut / 255f)
        {
            return;
        }
        LeaveLobbyProcess();
    }

    private void LeaveLobbyProcess()
    {
        SoundManager.Instance.PlaySoundSFX("STARTGAME");
        directionUIController.OnClickedforLeaveLobby();
        StartFadeImageAlpha(toIn);
        lobbyBase_Controller.lobbyPlayerController.RunningAnim();
        StartFadeOut();

        if (middleAllPanel.isOpened == true)
        {
            UpPanel();
        }
    }

    public void EnterLobby()
    {
        if (fadeOutImageObj.GetComponent<Image>().color.a != toIn / 255f && fadeOutImageObj.GetComponent<Image>().color.a != toOut / 255f)
        {
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