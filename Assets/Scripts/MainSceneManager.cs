using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject globalVolume;
    public float weightChangeSpeed = 0.25f;
    private Volume myVolume;
    static float t = 0.0f;
    private bool isWeekUp = true;

    void Start()
    {
        myVolume = globalVolume.GetComponent<Volume>();
        myVolume.weight = 1;
        isWeekUp = true;
        t = 0.9f;
    }

    void Update()
    {
        if (t < 1.0f && !isWeekUp)
        {
            t += Time.deltaTime * weightChangeSpeed;
            myVolume.weight = Mathf.Lerp(0, 1f, t);
        }
        else if (t > 0.06f && isWeekUp)
        {
            t -= Time.deltaTime * weightChangeSpeed;
            myVolume.weight = Mathf.Lerp(0, 1f, t);
        }

        if (t > 1)
        {
            Invoke(nameof(GoNext), 1f);
        }
    }

    public void NextScene()
    {
        isWeekUp = false;
    }

    private void GoNext()
    {
        SceneManager.LoadScene("1_Game", LoadSceneMode.Single);
    }
}
