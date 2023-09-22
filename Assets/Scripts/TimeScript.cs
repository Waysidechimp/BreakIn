using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Located on the Time text UI
public class TimeScript : MonoBehaviour
{
    //[SerializeField] GameObject camera;
    [SerializeField] float currentTime = 0;
    private Text TimeUI;

    // Start is called before the first frame update
    void Start()
    {
        TimeUI = gameObject.GetComponent<Text>();
    }

    public string getCurrentTime()
    {
        return TimeUI.text;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        string formattedTime = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
        TimeUI.text = formattedTime;
    }

    /// <summary>
    /// Adds time to the timer
    /// </summary>
    /// <param name="time"></param>
    public void addTime(float time)
    {
        currentTime += time;
    }
}
