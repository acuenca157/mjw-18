using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsController : MonoBehaviour
{
    [SerializeField]
    private RectTransform curtainLeft, curtainRight;
    [SerializeField]
    private float curtainLeftOpenedPosition, curtainLeftClosedPosition;
    [SerializeField]
    private float curtainRightOpenedPosition, curtainRightClosedPosition;
    private float curtainRightActualPosition, curtainLeftActualPosition;

    [SerializeField] [Range(0, 100f)]
    private float speed = 10.0f;

    private bool isOpened;

    [SerializeField]
    private EventReference sonidoTelon;


    private void Start()
    {
        curtainLeft.localPosition = new Vector3(curtainLeftOpenedPosition, 0, 0);
        curtainRight.localPosition = new Vector3(curtainRightOpenedPosition, 0, 0);
        curtainLeftActualPosition = curtainLeftOpenedPosition;
        curtainRightActualPosition = curtainRightOpenedPosition;
        isOpened = true;
    }

    // Update is called once per frame
    private void Update()
    {
        curtainLeft.localPosition = new Vector3(Mathf.Lerp(curtainLeft.localPosition.x, curtainLeftActualPosition, Time.deltaTime * speed), 0, 0);
        curtainRight.localPosition = new Vector3(Mathf.Lerp(curtainRight.localPosition.x, curtainRightActualPosition, Time.deltaTime * speed), 0, 0);
    }

    public void makeCurtainTransition()
    {
        FMODUnity.RuntimeManager.PlayOneShot(sonidoTelon);
        if (isOpened)
        {
            curtainLeftActualPosition = curtainLeftClosedPosition;
            curtainRightActualPosition = curtainRightClosedPosition;
        }
        else
        {
            curtainLeftActualPosition = curtainLeftOpenedPosition;
            curtainRightActualPosition = curtainRightOpenedPosition;
        }
        isOpened = !isOpened;
    }
}
