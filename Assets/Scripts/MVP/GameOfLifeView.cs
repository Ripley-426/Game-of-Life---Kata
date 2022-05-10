using System;
using UnityEngine;

namespace MVP
{
    public class GameOfLifeView : MonoBehaviour, IGameOfLifeView
    {
        [SerializeField] private GameObject cellPrefab;
        private IGameOfLifePresenter _presenter;
        public bool autoGeneration = false;
        public float currentTime;
        public float timeToWaitUntilNextGen = 0.5f;

        private void Awake()
        {
            _presenter = new GameOfLifePresenter(this);
        }
        
        private void Update()
        {
            if (!autoGeneration) return;
            AddTimeSinceLastFrameToCurrentTime();
            if (CurrentTimeStillIsLessThanTimeToWait()) return;
            IncreaseGeneration();
            ResetCurrentTime();
        }
        
        public void ChangeButton()
        {
            _presenter.ChangeButton();
        }

        public void IncreaseGeneration()
        {
            _presenter.IncreaseGeneration();
        }

        public void ChangeAutoGenerationStatus()
        {
            autoGeneration = !autoGeneration;
            ResetCurrentTime();
        }

        private void AddTimeSinceLastFrameToCurrentTime()
        {
            currentTime += Time.deltaTime;
        }

        private bool CurrentTimeStillIsLessThanTimeToWait()
        {
            return !(currentTime >= timeToWaitUntilNextGen);
        }
        
        private void ResetCurrentTime()
        {
            currentTime = 0;
        }

        
        public GameObject InstantiateNewGoCell()
        {
            GameObject newCell = Instantiate(cellPrefab, gameObject.transform);
            
            return newCell;
        }
    }
}