using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    private const float MAX_STAT = 100f;
    public float stat = MAX_STAT;
    private Image statBar;
    // Start is called before the first frame update
    void Start()
    {
        statBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        statBar.fillAmount = stat / MAX_STAT;
        
    }
}
