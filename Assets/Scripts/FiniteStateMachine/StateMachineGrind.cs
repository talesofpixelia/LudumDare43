using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    public class StateMachineGrind : MonoBehaviour
    {
        private int ResponceTimeMillisecond { get; set; }
        private int Percentage { get; set; }

        private string[] ResultWin = new string[] { "Action1", "Action2", "Action3", "Action4" };

        public StateMachineGrind(int responceTime, int percentage)
        {
            this.ResponceTimeMillisecond = responceTime;
            this.Percentage = percentage;
        }

        public string PlayerController(string grindCorect)
        {
            DateTime start = DateTime.Now;
            TimeSpan response = new TimeSpan(0, 0, 0, 0, this.ResponceTimeMillisecond);
            string result = grindCorect;
            if (!Win())
            {
                do
                {
                    System.Random rnd = new System.Random();
                    result = ResultWin[rnd.Next(0,3)];

                } while (result == grindCorect);
            }

            TimeSpan span;
            TimeSpan dur;
            do
            {
                dur = DateTime.Now - start;
                span = response - dur;
            } while ((int)span.TotalMilliseconds <= 0);

            return result;
        }

        class Items
        {
            public double Probability { get; set; }
            public bool Item { get; set; }
        }

        private bool Win()
        {
            bool win = false;
            var elements = new List<Items>
            {
                new Items {Probability = this.Percentage / 100.0, Item = true},
                new Items {Probability = (100.0 - this.Percentage ) / 100.0, Item = false}
            };

            System.Random r = new System.Random();
            double diceRoll = r.NextDouble();

            double cumulative = 0.0;
            for (int i = 0; i < elements.Count; i++)
            {
                cumulative += elements[i].Probability;
                if (diceRoll < cumulative)
                {
                    win = elements[i].Item;
                    break;
                }
            }
            return win;
        }
      

    }
}



