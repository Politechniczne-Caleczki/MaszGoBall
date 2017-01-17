using Assets.Scripts.GameEngine.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Game
{
    class GameGUI : MonoBehaviour
    {
        private static GameGUI instance;


        [SerializeField]
        private RectTransform compass;

        [SerializeField]
        private Image playerColor;

        [SerializeField]
        private RectTransform scorePanel;

        [SerializeField]
        private Text textPrefab;


        public static Transform Compass
        {
            get { return instance.compass; }
        }

        public static Image PlayerImage
        {
            get { return instance.playerColor; }
        }


        public static Transform ScorePanel
        {
            get { return instance.scorePanel; }
        }

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            HideScorePanel();
        }

        private void OnDisable()
        {
            HideScorePanel();
        }

        public static void ShowScorePanel()
        {
            instance.scorePanel.gameObject.SetActive(true);

            foreach(Assets.Scripts.GameEngine.Units.Player player in GameObject.FindObjectsOfType<Assets.Scripts.GameEngine.Units.Player>())            
                Instantiate(instance.textPrefab, instance.scorePanel).text = string.Format("{0}", player.ScoreString);
            

        }

        public static void HideScorePanel()
        {
            foreach (Transform child in instance.scorePanel)
            {
                Destroy(child.gameObject);
            }
            instance.scorePanel.gameObject.SetActive(false);
        }


    }
}
