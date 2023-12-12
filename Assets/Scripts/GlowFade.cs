using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public enum GlowState
{
    Fade_out, Fade_in, Idle
}

public class GlowFade : MonoBehaviour
{
   
    public Color usualColor;
    public Color emissionColor;
    public float transitionSpeed;
    private static float minSpeed = 1.4f; // Currently the same bc looks weird with different
    private static float maxSpeed = 1.4f;
    private float time = 0.0f;
    private GlowState state = GlowState.Fade_out;
    private bool shouldSwitchSpeed = true;
    


    // Glow starts glowing on the way to fade out
    void Start()
    {
        usualColor = GetComponent<Renderer>().material.color;
        emissionColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        turnOffGlow();
        setRandomTransitionSpeed();

    }

    void Update()
    {
        time += Time.deltaTime;

        if (shouldSwitchSpeed)
        {
            setRandomTransitionSpeed();
            shouldSwitchSpeed = false;
        }


        switch(state)
        {
            case GlowState.Fade_out:
                turnOffGlow();
                break;
            case GlowState.Fade_in:
                turnOnGlow();
                break;
            default: // If idle, do nothing
                break;

        }

       
        



        
    }


    void turnOnGlow()
    {

        // Turn on emission if not already on
        if (!GetComponent<Renderer>().material.IsKeywordEnabled("_EMISSION"))
        {
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }

        float lerpValue = Mathf.PingPong(time * transitionSpeed, 1f);
        Color lerpedColor = Color.Lerp(usualColor, emissionColor, lerpValue);
        this.GetComponent<Renderer>().material.SetColor("_EmissionColor", lerpedColor);
        
        if (lerpValue > 0.99f)
        {
            state = GlowState.Fade_out;
            time = 0;
            shouldSwitchSpeed = true;
        }

    }

    void turnOffGlow()
    {


        // Turn off emission if not already off
        if (GetComponent<Renderer>().material.IsKeywordEnabled("_EMISSION")) {
            GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
        
        float lerpValue = Mathf.PingPong(time * transitionSpeed, 1f);
        Color lerpedColor = Color.Lerp(emissionColor, usualColor, lerpValue);
        this.GetComponent<Renderer>().material.color = lerpedColor;


        if (lerpValue > 0.99f)
        {
            state = GlowState.Fade_out;
            shouldSwitchSpeed = true;
        }
    }

    void setRandomTransitionSpeed()
    {
        Debug.Log("Time: " + time);
        System.Random rnd = new System.Random();
        transitionSpeed = (float) rnd.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
    }





}
