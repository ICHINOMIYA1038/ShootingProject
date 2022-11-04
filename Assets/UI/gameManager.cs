using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public int currentScene;
    public const int TITLE_SCENE = 0;
    public const int MAIN_SCENE = 1;
    public const int CLEAR_SCENE = 2;
    public const int GAMEOVER_SCENE = 3;

    private void Start()
    {
        currentScene = TITLE_SCENE;
        
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
            CanvasGroup canvasGroup = gameOverCanvas.GetComponent<CanvasGroup>();
            StartCoroutine("setMouse");
            //Time.timeScale = 0.2f;

        }

        if (flag == CLEAR_SCENE)
        {
            gameClearCanvas.SetActive(true);
            gameMainCanvas.SetActive(false);
            CanvasGroup canvasGroup = gameClearCanvas.GetComponent<CanvasGroup>();
            StartCoroutine("setMouse");

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

}
