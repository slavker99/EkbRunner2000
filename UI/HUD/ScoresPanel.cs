using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresPanel : MonoBehaviour
{
    [SerializeField] private Text scoresText;
    [SerializeField] private Material ramkaMt;
    [SerializeField] private Material ramkaDefMt;
    [SerializeField] private Material ramkaActiveMt;

    public void SetScores(int scores)
    {
        StopAllCoroutines();
        scoresText.text = scores.ToString();
        if (this.isActiveAndEnabled) StartCoroutine(PanelAnimationCorutine());
    }

    private IEnumerator PanelAnimationCorutine()
    {
        float num = 0;
        while (num <= 1)
        {
            ramkaMt.color = Color.Lerp(ramkaDefMt.color, ramkaActiveMt.color, num);
            num = num + 0.25f;
            yield return new WaitForEndOfFrame();
        }
        while (num >= 0)
        {
            ramkaMt.color = Color.Lerp(ramkaDefMt.color, ramkaActiveMt.color, num);
            num = num - 0.25f;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
