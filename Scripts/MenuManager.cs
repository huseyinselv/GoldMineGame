using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    GameObject baslaYazi, baslBtn, nasilOynanirBtn;
    
    void Start()
    {
        StartCoroutine(BaslangicRoutine());
    }

   

    IEnumerator BaslangicRoutine()
    {
        baslaYazi.GetComponent<RectTransform>().DOScale(1, 0.4f).SetEase(Ease.OutBack);
        baslaYazi.GetComponent<CanvasGroup>().DOFade(1, 0.4f);

        yield return new WaitForSeconds(0.5f);

        baslBtn.GetComponent<RectTransform>().DOScale(1, 0.4f).SetEase(Ease.OutBack);
        baslBtn.GetComponent<CanvasGroup>().DOFade(1, 0.4f);

        yield return new WaitForSeconds(0.5f);

        nasilOynanirBtn.GetComponent<RectTransform>().DOScale(1, 0.4f).SetEase(Ease.OutBack);
        nasilOynanirBtn.GetComponent<CanvasGroup>().DOFade(1, 0.4f);

        yield return new WaitForSeconds(0.5f);



    }

    public void OyunaBasla()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
