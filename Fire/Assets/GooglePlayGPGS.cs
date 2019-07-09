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


public class GooglePlayGPGS : Singleton<GooglePlayGPGS>
{
    public Text debugText;
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


    //public Text LoginText;
    //public Text UserNicName;
    //public Image UserImage;

    //public InputField field;
    //public Button scoreButton;

    //public Text LoadDataText;


    private bool _authenticating = false;
    public bool Authenticated { get { return Social.Active.localUser.authenticated; } }

    TestSaveData TestData = new TestSaveData();

    private void Start()
    {
        GoogleServicesInit();
    }

    public void GoogleServicesInit()
    {
        debugText.text = "이닛들어옴";
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

        // enables saving game progress.
        .EnableSavedGames()
        // Will bring up a prompt for consent.
        .RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        .RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        .RequestIdToken()
        .Build();
        debugText.text = "이닛1";

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        // Google Play 게임 플랫폼 활성화
        PlayGamesPlatform.Activate();
        debugText.text = "이닛2";
        GoogleLogin();
        //debugText.text = "이닛3";

    }
    public void GoogleLogin()
    {
        if (Authenticated || _authenticating)
        {
            debugText.text = "로그인중";
            return;
        }

        _authenticating = true;

        Social.localUser.Authenticate((bool success) => {

            _authenticating = false;
            if (success)
            {
                debugText.text = "로그인성공";

                //성공
                //LoginText.text = "Login 되셧습니다.";
                //UserNicName.text = Social.localUser.userName;
                //if(Authenticated) OpenLoadGame(SaveFindName);
            }
            else
            {
                debugText.text = "로그인실패";

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

    
    public void Starter_AchievementPosting()
    {
        {
            //Social.ReportScore(100, GPGSIds.achievement_starter, (bool success) => 
            //{
            //    if (success)
            //    {
            //        Debug.Log("스타터 업적 열림");
            //    }
            //    else
            //    {

            //    }
            //});
        }
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_starter, 100, (bool success) =>
        {
            if (success)
            {
                debugText.text = "스타터 업적 열림";
            }
            else
            {

            }
        });
    }


    public void beginner_AchievementPosting()
    {
        Social.ReportScore(100, GPGSIds.achievement_beginner, (bool success) =>
        {
            if (success)
            {
                Debug.Log("비기너 업적 열림");
            }
            else
            {

            }
        });
    }


    #endregion

    #region 업적에 점수게시 -> 업적 진행도 표시 (0.0 ~ 100.0f) 

    public void AchievementsReportProgress(string AchievementsID , int progressvalue)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(AchievementsID, progressvalue, (bool success) => 
        {

        });
    }
    #endregion


    #region 리더보드 UI
    public void GoogleLederBoardUI()
    {
        debugText.text = "리더보드유아이눌림";

        if (Authenticated)
        {
            debugText.text = "리더보드유아이호출";
            Social.ShowLeaderboardUI();
        }



    }

    public void GoogleLederBoardUITarget(string LeaderBoardID)
    {
        if(Authenticated)
        PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderBoardID);
    }
    #endregion

    #region 리더보드에 점수게시

    public void LeaderBoardPostring()
    {
        if (Authenticated)
            GoogleLederBoardPostingScore(GPGSIds.leaderboard_sumsumzombie_leaderboard,100);
    }


    public void GoogleLederBoardPostingScore(string LeaderBoardID , long PostingScore)
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
    #endregion

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
    #endregion


    private string SaveFindName = "Game";

    public void SaveUIOpen()
    {
        if (Authenticated)
            ShowSelectUI();
    }
    void ShowSelectUI()
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
        if(Authenticated)
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
    void SaveGame(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        #region 클라우드에 저장할 데이터 
        TestSaveData TestData = new TestSaveData();
        TestData.TestInt = "111";
        TestData.Testfloat = "10.110f";
        TestData.TestString = "저장합니다";
        #endregion

        var stringToSave = JsonUtility.ToJson(TestData);
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

    #endregion

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
    void LoadGameData(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }
    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string dd = Encoding.UTF8.GetString(data);
            var text = JsonUtility.FromJson<TestSaveData>(dd);
           // LoadDataText.text = text.TestInt + text.Testfloat + text.TestString;
        }
        else
        {
            // handle error
           // LoadDataText.text = "LoadError";
        }
    }
    #endregion


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


