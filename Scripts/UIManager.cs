using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    
    public GameObject canvas1;
    public GameObject canvas2;

    public void Exit() {
        Application.Quit();
    }

    public void Next() {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }

    public void Back() {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }
}
