using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;
using System.Text;
using System;

public class TestSaveData
{
    public string TestInt;
    public string Testfloat;
    public string TestString;
}

public class GooglePlayGPGS : MonoBehaviour
{
    //public Text debugText;
    //private GooglePlayGPGS _instance;
    //public GooglePlayGPGS _Instance
    //{
    //    get
    //    {
    //        if (_instance != null) return _instance;
    //        _instance = FindObjectOfType<GooglePlayGPGS>();
    //        if (_instance == null)
    //        {
    //            _instance = new GameObject().AddComponent<GooglePlayGPGS>();
    //        }
    //        return _instance;
    //    }
    //}
    public static GooglePlayGPGS Instance;

    //public Text LoginText;
    //public Text UserNicName;
    //public Image UserImage;

    //public InputField field;
    //public Button scoreButton;

    //public Text LoadDataText;

    private bool _authenticating = false;
    public bool Authenticated { get { return Social.Active.localUser.authenticated; } }

    private TestSaveData TestData = new TestSaveData();

    private bool tutorial = false;
    private bool b_Stage1 = false;
    private bool b_Stage2 = false;
    private bool b_Stage5 = false;
    private bool b_Stage7 = false;
    private bool b_Stage10 = false;
    public bool x = false;

    private void Awake()
    {
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(this);
        //}
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        x = true;
    }

    public void InitProcess()
    {
        if (UserDataManager.Instance.userData.isTutorialClear == false)
        {
            Debug.Log("튜토리얼을진행전에는로그인하지 않습니다.");
        }
        else if (!Authenticated)
        {
            Debug.Log("로그인시도한다.");

            GoogleServicesInit();
        }
        else
        {
            LeaderBoardPostring((long)UserDataManager.Instance.userData.highScore);
        }
    }

    public void GoogleServicesInit()
    {
        //debugText.text = "이닛들어옴";
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

        // enables saving game progress.
        .EnableSavedGames()
        // Will bring up a prompt for consent.
        //.RequestEmail()
        //// requests a server auth code be generated so it can be passed to an
        ////  associated back end server application and exchanged for an OAuth token.
        //.RequestServerAuthCode(false)
        //// requests an ID token be generated.  This OAuth token can be used to
        ////  identify the player to other services such as Firebase.
        //.RequestIdToken()
        .Build();
        //debugText.text = "이닛1";

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        // Google Play 게임 플랫폼 활성화
        PlayGamesPlatform.Activate();
        //debugText.text = "이닛2";

        if (!Authenticated)
            GoogleLogin();
        //debugText.text = "이닛3";
    }

    public void GoogleLogin()
    {
        if (Authenticated || _authenticating)
        {
            //debugText.text = "로그인중";
            return;
        }

        _authenticating = true;

        Social.localUser.Authenticate((bool success) =>
        {
            _authenticating = false;
            if (success)
            {
                
                //debugText.text = "로그인성공";

                //성공
                //LoginText.text = "Login 되셧습니다.";
                //UserNicName.text = Social.localUser.userName;
                //if(Authenticated) OpenLoadGame(SaveFindName);
            }
            else
            {
                //debugText.text = "로그인실패";

                //실패
                //LoginText.text = "Login 실패 다시시도.";
                //GoogleLogin();
            }
        });
    }

    public Texture2D GetUserImage()
    {
        if (Authenticated)
            return Social.localUser.image;
        else
            return null;
    }

    

    public void GoogleLogOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    #region 업적 UI

    public void AchievementsUI()
    {
        if (Authenticated)
            Social.ShowAchievementsUI();
    }

    #endregion 업적 UI

    #region 업적열림

    /// <summary>
    /// 튜토리얼 업적 열림
    /// </summary>
    public void Starter_AchievementPosting()
    {
        Social.ReportProgress(GPGSIds.achievement_stater, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("튜토리얼 업적 열림");
                UserDataManager.Instance.userData.achievements[0] = true;
            }
        });
    }

    /// <summary>
    /// 스테이지1 업적 열림
    /// </summary>
    public void Stage1_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_stage1_clear, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("스테이지1 오픈");
                UserDataManager.Instance.userData.achievements[1] = true;
            }
        });
    }

    /// <summary>
    /// 스테이지2 업적 열림
    /// </summary>
    public void Stage2_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_stage2_clear, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("스테이지2 오픈");
                UserDataManager.Instance.userData.achievements[2] = true;
            }
        });
    }

    /// <summary>
    /// 스테이지5 업적 열림
    /// </summary>
    public void Stage5_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_stage5_clear, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("스테이지5 오픈");
                UserDataManager.Instance.userData.achievements[3] = true;
            }
        });
    }

    /// <summary>
    /// 스테이지7 업적 열림
    /// </summary>
    public void Stage7_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_stage_7_clear, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("스테이지7 오픈");
                UserDataManager.Instance.userData.achievements[4] = true;
            }
        });
    }

    /// <summary>
    /// 스테이지10 업적 열림
    /// </summary>
    public void Stage10_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_stage_10_clear, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("스테이지10 오픈");
                UserDataManager.Instance.userData.achievements[5] = true;
            }
        });
    }

    /// <summary>
    /// 누적 플레이 시간 n초 달성
    /// </summary>
    public void PlayTime_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_playtime, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"플레이타임 {3600}초 달성 오픈");
                UserDataManager.Instance.userData.achievements[6] = true;
            }
        });
    }

    /// <summary>
    /// 누적 플레이 횟수 n번 달성
    /// </summary>
    public void PlayCount_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_playcount, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"플레이횟수 {100}번 달성 오픈");
                UserDataManager.Instance.userData.achievements[7] = true;
            }
        });
    }

    /// <summary>
    /// 누적 획득 금액 n이상 달성
    /// </summary>
    public void Box150_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_gainbox_step3, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"박스 획득 {150}이상 달성 오픈");
                UserDataManager.Instance.userData.achievements[11] = true;
            }
        });
    }

    /// <summary>
    /// 누적 회복약 먹은 횟수 n번 달성
    /// </summary>
    public void Potion_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_potion, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"누적 회복약 먹은 횟수 {200}번 달성 오픈");
                UserDataManager.Instance.userData.achievements[8] = true;
            }
        });
    }

    /// <summary>
    /// 누적 박스 획득 갯수 n개 달성 오픈
    /// </summary>
    public void Box_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_gainbox_step2, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"누적 박스 획득 갯수 {100}개 달성 오픈");
                UserDataManager.Instance.userData.achievements[9] = true;
            }
        });
    }

    /// <summary>
    /// 누적 박스 깐 횟수 n개 달성 오픈
    /// </summary>
    public void Box50_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_gainbox_step1, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"누적 박스 획득 횟수 {50}개 달성 오픈");
                UserDataManager.Instance.userData.achievements[10] = true;
            }
        });
    }

    /// <summary>
    /// 누적 업그레이드 횟수 n번 달성 오픈
    /// </summary>
    public void Box200_Achievement_Open()
    {
        Social.ReportProgress(GPGSIds.achievement_gainbox_step4, 100f, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"누적 박스 횟수 {200}번 달성 오픈");
                UserDataManager.Instance.userData.achievements[12] = true;
            }
        });
    }

    #endregion 업적열림

    #region 업적에 점수게시 -> 업적 진행도 표시 (0.0 ~ 100.0f)

    public void AchievementsReportProgress(string AchievementsID, int progressvalue)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(AchievementsID, progressvalue, (bool success) =>
        {
        });
    }

    #endregion 업적에 점수게시 -> 업적 진행도 표시 (0.0 ~ 100.0f)

    #region 리더보드 UI

    public void GoogleLederBoardUI()
    {
        //debugText.text = "리더보드유아이눌림";

        if (Authenticated)
        {
            //debugText.text = "리더보드유아이호출";
            Social.ShowLeaderboardUI();
        }
    }

    public void GoogleLederBoardUITarget(string LeaderBoardID)
    {
        if (Authenticated)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderBoardID);
    }

    #endregion 리더보드 UI

    #region 리더보드에 점수게시

    public void LeaderBoardPostring(long score)
    {
        if (Authenticated)
            GoogleLederBoardPostingScore(GPGSIds.leaderboard_sumsumzombie_leaderboard, score);
    }

    public void GoogleLederBoardPostingScore(string LeaderBoardID, long PostingScore)
    {
        Social.ReportScore(PostingScore, LeaderBoardID, (bool success) =>
        {
            if (success)
            {
                //LoadDataText.text = $"리더보드 {PostingScore}게시 성공";
            }
            else
            {
                //LoadDataText.text = $"리더보드 게시 Error";
            }
        });
    }

    #endregion 리더보드에 점수게시

    #region 리더보드 정보 가져오기

    public void LeaderBoardScores()
    {
        if (Authenticated)
        {
            Social.LoadScores(GPGSIds.leaderboard_sumsumzombie_leaderboard, scores =>
            {
                if (scores.Length > 0)
                {
                    foreach (var item in scores)
                    {
                        ////리더보드의 아이디 == 로그인된 자신의 아이디
                        //if (item.userID == Social.localUser.id)
                        //{
                        //    var MyScore = item.value;
                        //}

                        var Score = item;
                        Debug.Log($"Id = {Score.userID} , Score = {Score.value} , Rank = {Score.rank}");
                    }
                }
            });
        }
    }

    public void LeaderBoardRank(string mStatus)
    {
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_sumsumzombie_leaderboard,
            LeaderboardStart.PlayerCentered,
            100,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (data) =>
            {
                mStatus = "Leaderboard data valid: " + data.Valid;
                mStatus += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;
            });

        Debug.Log(mStatus);
    }

    #endregion 리더보드 정보 가져오기

    private string SaveFindName = "Game";

    public void SaveUIOpen()
    {
        if (Authenticated)
            ShowSelectUI();
    }

    private void ShowSelectUI()
    {
        uint maxNumToDisplay = 3;
        bool allowCreateNew = true;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }

    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            if (Authenticated)
            {
                OpenLoadGame(SaveFindName);
            }

            //SaveGame(game);
        }
        else
        {
            // handle cancel or error
        }
    }

    #region 데이터 클라우드Save(저장할 데이터 지정해야됨)

    //데이터 저장
    public void SaveButtonClick()
    {
        if (Authenticated)
            OpenSavedGame(SaveFindName);
    }

    public void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(filename,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLongestPlaytime,
                                                            OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            SaveGame(game);
        }
        else
        {
            // handle error
            //  LoadDataText.text = "SaveError";
        }
    }

    private void SaveGame(ISavedGameMetadata game)
    {
        savedata1 v = new savedata1();
        v.adoff = UserDataManager.Instance.userData.AdOff;
        v.goldBonus = UserDataManager.Instance.userData.goldBonus;
        v.pakage = UserDataManager.Instance.userData.pakage;
        v.Money = UserDataManager.Instance.userData.Money;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        #region 클라우드에 저장할 데이터

        var data = v;

        #endregion 클라우드에 저장할 데이터

        var stringToSave = JsonUtility.ToJson(data);
        byte[] bytes = Encoding.UTF8.GetBytes(stringToSave);
        savedGameClient.CommitUpdate(game, update, bytes, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            //string text = "Save_성공";
            //LoadDataText.text = text;
        }
        else
        {
            // handle error
            //string text = "Save_실패";
            //LoadDataText.text = text;
        }
    }

    #endregion 데이터 클라우드Save(저장할 데이터 지정해야됨)

    #region 데이터 클라우드Load

    //저장된 데이터 읽기
    public void LoadButtonClick()
    {
        OpenLoadGame(SaveFindName);
    }

    public void OpenLoadGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(filename,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLongestPlaytime,
                                                            OnLoadGameRead);
    }

    public void OnLoadGameRead(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            LoadGameData(game);
        }
        else
        {
            // handle error
        }
    }

    private void LoadGameData(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }
    struct savedata1
    {
        public bool adoff ;
        public bool goldBonus ;
        public bool pakage ;
        public float Money;
    }
    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {

            string dd = Encoding.UTF8.GetString(data);
            var text = JsonUtility.FromJson<savedata1>(dd);
            UserDataManager.Instance.userData.Money = text.Money;
            UserDataManager.Instance.userData.AdOff =  text.adoff;
            if (UserDataManager.Instance.userData.AdOff)
            {
                FindObjectOfType<AdmobBanner>().ToogleAd(false);
            }
            UserDataManager.Instance.userData.goldBonus = text.goldBonus ;
            UserDataManager.Instance.userData.pakage = text.pakage;
            if (UserDataManager.Instance.userData.pakage)
            {
                FindObjectOfType<AdmobBanner>().ToogleAd(false);
            }
            // LoadDataText.text = text.TestInt + text.Testfloat + text.TestString;
        }
        else
        {
            // handle error
            // LoadDataText.text = "LoadError";
        }
    }

    #endregion 데이터 클라우드Load

    public Texture2D getScreenshot()
    {
        // Create a 2D texture that is 1024x700 pixels from which the PNG will be
        // extracted
        Texture2D screenShot = new Texture2D(1024, 700);

        // Takes the screenshot from top left hand corner of screen and maps to top
        // left hand corner of screenShot texture
        screenShot.ReadPixels(
            new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
        return screenShot;
    }
}