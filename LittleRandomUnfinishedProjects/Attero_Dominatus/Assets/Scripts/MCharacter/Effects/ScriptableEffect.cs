using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEffect : ScriptableObject
{
    public string EffectName;

    public List<Sprite> IdleSprites = new List<Sprite>();
    public List<Sprite> WalkingSprites = new List<Sprite>();

}
