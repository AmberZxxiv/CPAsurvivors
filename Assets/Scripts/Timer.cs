using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timetoBeat;
    [SerializeField] TextMeshProUGUI fasterLoser;
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
        timetoBeat.text = "Best survivor: " + PlayerPrefs.GetFloat("timetoBeat").ToString();
        fasterLoser.text = "Best loser: " + PlayerPrefs.GetFloat("fasterLoser").ToString();
        // si el contador es MENOR que el tiempo a vencer, actualiza
        if (timerTime < PlayerPrefs.GetFloat("timetoBeat")||!PlayerPrefs.HasKey("timetoBeat"))
        {
            PlayerPrefs.SetFloat("timetoBeat", timerTime);
        }
        // si el contador es MENOR que el anterior, actualiza
        if (timerTime < PlayerPrefs.GetFloat("fasterLoser") || !PlayerPrefs.HasKey("fasterLoser"))
        {
            PlayerPrefs.SetFloat("fasterLoser", timerTime);
        }
    }
}
