using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Transform target { get; internal set; }
    public float tamano { get; internal set; }

    public abstract void Init(Transform target, float tamano, float tiempo);
}
