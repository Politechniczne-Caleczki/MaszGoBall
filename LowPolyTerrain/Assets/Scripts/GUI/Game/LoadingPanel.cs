using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    class LoadingPanel:MonoBehaviour
    {
        [SerializeField]
        Image image;

        private static LoadingPanel instance { get; set; }

        private void Awake()
        {
            instance = this;
        }

        public static void OnProgress(float progress)
        {
            instance.image.rectTransform.sizeDelta = new Vector2(300 * progress, 10);
            instance.image.rectTransform.localPosition = new Vector3(-150 + (150 *progress), -50);
            // instance.image.rectTransform.position = new Vector3(-150 * progress, -50);
        }

        public static void Disable()
        {
            instance.gameObject.SetActive(false);
        }

        public static void Enable()
        {
            instance.gameObject.SetActive(true);
        }
    }
}
