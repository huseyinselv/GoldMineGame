using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KancaHareketController : MonoBehaviour
{
    [SerializeField]
    float min_Z = -50f, max_Z = 50f;
    [SerializeField]
    float donusHizi = 5f;

    float donusAcisi;
    bool sagDonsunmu;
    bool donebilirmi;

    [SerializeField]
    float inmeHizi = 3f;
    float baslangicInmeHizi;

    [SerializeField]
    float min_Y = -2.5f;

    float baslangicY;

    bool asagiGitsinmi;

    ipRenderer ipRendererClass;
    GameManager gameManager;
    SesManager sesManager;

    private void Awake()
    {
       ipRendererClass = GetComponent<ipRenderer>();
       sesManager = Object.FindObjectOfType<SesManager>();
        gameManager = Object.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        baslangicY = transform.position.y;
        baslangicInmeHizi = inmeHizi;
        donebilirmi = true;
    }
    private void Update()
    {
        Rotate();
        MauseBasildimi();
        kancahareketi();
    }



    private void Rotate()
    {

        if (gameManager.oyunBittimi)
            return;

        if(!donebilirmi)
          return;
        if (sagDonsunmu)
        {
            donusAcisi += donusHizi * Time.deltaTime;
        }
        else
        {
            donusAcisi -= donusHizi * Time.deltaTime;
        }

        transform.rotation = Quaternion.AngleAxis(donusAcisi, Vector3.forward);


        if(donusAcisi >= max_Z)
        {

           sagDonsunmu = false;

        } else if (donusAcisi <= min_Z) 
        {
            sagDonsunmu =true;
        }
        
            
        

    }

    void MauseBasildimi()
    {
        if(Input.GetMouseButtonDown(0) && !gameManager.oyunBittimi)
        {
            if(donebilirmi)
            {
                sesManager.KancaSesiCikar(true);
                donebilirmi = false;
                asagiGitsinmi = true;
            }
        }
    }
    void kancahareketi()
    {
        if (gameManager.oyunBittimi)
            return;
        if (donebilirmi)
            return;
        if (!donebilirmi)
        {
            Vector3 temp = transform.position;
            if(asagiGitsinmi)
            {
                temp -= transform.up * Time.deltaTime * inmeHizi;
               
            }
            else
            {
                temp += transform.up * Time.deltaTime * inmeHizi;
            }

            transform.position = temp;

            if(temp.y <= min_Y)
            {
                asagiGitsinmi = false;
                sesManager.KancaSesiCikar(false);
            }

            if(temp.y >= baslangicY)
            {
                donebilirmi=true;
                inmeHizi = baslangicInmeHizi;

                ipRendererClass.ipGosteFNK(temp, false);
            }

            ipRendererClass.ipGosteFNK(temp, true);
        }
    }

    public void KancaYukariDonsun()
    {
        asagiGitsinmi = false;
    }

}
