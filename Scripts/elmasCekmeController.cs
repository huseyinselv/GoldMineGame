using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class elmasCekmeController : MonoBehaviour
{
    [SerializeField]
    Transform elmasTutucuTransform;

    bool elmasTutuldumu;

    KancaHareketController kancaHareketController;

    MadenciAnimasyonController madenciAnimasyonController;
    GameManager gameManager;
    SesManager sesManager;

    private void Awake()
    {
        kancaHareketController = Object.FindObjectOfType<KancaHareketController>();
        madenciAnimasyonController = GetComponentInParent<MadenciAnimasyonController>();
        gameManager = Object.FindObjectOfType<GameManager>();
        sesManager = Object.FindObjectOfType<SesManager>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "buyukElmas" || target.tag == "ortaElmas" || target.tag == "kucukElmas")
        {
            elmasTutuldumu = true;

            target.transform.parent = elmasTutucuTransform;
            target.transform.position = elmasTutucuTransform.position;

            kancaHareketController.KancaYukariDonsun();

            madenciAnimasyonController.ipSarmaAnimasyonu();


            if(target.tag == "buyukElmas" || target.tag == "ortaElmas")
            {
                sesManager.BuyukElmasSesiCikar();
            } else
            {
                sesManager.KucukElmasSesiCikar();
            }

            sesManager.ElmasCekmeSesiCikar(true);

        }

        if(target.tag == "gelenElmas")
        {
            if(elmasTutuldumu)
            {
                elmasTutuldumu = false;
                Transform objChild = elmasTutucuTransform.GetChild(0);

                int elmasDegeri = int.Parse(objChild.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text);
                gameManager.SonucuKontrolEt(elmasDegeri);

                objChild.parent = null;
                objChild.gameObject.SetActive(false);

                madenciAnimasyonController.durmaAnimasyonu();

                sesManager.ElmasCekmeSesiCikar(false);

            }
        }
    }




}
