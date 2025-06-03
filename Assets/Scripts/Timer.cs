using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timetoBeat;
    float timerTime; 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timerTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timerTime / 60);
        int seconds = Mathf.FloorToInt(timerTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void SetTimeToBeat()
    {
        if (timerTime < PlayerPrefs.GetFloat("timetoBeat")||!PlayerPrefs.HasKey("timetoBeat"))
        {
            PlayerPrefs.SetFloat("timetoBeat", timerTime);
        }

        float thisTime = PlayerPrefs.GetFloat("timetoBeat");
        timetoBeat.text = "Best survivor: " + thisTime.ToString();
        int minutes = Mathf.FloorToInt(thisTime / 60);
        int seconds = Mathf.FloorToInt(thisTime % 60);
        timetoBeat.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
