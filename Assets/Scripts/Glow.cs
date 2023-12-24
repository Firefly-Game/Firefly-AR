using UnityEngine;

// Starts with pause, then glows for duration and repeats
public class Glow : MonoBehaviour
{
    private double glowTime = 2.0f;
    private double pauseTime = 1.0f;
    private bool isGlowing = false;
    private double time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION"); ; // Disable glow
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log("Time: " + time);

        if (time > pauseTime) {
            Debug.Log("Time > pausetime");
            if (time < pauseTime + glowTime)
            {
                Debug.Log("Should turn on light");
                turnOnGlow();
            }
            else
            {
                Debug.Log("Should turn off light and set time=0");
                time = 0;
                turnOffGlow();
            }
        } 
    }
        
    private void turnOnGlow() {
        Debug.Log("Inside turn on glow " + isGlowing);

        if (!isGlowing)
        {
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            isGlowing = true;
            Debug.Log("Light should have been turned on");
        } 
    }

    private void turnOffGlow()
    {
        Debug.Log("Inside turn off glow " + isGlowing);
        if (isGlowing)
        {
            GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            isGlowing = false;
            Debug.Log("Light should have been turned off");
        }
    }
}
