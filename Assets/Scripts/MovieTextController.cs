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
        csvFile = Resources.Load("dialog") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(csvFile.text);
        textArray = new string[10];
        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        int i = 0;
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            textArray[i] = line; // , ��؂�Ń��X�g�ɒǉ�
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
