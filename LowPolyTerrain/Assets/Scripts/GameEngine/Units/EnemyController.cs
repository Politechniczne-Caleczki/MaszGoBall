using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameEngine.Units
{
    class EnemyController
    {
        private static List<Nation> Enemies = new List<Nation>();

        public static void Add(Nation nation)
        {
            Enemies.Add(nation);
        }

        public static void Claer()
        {
            Enemies.Clear();
        }

        public static void Enable()
        {
            Enemies.ForEach((x) => { x.gameObject.SetActive(true); });
        }

        public static void Disable()
        {
            Enemies.ForEach((x) => { x.gameObject.SetActive(false); });
        }

    }
}
