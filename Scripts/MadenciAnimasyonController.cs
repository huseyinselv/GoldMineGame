using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadenciAnimasyonController : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public void durmaAnimasyonu()
    {
        anim.Play("durmaAnimasyonu");
    }
    public void ipSarmaAnimasyonu()
    {
        anim.Play("ipSarma");
    }

}
