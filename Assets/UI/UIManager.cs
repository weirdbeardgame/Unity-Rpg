using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool isOpen;
    public GameObject pScreen;
    public List<GameObject> menuPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Open(int index)
    {
        if (!isOpen)
        {
            pScreen = Instantiate(menuPrefabs[0]);
            isOpen = true;
        }
        if (isOpen == true || pScreen != menuPrefabs[index])
        {
            pScreen = Instantiate(menuPrefabs[index]);
        }
    }

    public void Close()
    {
        if (pScreen && isOpen)
        {
            pScreen.SetActive(false);
            //Destroy(pScreen);
            isOpen = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
