using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//ゲーム全体を管理する
//UIの表示・非表示
//ゲームの画面変化
//画面遷移に伴う音声
//プレイヤーのHPの管理
public class GameManager : MonoBehaviour
{
    //private int Score { get; set;} 
    public int currentScene { get; set; }
    public const int TITLE_SCENE = 0;
    public const int MAIN_SCENE = 1;
    public const int CLEAR_SCENE = 2;
    public const int GAMEOVER_SCENE = 3;

    //各種設定
    float movieTime = 15f; //冒頭ムービーの長さ
    public bool isMovieNow { get; set; } = false; //現在がムービーかどうかの判定
    public bool isConfig { get; set; } = false; //メニューを開いているかどうかの判定

    [Header("プレイヤーの初期設定")]
    [SerializeField]
    private int maxHP;
    public int HP { get; set; }

    [Space(10)]
    [Header("各種UI")]
    [SerializeField]
    GameObject hpGauge;
    RectTransform hpGaugeTransform;
    [SerializeField]
    GameObject gameOverCanvas;
    [SerializeField]
    GameObject gameMainCanvas;
    [SerializeField]
    GameObject gameClearCanvas;
    [SerializeField]
    GameObject focusCanvas;
    [SerializeField]
    GameObject configCanvas;

    [Space(10)]
    [Header("音声")]
    [SerializeField]
    AudioSource gameSE;
    [SerializeField]
    AudioSource gameBGM;
    [SerializeField]
    AudioClip gameOverSE;
    [SerializeField]
    AudioClip gameClearSE;

    [Space(10)]
    [Header("キャラクターコントローラー")]
    [SerializeField]
    PlayerHeliController playerHeliController;

    [Space(10)]
    [Header("カメラ")]
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject ManagerCamera;

    private void Start()
    {
        
        isConfig = false;
        currentScene = MAIN_SCENE;
        StartCoroutine("MovieStart");

    }

    //他のスクリプトよりも早く実行させるためにAwakeを使用している。
    //もし他に、Awakeを使用する場合で、実行順序を指定したい時には、
    //PlayerSetting → [Script Execution Order] からスクリプトの実行順序を制御する。

    void Awake()
    {
        HP = maxHP;
        configCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameClearCanvas.SetActive(false);
        gameMainCanvas.SetActive(true);

        hpGauge = GameObject.Find("hpGauge");
        hpGaugeTransform = hpGauge.GetComponent<RectTransform>();
        changePanelSize(ref hpGaugeTransform, 200f);

    }

    // Config画面への遷移
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && isConfig == false)
        {
            GoConfigPanel();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isConfig == true)
        {
            ReturnFromConfig();
        }
    }

    //プレイヤーのスクリプトから呼び出す。
    //HPを同期させる
    public void updateParam(string category, int damage)
    {
        if (category.Equals("HP"))
        {
            HP -= damage;
            float size = (float)HP / maxHP * 200f;
            changePanelSize(ref hpGaugeTransform, size);
        } 
    }

    //UIのサイズを変更する時に用いる。
    //HPゲージのrectTransformとx方向のsizeを引数にとる。
    void changePanelSize(ref RectTransform rectTransform,float size)
    {
        Vector2 rectSize = rectTransform.sizeDelta;
        rectSize.x = size;
        rectTransform.sizeDelta = rectSize;
    }

    //引数に応じて画面遷移
    //ゲームオーバーとゲームクリア時には、キャンバスの入れ替えと効果音、マウスの有効・無効を設定する。
    public void sceneChange(int flag)
    {
        if(flag==TITLE_SCENE){
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
        if(flag==MAIN_SCENE)
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        }
        
        if (flag == GAMEOVER_SCENE)
        {
            gameOverCanvas.SetActive(true);
            gameMainCanvas.SetActive(false);
            focusCanvas.SetActive(false);
            CanvasGroup canvasGroup = gameOverCanvas.GetComponent<CanvasGroup>();
            StartCoroutine("setMouse");
            //Time.timeScale = 0.2f;
            gameSE.PlayOneShot(gameOverSE);
            gameBGM.Stop();
            currentScene = GAMEOVER_SCENE;

        }

        if (flag == CLEAR_SCENE)
        {
            gameClearCanvas.SetActive(true);
            gameMainCanvas.SetActive(false);
            focusCanvas.SetActive(false);
            CanvasGroup canvasGroup = gameClearCanvas.GetComponent<CanvasGroup>();
            StartCoroutine("setMouse");
            gameSE.PlayOneShot(gameClearSE);
            gameBGM.Stop();
            currentScene = CLEAR_SCENE;
        }

    }

    //ゲームオーバーとゲームクリア時に遅れてマウスボタンが有効になるように設定する。
    IEnumerator setMouse()
    {
        yield return new WaitForSeconds(4f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }


    //現在のシーンIndexを返す
    public int getCurrentScene()
    {
        return currentScene;
    }

    //設定画面を開く
    public void GoConfigPanel()
    {
        Time.timeScale = 0f;
        gameMainCanvas.SetActive(false);
        focusCanvas.SetActive(false);
        configCanvas.SetActive(true);
        isConfig = true;
        //gameBGM.Stop();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    //設定画面からメイン画面に戻る
    public void ReturnFromConfig()
    {
        if (playerHeliController.isMovie == false)
        {
            gameMainCanvas.SetActive(true);
        }
            Time.timeScale = 1f;
        configCanvas.SetActive(false);
        isConfig = false;
        //gameBGM.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    //映像を再生時の処理
    IEnumerator MovieStart()
    {
        gameMainCanvas.SetActive(false);
        focusCanvas.SetActive(false);
        mainCamera.SetActive(false);
        ManagerCamera.SetActive(true);
        playerHeliController.isMovie = true;
        gameBGM.Play();
        isMovieNow = true;
        yield return new WaitForSeconds(movieTime);
        gameMainCanvas.SetActive(true);
        ManagerCamera.SetActive(false);
        mainCamera.SetActive(true);
        playerHeliController.isMovie = false;
        configCanvas.SetActive(false);
        isMovieNow = false;

    }

}
