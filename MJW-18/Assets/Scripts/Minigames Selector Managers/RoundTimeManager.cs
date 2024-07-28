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
    [SerializeField] private Image slider, hpSlider;
    private MinigamesController minigamesController;

    // HIJO PUTISMO SYSTEM
    public int maxHP = 1000;
    private int actualHP = 0;

    // Start is called before the first frame update
    void Start()
    {
        minigamesController = FindObjectOfType<MinigamesController>();
        slider.fillAmount = 1;
        hpSlider.fillAmount = 0;
        actualTime = totalTime;
        actualHP = 0;
        StartCoroutine(startCountDown());
    }

    private void Update()
    {
        slider.fillAmount = (float) actualTime / totalTime;
        hpSlider.fillAmount = (float) actualHP / maxHP;
    }

    public IEnumerator startCountDown() {
        while (actualTime > 0 || actualHP < maxHP) {
            if (!minigamesController.IsOnMinigame)
            {
                actualTime -= 1;
            }
            yield return new WaitForSeconds(removeSpeed / 100);
        }

        minigamesController.endRound(actualHP >= maxHP);
        yield return new WaitForEndOfFrame();
    }

    public void reset() {
        removeSpeed = removeSpeed * minigamesController.actualLvl;
        Start();
    }

    public void addTime(int amnt) {
        actualTime += amnt;
    }

    public void addHP(int amnt)
    {
        actualHP += amnt;
    }
}
