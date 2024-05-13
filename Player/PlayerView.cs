using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  ласс PlayerView
/// отвечает за внешний вид игрока (модель, анимации, звук)
/// </summary>

public class PlayerView : MonoBehaviour
{
    public PlayerTuningView TuningView;
    [SerializeField] private PlayerController controller;
    [Header("«вуки")]
    [SerializeField] private AudioSource soundCrash;
    [SerializeField] private AudioSource soundEngine;
    [SerializeField] private AudioSource scoreBonuceSound;
    [Header("јнимации")]
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private Animator bodyLocalAnimator;
    [SerializeField] private List<Transform> wheelsTransform;
    [SerializeField] private Transform body;
    [Header("‘ары")]
    [SerializeField] private GameObject stopLights;
    [SerializeField] private GameObject povorotnikiLight;
    [Header("Ёффект получени€ бонуса")]
    [SerializeField] private Transform bonuceLight;

    private float lastSpeed = 1;
    private float curSpeed = 1;

    private void Awake()
    {
        bonuceLight.gameObject.SetActive(false);
    }

    private void Update()
    {
        lastSpeed = curSpeed;
        curSpeed = controller.PlayerMovement.GetCurrentSpeed();
        if (curSpeed < lastSpeed)
        {
            OnStopLights(true);
        }
        else
            OnStopLights(false);
    }

    public void SetBodyXRotation(float val)
    {
        body.transform.localRotation = Quaternion.Euler(- val*2, body.transform.localRotation.y, body.transform.localRotation.z);
    }

    public void CrashState(Direction dir)
    {
        soundCrash.Play();
        if (dir == Direction.Left)
            bodyAnimator.Play("LeftCrash", 0);
        else if (dir == Direction.Right)
            bodyAnimator.Play("RightCrash", 0);
        StartCoroutine(PororotnikiCrashCorutine());
    }

    private IEnumerator PororotnikiCrashCorutine()
    {
        povorotnikiLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        povorotnikiLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        povorotnikiLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        povorotnikiLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        povorotnikiLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        povorotnikiLight.SetActive(false);
        yield return null;
    }

    public void ChangingLaneState(Direction dir)
    {
        if (dir == Direction.Left)
            bodyLocalAnimator.Play("ChangeLaneLeft", 0);
        else if (dir == Direction.Right)
            bodyLocalAnimator.Play("ChangeLaneRight", 0);
    }

    public void BonuceState()
    {
        scoreBonuceSound.Play();
        StopCoroutine(GetBonuceCorutine());
        bonuceLight.gameObject.SetActive(true);

        bonuceLight.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        StartCoroutine(GetBonuceCorutine());
    }

    private IEnumerator GetBonuceCorutine()
    {
        int num = 0;
        while (num < 5)
        {
            bonuceLight.localScale = bonuceLight.localScale * 1.1f;
            num++;
            yield return new WaitForEndOfFrame();
        }
        bonuceLight.gameObject.SetActive(false);
        bonuceLight.localScale = Vector3.one;
        yield return null;
    }

    public void GameOverState()
    {
        StartCoroutine(PororotnikiCrashCorutine());
    }

    public void OnMainHeadLights()
    {

    }

    public void OnStopLights(bool val)
    {
        stopLights.SetActive(val);
    }

    public void OnGabaritLights()
    {

    }

    public void RotateWheelsAnimation()
    {
        foreach (var wh in wheelsTransform)
            wh.Rotate(new Vector3(0,0, 700 * Time.deltaTime));
            //wh.rotation = Quaternion.Euler(wh.rotation.eulerAngles.x, wh.rotation.eulerAngles.y, wh.rotation.eulerAngles.z + 700 * Time.deltaTime);
    }
}
