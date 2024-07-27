using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimeManager : MonoBehaviour
{
    public int totalTime = 1000;
    public float removeSpeed = 1;
    private int actualTime = 0;
    [SerializeField] private Image slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.fillAmount = 1;
        actualTime = totalTime;
        StartCoroutine(startCountDown());
    }

    private void Update()
    {
        slider.fillAmount = (float)actualTime / totalTime;
    }

    public IEnumerator startCountDown() {
        while (actualTime > 0) {
            actualTime -= 1;
            yield return new WaitForSeconds(removeSpeed / 100);
        }
        Debug.Log("HAS MORIDO");
        yield return new WaitForEndOfFrame();
    }
}
