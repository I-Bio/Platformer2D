using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeStealerView : MonoBehaviour
{
    [SerializeField] private LifeStealer _lifeStealer;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _minValue;

    private Image _image;

    private void OnEnable()
    {
        _image = GetComponent<Image>();

        _lifeStealer.Started += ToEmpty;
        _lifeStealer.NeededCoolDown += ToFill;
    }

    private void OnDisable()
    {
        _lifeStealer.Started -= ToEmpty;
        _lifeStealer.NeededCoolDown -= ToFill;
    }

    private void ToFill()
    {
        StartCoroutine(Filling());
    }

    private void ToEmpty()
    {
        _image.fillAmount = _minValue;
    }

    private IEnumerator Filling()
    {
        float elapsed = 0;
        float duration = _lifeStealer.CoolDown;
        
        while (elapsed < duration)
        {
            _image.fillAmount = Mathf.Lerp(_minValue, _maxValue, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
