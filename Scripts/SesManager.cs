using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SesManager : MonoBehaviour
{
    [SerializeField] 
    AudioSource buyukElmas_FX, kucukElmas_FX, kanca_FX, elmascek_FX, oyuncugulme_FX, oyunsonu_FX, sureBitiyor_FX;

    private void Start()
    {
        
    }

    public void BuyukElmasSesiCikar()
    {
        buyukElmas_FX.Play();
    }
    public void KucukElmasSesiCikar()
    {
        kucukElmas_FX.Play();
    }
    public void KancaSesiCikar(bool sesCiksinmi)
    {
        if(sesCiksinmi)
        {
            if(!kanca_FX.isPlaying)
            {
                kanca_FX.Play();
            }
        } else
        {
            if(kanca_FX.isPlaying)
            {
                kanca_FX.Stop();
            }
        }
    }

    public void ElmasCekmeSesiCikar(bool sesCiksinmi)
    {
        if (sesCiksinmi)
        {
            if (!elmascek_FX.isPlaying)
            {
                elmascek_FX.Play();
            }
        }
        else
        {
            if (elmascek_FX.isPlaying)
            {
                elmascek_FX.Stop();
            }
        }
    }

    public void SureBitiyorSesiCikar(bool sesCiksinmi)
    {
        if (sesCiksinmi)
        {
            if (!sureBitiyor_FX.isPlaying)
            {
                sureBitiyor_FX.Play();
            }
        }
        else
        {
            if (sureBitiyor_FX.isPlaying)
            {
                sureBitiyor_FX.Stop();
            }
        }
    }

    public void oyunBittiSesiCikar ()
    {
        oyunsonu_FX.Play();
    }

    public void oyuncuGulmeSesiCikar()
    {
        oyuncugulme_FX.Play();
    }

}

