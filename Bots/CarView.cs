using UnityEngine;

public class CarView : MonoBehaviour
{
    [SerializeField] private GameObject gabaritLights;
    [SerializeField] private GameObject stopLights;
    [SerializeField] private GameObject headlights;
    [SerializeField] private GameObject salonLight;

    private void Start()
    {
        SetNightMode(NightState.isNight);
        NightState.SimpleNightEvent.AddListener(SetNightMode);
    }

    private void SetNightMode(bool val)
    {
        EnableSalonLight(val);
    }

    public void EnableSalonLight(bool enable)
    {
        if (salonLight != null) salonLight.SetActive(enable);
    }

    public void EnableGabarits(bool enable)
    {

    }

    public void EnablePovorotniks(bool enable)
    {

    }

    public void EnableStops(bool enable)
    {

    }

    public void EnableHeadlights(bool enable)
    {

    }


}
