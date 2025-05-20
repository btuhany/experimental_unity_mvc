using System;
using System.Collections.Generic;
using Batuhan.EventBus;
using Batuhan.MVC.Core;
using SnakeExample.Config;
using SnakeExample.Events;
using SnakeExample.Grid;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ExperimentalMVC.SnakeExample.Entities.FoodController
{
    public class FoodModel : IGridObject
    {
        public Action<FoodModel> OnEaten;
        public Vector2Int GridPos { get; set; }
        public bool IsOnGrid { get; set; }
        public GridObjectType ObjectType => GridObjectType.Food;
        public void OnRemovedFromGrid()
        {
            OnEaten?.Invoke(this);
        }
        public void AddEatenCallback(Action<FoodModel> callback)
        {
            OnEaten += callback;
        }
        public void RemoveEatenCallback(Action<FoodModel> callback)
        {
            OnEaten -= callback;
        }
    }
    public class FoodController : ISceneLifeCycleManaged
    {
        private Dictionary<FoodModel, FoodView> _foodDict; //different approach?
        [Inject] private IEventBus<GameEvent> _eventBus;
        [Inject] private IGridModelHelper _gridModel;
        [Inject] private FoodView _foodViewPrefab;
        [Inject] private GameConfigDataSO _configDataSO;
        public void OnAwakeCallback()
        {
            _foodDict = new Dictionary<FoodModel, FoodView>();
            _eventBus.Subscribe<SceneInitializationEvent>(OnSceneInitialization);
        }
        public void OnDestroyCallback()
        {
            _eventBus.Unsubscribe<SceneInitializationEvent>(OnSceneInitialization);
        }
        private void OnSceneInitialization(SceneInitializationEvent obj)
        {
            var foodCount = _configDataSO.FoodCount;
            for (int i = 0; i < foodCount; i++)
            {
                GenerateFood();
            }
        }

        private void GenerateFood()
        {
            var width = _gridModel.Grid.Width;
            var height = _gridModel.Grid.Height;
            
            int randomX = UnityEngine.Random.Range(0, width);
            int randomY = UnityEngine.Random.Range(0, height);
            var foodModel = new FoodModel();

            int iterate = 5;
            while (iterate > 0)
            {
                if (_gridModel.Grid.TrySetElement(randomX, randomY, foodModel))
                {
                    var foodView = Object.Instantiate(_foodViewPrefab, _gridModel.Grid.GetWorldPositionCenter(randomX, randomY), Quaternion.identity);
                    foodModel.AddEatenCallback(OnFoodModelEaten);
                    _foodDict.Add(foodModel, foodView);
                    break;
                }
                else
                {
                    iterate--;
                    randomX = UnityEngine.Random.Range(0, width);
                    randomY = UnityEngine.Random.Range(0, height);
                }
            }
        }

        private void OnFoodModelEaten(FoodModel model)
        {
            model.RemoveEatenCallback(OnFoodModelEaten);
            
            var view = _foodDict[model];
            Object.Destroy(view.gameObject);
            
            _foodDict.Remove(model);

            GenerateFood();
        }

    }
}