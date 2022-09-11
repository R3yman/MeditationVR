using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightCycle : MonoBehaviour
{

   double target;
   bool lower;

    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultiplier;

   
    // Start is called before the first frame update

    void Start()
    {
        
        
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        SampleTime();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //0.63-0.82
        print(target);
        if(time <= target){
            if(lower == true){
                SampleTime();
            }
            else{
                time += timeRate * Time.deltaTime;
            }
            
        }
        else if(time >= target){
            if(lower == false){
                SampleTime();
            }
            else{
                time -= timeRate * Time.deltaTime;
            }
            
        }
        
        /*
        // increment time
        time += timeRate * Time.deltaTime;
        */
        if(time >= 1.0f){
            time = 0.0f;
        }

        // light rotation
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon *4.0f;

        // light intensity
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);   

        // change colors
        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);

       



        // enable / disable the sun
        if(sun.intensity == 0 &&  sun.enabled){
            sun.enabled = false;
          
            
            
            
        }
    
        else if(sun.intensity > 0 && !sun.enabled)
            sun.enabled = true;
            

        // enable / disable the moon
        if(moon.intensity == 0 && moon.enabled)
            moon.enabled = false;
        else if(moon.intensity > 0 && !moon.enabled)
            moon.enabled = true;
            
            
        // lighting and reflections intensity
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
    }

public double GetRandomNumber(double minimum, double maximum){ 
    System.Random random = new System.Random();
    return random.NextDouble() * (maximum - minimum) + minimum;
}

private void SampleTime(){
     //0.63-0.82

     /*
     do{
            target = GetRandomNumber(0.63, 0.82);
        }while(target == time);

        
        if(target < time){
            lower = true;
        }
        else{
            lower = false;
        }
     */
     do{
            double rand = GetRandomNumber(-0.05, 0.05);
            target = time + rand;
        }while(target > 0.82 || target < 0.7) ;

        
        if(target < time){
            lower = true;
        }
        else{
            lower = false;
        }

}

    
}
