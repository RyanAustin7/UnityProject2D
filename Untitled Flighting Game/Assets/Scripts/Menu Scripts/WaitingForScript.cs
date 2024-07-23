using UnityEngine;
using TMPro;
using System.Collections;

public class WaitingForScript : MonoBehaviour
{
    [Header("Blink Settings")]
    [SerializeField] private float blinkOnDuration = 0.5f; // Duration the text is visible
    [SerializeField] private float blinkOffDuration = 0.2f; // Duration the text is invisible

    [Header("TextMeshPro Objects")]
    [SerializeField] private TextMeshProUGUI waitForP1;
    [SerializeField] private TextMeshProUGUI waitForP2;
    [Header("Ready TMP Objects")]
    [SerializeField] private TextMeshProUGUI staticReadyP1;
    [SerializeField] private TextMeshProUGUI staticReadyP2;

    private Coroutine p1BlinkCoroutine;
    private Coroutine p2BlinkCoroutine;

    void Start()
    {
        // Ensure TMP objects are initially inactive
        if (waitForP1 != null)
            waitForP1.gameObject.SetActive(false);

        if (waitForP2 != null)
            waitForP2.gameObject.SetActive(false);
    }

    void Update()
    {
        bool isLeftShiftHeld = Input.GetKey(KeyCode.LeftShift);
        bool isRightShiftHeld = Input.GetKey(KeyCode.RightShift);

        // Handle TMP visibility and blinking for Left Shift
        if (isLeftShiftHeld)
        {
            if (waitForP1 != null)
            {
                if (p1BlinkCoroutine == null)
                {
                    waitForP1.gameObject.SetActive(true); // Ensure TMP is visible
                    p1BlinkCoroutine = StartCoroutine(BlinkText(waitForP1));
                }
            }
        }
        else
        {
            if (waitForP1 != null && p1BlinkCoroutine != null)
            {
                StopCoroutine(p1BlinkCoroutine);
                p1BlinkCoroutine = null;
                waitForP1.gameObject.SetActive(false); // Disable TMP when Left Shift is released
            }
        }

        // Handle TMP visibility and blinking for Right Shift
        if (isRightShiftHeld)
        {
            if (waitForP2 != null)
            {
                if (p2BlinkCoroutine == null)
                {
                    waitForP2.gameObject.SetActive(true); // Ensure TMP is visible
                    p2BlinkCoroutine = StartCoroutine(BlinkText(waitForP2));
                }
            }
        }
        else
        {
            if (waitForP2 != null && p2BlinkCoroutine != null)
            {
                StopCoroutine(p2BlinkCoroutine);
                p2BlinkCoroutine = null;
                waitForP2.gameObject.SetActive(false); // Disable TMP when Right Shift is released
            }
        }

        // Handle both shift keys pressed
        if (isLeftShiftHeld && isRightShiftHeld)
        {
            if (waitForP1 != null) waitForP1.gameObject.SetActive(false);
            if (waitForP2 != null) waitForP2.gameObject.SetActive(false);
        }


        //For Static Ready Objects:
        if (isLeftShiftHeld)
        {
            staticReadyP1.gameObject.SetActive(true);
        }
        else
        {
            staticReadyP1.gameObject.SetActive(false);
        }
        if (isRightShiftHeld)
        {
            staticReadyP2.gameObject.SetActive(true);
        }
        else
        {
            staticReadyP2.gameObject.SetActive(false);
        }
    }

    private IEnumerator BlinkText(TextMeshProUGUI tmp)
    {
        while (true)
        {
            tmp.enabled = true; // Text is visible
            yield return new WaitForSeconds(blinkOnDuration);
            tmp.enabled = false; // Text is invisible
            yield return new WaitForSeconds(blinkOffDuration);
        }
    }
}
