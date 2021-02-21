using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Jam
{
    [RequireComponent(typeof(Button))]
    public class ButtonTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] private bool enabled = true;
        [SerializeField] private float transitionTime = 0.1f;
        private Color disabledColor;
        private Color highlightColor;
        private Color baseColor;
        private TextMeshProUGUI textObject;

        private Button buttonObject;

        [SerializeField]
        private AudioClip button_mouseover;


        private AudioSource mouseoverSource;

        public void Awake()
        {
            textObject = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            buttonObject = gameObject.GetComponent<Button>();
            baseColor = textObject.color;
            highlightColor = buttonObject.colors.highlightedColor;
            disabledColor = buttonObject.colors.disabledColor;
            mouseoverSource = Utils.AddAudioNoFalloff(gameObject, button_mouseover, false, false, 0.6f, 1);
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
            buttonObject.interactable = enabled;
            if (enabled)
            {
                textObject.color = baseColor;
            }
            else
            {
                StopAllCoroutines();
                textObject.color = disabledColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!enabled) return;

            StopAllCoroutines();
            StartCoroutine(UpdateColor(highlightColor));
            mouseoverSource.Play();
            Debug.Log("ENTERD");

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!enabled) return;

            StopAllCoroutines();
            StartCoroutine(UpdateColor(baseColor));

        }

        public IEnumerator UpdateColor(Color target)
        {
            float elapsedTime = 0;
            Color current = textObject.color;
            while (elapsedTime < transitionTime)
            {
                elapsedTime += Time.deltaTime;
                textObject.color = Color.Lerp(current, target, (elapsedTime / transitionTime));
                yield return null;
            }

        }


    }

}