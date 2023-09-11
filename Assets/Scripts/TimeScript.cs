using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{

    [SerializeField] float currentTime = 0;
    [SerializeField] Text TimeUI;

    // Start is called before the first frame update
    void Start()
    {
        TimeUI = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        string formattedTime = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
        TimeUI.text = formattedTime;
    }

    public void addTime(float time)
    {
        currentTime += time;
    }
}
