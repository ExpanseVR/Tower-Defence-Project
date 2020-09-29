using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimerUI : MonoBehaviour
{
    [SerializeField]
    Text _countDownText;

    public IEnumerator CountDown (int StartNumber)
    {
        _countDownText.enabled = true;
        int _currentCount = StartNumber;
        while (_currentCount > 0)
        {
            _countDownText.text = _currentCount.ToString();
            yield return new WaitForSeconds(1f);
            _currentCount -= 1;
        }

        _countDownText.fontSize = 130;
        _countDownText.text = "DEFEND THE CITY!!!";
        yield return new WaitForSeconds(2f);
        _countDownText.enabled = false;
    }
}
