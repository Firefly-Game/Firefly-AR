using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    private Light myLight;
    public ARCameraManager cameraManager;
    ILogger logger = Debug.unityLogger;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();

        // Check availablity
    }

    void OnEnable()
    {
        cameraManager.frameReceived += OnChange;
    }
    void OnDisable()

    {
        cameraManager.frameReceived -= OnChange;
    }

    void OnChange(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            myLight.intensity = args.lightEstimation.averageBrightness.Value;
        }
        else
        {
            // Average brightness not available, see if manual calculation is possible
            if(args.lightEstimation.mainLightColor.HasValue)
            {
                logger.Log("Manual calculation");
                myLight.color = args.lightEstimation.mainLightColor.Value;
                float av_brightness = 0.2126f * myLight.color.r + 0.7152f * myLight.color.g + 0.0722f * myLight.color.b;
                myLight.intensity = av_brightness;
            }
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            myLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }
        else
        {
            logger.Log("No color value");
        }

        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            myLight.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
        }
        else
        {
            logger.Log("No main light direction value");
        }

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientProbe = args.lightEstimation.ambientSphericalHarmonics.Value;
        }
        else
        {
            logger.Log("No ambient spherical harmonics value");
        }
    }
}
