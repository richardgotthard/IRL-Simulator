using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    Text TextScoreUI;

    private float _stat;
    public float Stat{ 
    get { return _stat; } 
    set{
        _stat =value;
        }
        }
}
