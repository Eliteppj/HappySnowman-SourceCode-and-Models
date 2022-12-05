using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameStart;
    public PlayerController playerController;
    public GameObject character;
    public GameObject EndCharacter;
    public float endTime;
    public bool isEnd;
    public float endLastTime;

    public GameObject TitleScene;
    public AudioSource audioSource;
    public AudioClip[] audioList;
    public GameObject CurrentSpawnPoint;

    public GameObject SP_GrassLand;
    public GameObject SP_DesertLand;
    public GameObject SP_MagmaLand;
    public GameObject SP_StoneLand;
    public GameObject SP_IceLand;

    public GameObject Flag_GrassLand;
    public GameObject Flag_DesertLand;
    public GameObject Flag_MagmaLand;
    public GameObject Flag_StoneLand;
    public GameObject Flag_IceLand;

    public int Clear;



    public GameObject snowman;
    public UniData.GMDATA_PlayerData playerdata;
    public UniData.PlayMode playMode = UniData.PlayMode.TitleUI;

    public int currentSongIndex;



    [Header("UI List")]
    public Canvas UI_MasterCanvas;
    public GameObject UI_BlackPaper;
    public GameObject UI_Title;
    public GameObject UI_Pause;
    public GameObject UI_Credit;
    public GameObject UI_Option;
    public GameObject UI_Help;

    public GameObject UI_Back;
    public GameObject UI_GeneralPanel;

    public GameObject UI_ResetInform;

    public TMP_Dropdown DirectUI_Resolution;
    public TMP_Dropdown DirectUI_AntiAliasing;
    public Slider DirectUI_MouseSensitivity;
    public Slider DirectUI_SoundVolume;
    public Toggle DirectUI_FullScreen;
    public Toggle DirectUI_VSync;

    public TextMeshProUGUI DirectUI_Clear;

    public TextMeshProUGUI DirectUI_Forward;
    public TextMeshProUGUI DirectUI_Backward;
    public TextMeshProUGUI DirectUI_Left;
    public TextMeshProUGUI DirectUI_Right;
    public TextMeshProUGUI DirectUI_Jump;
    public TextMeshProUGUI DirectUI_Interaction;



    [Header("Settngs")]
    public string hasGameSave = "false";
    public string hasPlayerSave = "false";

    public int currentResolutionNumber;

    public bool currentVSync;
    public int currentVSyncInt;



    public int currentAntiAilasing;
    public float currentSoundVolume;
    public float currentMouseSensitivity;

    public bool fullscreen;
    public int fullscreenInt;



    private void Start()
    {
        LoadGameData();
        LoadPlayerData();
        Respawn();
        QualitySettings.antiAliasing = 0;
        //currentSongIndex = RandomAudio();
        currentSongIndex = 1;
        audioSource.clip = audioList[currentSongIndex];
        audioSource.Play();

    }
    private void Update()
    {

        songPlayer();
        EndCheck();
    }


    public void test12()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            EndGame();
        }
    }


    private void LateUpdate()
    {
        KillZoneCheck();
    }
    private int RandomAudio()
    {
        return Random.Range(0, audioList.Length);
    }

    public void Respawn()
    {
        snowman.transform.position = CurrentSpawnPoint.transform.position;

    }

    public void KillZoneCheck()
    {
        if (snowman.transform.position.y < -20)
        {
            Respawn();
        }


    }

    public void LoadGameData()
    {
        if (PlayerPrefs.GetInt("GameSave") == 0) //save없음
        {
            ChangeStage(0);
        }
        else //세이브 데이터 있음
        {
            playerdata.stage = PlayerPrefs.GetInt("Stage");
            ChangeStage(playerdata.stage);
            playerdata.cookie = PlayerPrefs.GetInt("Cookie");
            playerdata.presentbox = PlayerPrefs.GetInt("PresentBox");

            playerdata.kForward = (KeyCode)PlayerPrefs.GetInt("kForward");
            playerdata.kBackward = (KeyCode)PlayerPrefs.GetInt("kBackward");
            playerdata.kLeft = (KeyCode)PlayerPrefs.GetInt("kLeft");
            playerdata.kRight = (KeyCode)PlayerPrefs.GetInt("kRight");
            playerdata.kJump = (KeyCode)PlayerPrefs.GetInt("kJump");
            playerdata.kFire = (KeyCode)PlayerPrefs.GetInt("kFire");
            playerdata.kInteraction = (KeyCode)PlayerPrefs.GetInt("kInteraction");
        }



    }

    public void LoadPlayerData()
    {

        if (PlayerPrefs.GetInt("PlayerData") == 0) //save없음
        {
            AntiAliasing(0);
            ChangeStage(0);
        }
        else //세이브 데이터 있음
        {
            currentResolutionNumber = PlayerPrefs.GetInt("Resolution");
            DirectUI_Resolution.value = currentResolutionNumber;
            ChangeResolution(currentResolutionNumber);

            currentAntiAilasing = PlayerPrefs.GetInt("AntiAliasing");
            DirectUI_AntiAliasing.value = currentAntiAilasing;
            AntiAliasing(currentAntiAilasing);

            currentSoundVolume = PlayerPrefs.GetFloat("Volume");
            DirectUI_SoundVolume.value = currentSoundVolume;
            audioSource.volume = currentSoundVolume;


            currentMouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
            DirectUI_MouseSensitivity.value = currentMouseSensitivity;

            currentVSyncInt = PlayerPrefs.GetInt("VSync");

            switch (currentVSyncInt)
            {
                case 0:
                    currentVSync = false;
                    DirectUI_VSync.isOn = false;
                    break;

                case 1:
                    DirectUI_VSync.isOn = true;
                    currentVSync = true;
                    break;

                default:
                    DirectUI_VSync.isOn = false;
                    currentVSync = false;
                    break;
            }


            fullscreenInt = PlayerPrefs.GetInt("FullScreen");

            PlayerPrefs.GetInt("FullScreen");


            switch (fullscreenInt)
            {
                case 0:
                    fullscreen = false;
                    DirectUI_FullScreen.isOn = false;
                    break;
                case 1:
                    fullscreen = true;
                    DirectUI_FullScreen.isOn = true;
                    break;
                default:
                    fullscreen = true;
                    DirectUI_FullScreen.isOn = true;
                    break;
            }

            DirectUI_Clear.text = PlayerPrefs.GetInt("Clear").ToString();
            Clear = PlayerPrefs.GetInt("Clear");

        }

    }
    public void SaveGameData()
    {
        PlayerPrefs.SetInt("GameSave", 1);
        PlayerPrefs.SetInt("Stage", playerdata.stage);
        PlayerPrefs.SetInt("SnowmanCookie", playerdata.presentbox);
        PlayerPrefs.SetInt("Cookie", playerdata.cookie);
        PlayerPrefs.Save();

    }

    public void SavePlayerData() //키설정
    {
        PlayerPrefs.SetInt("PlayerData", 1);
        PlayerPrefs.SetInt("kForward", (int)playerdata.kForward);
        PlayerPrefs.SetInt("kBackward", (int)playerdata.kBackward);
        PlayerPrefs.SetInt("kLeft", (int)playerdata.kLeft);
        PlayerPrefs.SetInt("kRight", (int)playerdata.kRight);
        PlayerPrefs.SetInt("kJump", (int)playerdata.kJump);
        PlayerPrefs.SetInt("kFire", (int)playerdata.kFire);
        PlayerPrefs.SetInt("kInteraction", (int)playerdata.kInteraction);

        PlayerPrefs.SetInt("Resolution", currentResolutionNumber);
        PlayerPrefs.SetInt("AntiAliasing", currentAntiAilasing);
        PlayerPrefs.SetFloat("Volume", DirectUI_SoundVolume.value);
        PlayerPrefs.SetFloat("Sensitivity", currentMouseSensitivity);
        PlayerPrefs.SetInt("VSync", currentVSyncInt);
        PlayerPrefs.SetInt("FullScreen", fullscreenInt);
        PlayerPrefs.SetInt("Clear", Clear);
        PlayerPrefs.Save();
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        CurrentSpawnPoint = SP_GrassLand;
        playerdata.stage = 0;
        ChangeStage(playerdata.stage);
        DirectUI_Clear.text = "0";
    }


    public void GamePlayStart() //Let's Go 버튼을 눌렀을 때 처리할 동작들
    {

    }

    public void SetOption() //옵션을 변경했을 때 처리할 동작들
    {

    }

    public void songPlayer() //인덱스에 따라 다음에 재생될 노래를 선택함
    {
        if (audioSource.time >= audioSource.clip.length) //현재 재생시간이 곡의 재생시간 이상일 때 
        {
            currentSongIndex++;

            if (currentSongIndex >= audioList.Length)
            {
                currentSongIndex = 0;
            }
            audioSource.clip = audioList[currentSongIndex];
            audioSource.time = 0;
            audioSource.Play();
        }

        //Debug.Log(audioSource.time + ":::" + audioSource.clip.length);
    }



    /////////////////////////UIAction


    public void PlayGame()
    {
        playMode = UniData.PlayMode.Gameplay;
        playerController.canControl = true; //조종 가능상태로 변경

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CloseTitle();
        TitleScene.SetActive(false);
        character.SetActive(true);
        character.GetComponent<CharacterController>().enabled = false;
        Respawn();
        character.GetComponent<CharacterController>().enabled = true;

    }


    public void BackToTitle()
    {
        playMode = UniData.PlayMode.TitleUI;
        playerController.canControl = false; //조종 가능상태로 변경

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        OpenTitle();
        ClosePause();
        CloseCredit();
        CloseOption();
        CloseHelp();
        ClosedGeneralPanel();
        TitleScene.SetActive(true);
        //character.SetActive(false); //버그인 것 같은데 한번 타이틀 나갔다 플레이하면 캐릭터가 이동방향에 따라 회전을 안함
    }

    public void OpenTitle()
    {
        UI_Title.SetActive(true);
    }

    public void CloseTitle()
    {
        UI_Title.SetActive(false);
    }
    public void OpenPause()
    {
        UI_Pause.SetActive(true);
    }
    public void ClosePause()
    {
        UI_Pause.SetActive(false);
        // isCanvasOn = false;
    }

    public void OpenGeneralPanel()
    {
        UI_GeneralPanel.SetActive(true);
    }
    public void ClosedGeneralPanel()
    {
        UI_GeneralPanel.SetActive(false);
    }
    public void OpenCredit()
    {
        UI_Credit.SetActive(true);
    }
    public void CloseCredit()
    {
        UI_Credit.SetActive(false);
    }
    public void OpenOption()
    {
        UI_Option.SetActive(true);
    }
    public void CloseOption()
    {
        UI_Option.SetActive(false);
    }
    public void OpenHelp()
    {
        UI_Help.SetActive(true);
    }
    public void CloseHelp()
    {
        UI_Help.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenReset()
    {
        UI_ResetInform.SetActive(true);
    }

    public void CloseReset()
    {
        UI_ResetInform.SetActive(false);
    }


    //각종 옵션들

    public void ChangeResolution(int var)
    {
        switch (var)
        {
            case 0:
                Screen.SetResolution(2560, 1440, fullscreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, fullscreen);
                break;
            case 2:
                Screen.SetResolution(1600, 900, fullscreen);
                break;
            case 3:
                Screen.SetResolution(1280, 720, fullscreen);
                break;
            case 4:
                Screen.SetResolution(960, 540, fullscreen);
                break;
            case 5:
                Screen.SetResolution(640, 360, fullscreen);
                break;

        }


    }

    public void AntiAliasing(int var)
    {
        switch (var)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
        }

    }

    public void VSync(bool var)
    {
        if (var)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }



    }


    public void ChangeStage(int var)
    {

        switch (var)
        {

            case 0:
                CurrentSpawnPoint = SP_GrassLand;
                playerdata.stage = var;
                Flag_GrassLand.GetComponent<MeshRenderer>().material.color = Color.green;
                Flag_DesertLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_MagmaLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_StoneLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_IceLand.GetComponent<MeshRenderer>().material.color = Color.red;
                break;

            case 1:
                CurrentSpawnPoint = SP_DesertLand;
                playerdata.stage = var;
                Flag_GrassLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_DesertLand.GetComponent<MeshRenderer>().material.color = Color.green;
                Flag_MagmaLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_StoneLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_IceLand.GetComponent<MeshRenderer>().material.color = Color.red;
                break;

            case 2:
                CurrentSpawnPoint = SP_MagmaLand;
                playerdata.stage = var;
                Flag_GrassLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_DesertLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_MagmaLand.GetComponent<MeshRenderer>().material.color = Color.green;
                Flag_StoneLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_IceLand.GetComponent<MeshRenderer>().material.color = Color.red;
                break;

            case 3:
                CurrentSpawnPoint = SP_StoneLand;
                playerdata.stage = var;
                Flag_GrassLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_DesertLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_MagmaLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_StoneLand.GetComponent<MeshRenderer>().material.color = Color.green;
                Flag_IceLand.GetComponent<MeshRenderer>().material.color = Color.red;
                break;

            case 4:
                CurrentSpawnPoint = SP_IceLand;
                playerdata.stage = var;
                Flag_GrassLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_DesertLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_MagmaLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_StoneLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_IceLand.GetComponent<MeshRenderer>().material.color = Color.green;

                break;

            default:
                CurrentSpawnPoint = SP_GrassLand;
                playerdata.stage = 0;
                Flag_GrassLand.GetComponent<MeshRenderer>().material.color = Color.green;
                Flag_DesertLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_MagmaLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_StoneLand.GetComponent<MeshRenderer>().material.color = Color.red;
                Flag_IceLand.GetComponent<MeshRenderer>().material.color = Color.red;
                break;

        }
    }

    public void EndGame()
    {
        isEnd = true;
        playerController.canControl = false;
        EndCharacter.SetActive(true);
    }

    public void EndCheck()
    {
        if (isEnd)
        {
            endTime += Time.deltaTime;
        }

        if (endTime > endLastTime)
        {
            BackToTitle();

            EndCharacter.SetActive(false);
            isEnd = false;
            endTime = 0;
            playerController.canControl = true;
            CurrentSpawnPoint = SP_GrassLand;
            Clear += 1;
            DirectUI_Clear.text = Clear.ToString();
            SaveGameData();
            SavePlayerData();
            ChangeStage(0);
            PlayerPrefs.SetInt("Stage", 0);
          
        }
    }

}


