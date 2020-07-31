using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{

    public Button exit;

    // Start is called before the first frame update
    void Start()
    {
        exit.GetComponent<Button>();
    }

    void nowQuit()
    {
    }


    // Update is called once per frame
    void Update()
    {
        exit.onClick.AddListener(() => {nowQuit(); });
    }
}
