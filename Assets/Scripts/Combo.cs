using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Combo : MonoBehaviour
{
    private Text text;
    [SerializeField] BallScript ballScript;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ballScript.combo + " X";
    }
}
