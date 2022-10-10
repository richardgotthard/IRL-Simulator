using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
  // public bool AlarmState =

  public bool alarm;

  void Start()
    {
         alarm = false;
    }

    public void setAlarm(){

        alarm =! alarm;

       // Debug.Log(alarm);
    }

    
}
