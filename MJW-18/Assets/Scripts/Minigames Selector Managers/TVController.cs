using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVController : MonoBehaviour
{
    [SerializeField] private RawImage panel;
    [SerializeField] private RectTransform TVPanel;
    [SerializeField] private float hidenPosition, shownPosition;
    [SerializeField][Range(0, 30)] private float speed;
    private float actualPosition;
    private bool isShown;

    // Start is called before the first frame update
    void Start()
    {
        isShown = false;
        actualPosition = hidenPosition;
    }

    // Update is called once per frame
    void Update()
    {
        TVPanel.localPosition = new Vector3(0, Mathf.Lerp(TVPanel.localPosition.y, actualPosition, Time.deltaTime * speed), 0);
    }

    public void toggleTV(RenderTexture render = null) {
        if (isShown)
        {
            actualPosition = hidenPosition;
        } else {
            actualPosition = shownPosition;
        }

        if (render != null) {
            panel.texture = render;
        }

        isShown = !isShown;
    }
}
