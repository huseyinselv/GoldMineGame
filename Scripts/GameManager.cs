using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform[] elmasYerleri;

    [SerializeField]
    List<GameObject> elmaslarPrefabs;

    [SerializeField]
    Transform elmaslariTutanTransform;

    [SerializeField]
    private TextMeshProUGUI soruText;

    [SerializeField]
    List<int> sayilarListesi;

    [SerializeField]
    private Text geriSaymaTxt;

    [SerializeField]
    private Image sliderImg;

    [SerializeField]
    Text puanTxt;

    [SerializeField]
    GameObject yildizImg;

    [SerializeField]
    GameObject birinciYildizImg, ikinciYildizImg, ucuncuYildizImg;

   

    int kalanHak = 3;

    int sorulacakSayi;

    List<int> dogruCevaplar = new List<int>();
    List<int> yanlisCevaplar = new List<int>();
    List<int> bolenlerListesi = new List<int>();

    int geriSayac = 60;

    float skor = 25f;
    float toplamSkor = 0;

    SesManager sesManager;
    public bool oyunBittimi;

    private void Awake()
    {
        sesManager = Object.FindObjectOfType<SesManager>();
    }

    private void Start()
    {
        oyunBittimi = false;
        MadenciyeSoruSor();

        bolenleriBul();
        ElmaslariYerlestir();


        geriSaymaTxt.text = geriSayac.ToString();

        StartCoroutine("GeriSaymaRouitine");

        puanTxt.text = toplamSkor.ToString();
        haklariGoster();
        
    }


    void ElmaslariYerlestir()
    {
        elmaslarPrefabs = elmaslarPrefabs.OrderBy(i => Random.value).ToList();
        
        for (int i = 0; i < elmasYerleri.Length; i++)
        {
            GameObject elmas = Instantiate(elmaslarPrefabs[i]) as GameObject;
            elmas.GetComponentInChildren<TextMeshProUGUI>().text = bolenlerListesi[i].ToString();
            elmas.transform.parent = elmaslariTutanTransform;
            elmas.transform.position = elmasYerleri[i].position;
        }
    }

    
    void MadenciyeSoruSor()
    {
        sayilarListesi = sayilarListesi.OrderBy(i => Random.value).ToList();

        sorulacakSayi = sayilarListesi[0];

        soruText.text = sayilarListesi[0] + " Sayýsýnýn bölenlerini bul ";
    }

    void bolenleriBul ()
    {
        for (int i = 2; i < sorulacakSayi; i++)
        {
            if(sorulacakSayi%i== 0)
            {
                dogruCevaplar.Add(i);
            }
            else
            {
                yanlisCevaplar.Add(i);
            }
        }
        dogruCevaplar = dogruCevaplar.OrderBy(i => Random.value).ToList();
        yanlisCevaplar = yanlisCevaplar.OrderBy(i => Random.value).ToList();

        for (int i = 0; i < 4; i++)
        {
            bolenlerListesi.Add(dogruCevaplar[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            bolenlerListesi.Add(yanlisCevaplar[i]);
        }

        bolenlerListesi = bolenlerListesi.OrderBy(i => Random.value).ToList();

    }
    public void SonucuKontrolEt(int gelenDeger)
    {
        if(dogruCevaplar.Contains(gelenDeger))
        {
            PuaniGoster();
            yildiziCikar();

        } else
        {
            kalanHak--;
            haklariGoster();
        }
    }


    IEnumerator GeriSaymaRouitine()
    {
        yield return new WaitForSeconds(1f);

        geriSayac--;
        geriSaymaTxt.text = geriSayac.ToString();
        StartCoroutine("GeriSaymaRouitine");

        if(geriSayac < 10)
        {
            geriSaymaTxt.text = "0"+ geriSayac.ToString();
            sesManager.SureBitiyorSesiCikar(true);
        }

        if(geriSayac <= 0)
        {
            StopCoroutine("GeriSaymaRouitine");
            geriSaymaTxt.text = " ";
            sesManager.SureBitiyorSesiCikar(false);
            oyunBittimi = true;
            StartCoroutine("oyunBittiRoutine");
            //oyun bitti
        }

    }

    public void PuaniGoster()
    {
        toplamSkor += skor;
        sliderImg.fillAmount = (float)toplamSkor / 100f;

        puanTxt.text = toplamSkor.ToString();

        if(toplamSkor >= 100 )
        {
            StopCoroutine("GeriSaymaRouitine");
            geriSaymaTxt.text = " ";
            oyunBittimi = true;
            StartCoroutine("oyunBittiRoutine");
            //oyun bitti
        }
    }

    private void yildiziCikar()
    {
        yildizImg.GetComponent<RectTransform>().DOScale(1 , .05f) .SetEase(Ease.OutBack);
        yildizImg.GetComponent<CanvasGroup>().DOFade(1, .05F);

        Invoke("yildizGonder", 1f);
    }

    void yildizGonder()
    {
        yildizImg.GetComponent<RectTransform>().DOScale(0, .05f).SetEase(Ease.InBack);
        yildizImg.GetComponent<CanvasGroup>().DOFade(0, .05F);
    }

    void haklariGoster()
    {
        if (kalanHak == 3)
        {
            birinciYildizImg.SetActive(true);
            ikinciYildizImg.SetActive(true);
            ucuncuYildizImg.SetActive(true);
        } else if (kalanHak == 2)
        {
            birinciYildizImg.SetActive(true);
            ikinciYildizImg.SetActive(true);
            ucuncuYildizImg.SetActive(false);

        } else if(kalanHak == 1)
        {
            birinciYildizImg.SetActive(true);
            ikinciYildizImg.SetActive(false);
            ucuncuYildizImg.SetActive(false);
        } else if (kalanHak == 0)
        {
            StartCoroutine("oyunBittiRoutine");
            oyunBittimi = true;

            birinciYildizImg.SetActive(false);
            ikinciYildizImg.SetActive(false);
            ucuncuYildizImg.SetActive(false);
            
        }

        
    }

  IEnumerator oyunBittiRoutine()
    {
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}
