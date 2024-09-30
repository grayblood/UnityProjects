
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
   


}
