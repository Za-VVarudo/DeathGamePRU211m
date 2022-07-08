using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Timer : MonoBehaviour
    {
        private float countDown = 0;
        private bool isStart = false;

        public float CountDown { get => countDown > 0 ? countDown : 0; set => countDown = value; }

        private void Update()
        {
            if (CountDown >= 0 && isStart)
            {
                CountDown -= Time.deltaTime;
                return;
            }

            isStart = false;
        }

        public bool IsFinished
        {
            get => CountDown == 0;
        }

        public void StartCountDown()
        {
            isStart = true;
        }
    }
}
