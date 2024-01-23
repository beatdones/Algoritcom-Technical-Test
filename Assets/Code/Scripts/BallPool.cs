using Algoritcom.TechnicalTest.Ball;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Algoritcom.TechnicalTest.BallSpawn
{
    public class BallPool : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField] private AssetReference _assetReference;
        [SerializeField] private int _poolSize = 1;

        [SerializeField] private List<GameObject> _pool;


        private static BallPool instance;
        public static BallPool Instance { get { return instance; } }
        #endregion

        #region UNITY METHODS
        private void Awake()
        {
            CreateSingleton();
        }

        void Start()
        {
            InstantiateObjectToPool(_poolSize);
        }
        #endregion

        #region PRIVATE METHODS
        private void CreateSingleton()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InstantiateObjectToPool(int amount)
        {
            _pool = new List<GameObject>();

            for (int i = 0; i < amount; i++)
            {
                var asyncOperationHandle = _assetReference.InstantiateAsync();

                asyncOperationHandle.Completed += OnComplet;
            }
        }

        private void OnComplet(AsyncOperationHandle<GameObject> obj)
        {
            GameObject assetObject = obj.Result;

            BallEnabledOrDisabled(assetObject, false);

            AddBallsToPool(assetObject);
        }

        private void BallEnabledOrDisabled(GameObject obj, bool active)
        {
            obj.SetActive(active);
        }

        private void AddBallsToPool(GameObject obj)
        {
            _pool.Add(obj);
        }
        #endregion

        #region PUBLIC METHODS
        public GameObject RequestBall()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeSelf)
                {
                    BallEnabledOrDisabled(_pool[i], true);
                    _pool[i].transform.position = gameObject.transform.position;
                    return _pool[i];
                }
            }
            return null;
        }
        #endregion
    }

}
