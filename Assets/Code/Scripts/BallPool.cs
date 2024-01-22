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
            obj.transform.parent = transform;
        }
        #endregion

        #region PUBLIC METHODS
        public void TestRequestBall()
        {
            RequestBall();
        }

        public GameObject RequestBall()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeSelf)
                {
                    BallEnabledOrDisabled(_pool[i], true);
                    return _pool[i];
                }
            }

            return null;
        }
        #endregion

        
    }

}
