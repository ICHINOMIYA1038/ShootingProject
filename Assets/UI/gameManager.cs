using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using HeneGames.Airplane;
public class gameManager : MonoBehaviour
{
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private int maxMoral;
    public int HP { get; set; }
    public int moral { get; set; }
    TextMeshProUGUI hpText;
    TextMeshProUGUI moralText;
    [SerializeField]
    GameObject hpGauge;
    [SerializeField]
    GameObject moralGauge;
    [SerializeField]
    RectTransform hpGaugeTransform;
    [SerializeField]
    RectTransform moralGaugeTransform;
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
    [SerializeField]
    AudioSource gameSE;
    [SerializeField]
    AudioSource gameBGM;
    [SerializeField]
    AudioClip gameOverSE;
    [SerializeField]
    AudioClip gameClearSE;
    [SerializeField]
    SimpleAirPlaneController simpleAirPlaneController;
    [SerializeField]
    float movieTime = 15f;
    public bool isMovieNow = false;
    public bool isConfig = false;

    public int currentScene;
    public const int TITLE_SCENE = 0;
    public const int MAIN_SCENE = 1;
    public const int CLEAR_SCENE = 2;
    public const int GAMEOVER_SCENE = 3;

    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject ManagerCamera;

    private void Start()
    {
        configCanvas.SetActive(false);
        isConfig = false;
        currentScene = MAIN_SCENE;
        StartCoroutine("MovieStart");
        ;

    }
    void Awake()
    {
        HP = maxHP;
        moral = maxMoral;
        gameOverCanvas.SetActive(false);
        gameClearCanvas.SetActive(false);
        gameMainCanvas.SetActive(true);
        hpText = GameObject.Find("hpText").GetComponent<TextMeshProUGUI>();
        //moralText = GameObject.Find("moralText").GetComponent<TextMeshProUGUI>();

        hpGauge = GameObject.Find("hpGauge");
        //moralGauge = GameObject.Find("moralGauge");

        hpGaugeTransform = hpGauge.GetComponent<RectTransform>();
        //moralGaugeTransform = moralGauge.GetComponent<RectTransform>();

        changePanelSize(ref hpGaugeTransform, 200f);
        //changePanelSize(ref moralGaugeTransform, 200f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha0) && isConfig == false)
        {
            GoConfigPanel();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha0) && isConfig == true)
        {
            ReturnFromConfig();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void updateParam(string category, int damage)
    {
        if (category.Equals("HP"))
        {
            HP -= damage;
            float size = (float)HP / maxHP * 200f;
            changePanelSize(ref hpGaugeTransform, size);
        }
        if (category.Equals("Moral"))
        {
            moral -= damage;
            float size = (float)moral / maxMoral * 200f;
            changePanelSize(ref moralGaugeTransform, size);
        }
    }

    void changePanelSize(ref RectTransform rectTransform,float size)
    {
        Vector2 rectSize = rectTransform.sizeDelta;
        rectSize.x = size;
        rectTransform.sizeDelta = rectSize;
    }

    public void sceneChange(int flag)
    {
        if(flag==0){
            SceneManager.LoadScene("Title", LoadSceneMode.Additive);
        }
        if(flag==1)
        {
            SceneManager.LoadScene("main", LoadSceneMode.Additive);
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

        /*
        if (SceneManager.GetActiveScene().name.Equals("01"))
        {
            SceneManager.LoadScene("02", LoadSceneMode.Additive);
        }
        */
    }

    IEnumerator setMouse()
    {
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    public int getCurrentScene()
    {
        return currentScene;
    }

    public void GoConfigPanel()
    {
        Debug.Log("go");
        Time.timeScale = 0f;
        gameMainCanvas.SetActive(false);
        focusCanvas.SetActive(false);
        configCanvas.SetActive(true);
        isConfig = true;
        gameBGM.Stop();

    }

    public void ReturnFromConfig()
    {
        Debug.Log("return");
        if (simpleAirPlaneController.isMovie == false)
        {
            gameMainCanvas.SetActive(true);
        }
            Time.timeScale = 1f;
        configCanvas.SetActive(false);
        isConfig = false;
        gameBGM.Play();

    }

    IEnumerator MovieStart()
    {
        gameMainCanvas.SetActive(false);
        focusCanvas.SetActive(false);
        mainCamera.SetActive(false);
        ManagerCamera.SetActive(true);
        simpleAirPlaneController.isMovie = true;
        gameBGM.Play();
        isMovieNow = true;
        yield return new WaitForSeconds(movieTime);
        gameMainCanvas.SetActive(true);
        ManagerCamera.SetActive(false);
        mainCamera.SetActive(true);
        simpleAirPlaneController.isMovie = false;
        configCanvas.SetActive(false);
        isMovieNow = false;

    }

}
