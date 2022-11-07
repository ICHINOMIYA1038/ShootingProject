using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class MovieTextController : MonoBehaviour
{
    TextMeshProUGUI movieText;
    [SerializeField]
    string[] textArray;
    TextAsset csvFile;
    int textMaxIndex = 0;
    int textNow = 0;
    [SerializeField]
    bool auto;
    float time;
    [SerializeField]
    float changeTime;

    // Start is called before the first frame update
    void Start()
    {
        movieText = GetComponent<TextMeshProUGUI>();
        csvFile = Resources.Load("dialog") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);
        textArray = new string[10];
        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        int i = 0;
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            textArray[i] = line; // , 区切りでリストに追加
            i += 1;
        }
        textMaxIndex = i;
        movieText.text = textArray[textNow];
        
    }

    // Update is called once per frame
    void Update()
    {
        if(auto==true)
        {
            time += Time.deltaTime;
            if (time > changeTime)
            {
                time = 0;
                NextText();
            }
        }
    }

    public bool NextText()
    {
        if(textNow<textMaxIndex)
        {
            textNow += 1;
            movieText.text = textArray[textNow];
            return true;
        }
        else
        {
            return false;
        }
        
    }

}
