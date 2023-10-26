using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Located on the Time text UI
public class TimeScript : MonoBehaviour
{
    [SerializeField] float currentTime = 60.00f;
    [SerializeField] PauseControl pauseControl;
    [SerializeField] Color defaultColor;
    [SerializeField] Color SuccessColor;
    [SerializeField] Color FailColor;

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

    public float getTime()
    {
        return currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        string formattedTime = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
        TimeUI.text = formattedTime;

        if (currentTime <= 0)
        {
            //the end screen
            Debug.Log(currentTime);
            pauseControl.loseGame();
        }
    }

    /// <summary>
    /// Adds time to the timer
    /// </summary>
    /// <param name="time"></param>
    public void loseTime(float timeLost)
    {
        currentTime -= timeLost;
        StartCoroutine(turnRed());
        
    }

    public void addTime(float timeLost)
    {
        currentTime += timeLost;
        StartCoroutine(turnGreen());
    }

    private IEnumerator turnGreen()
    {
        TimeUI.color = SuccessColor;
        yield return new WaitForSeconds(0.2f);
        TimeUI.color = defaultColor;

    }

    private IEnumerator turnRed()
    {
        TimeUI.color = FailColor;
        yield return new WaitForSeconds(0.2f);
        TimeUI.color = defaultColor;

    }





}
