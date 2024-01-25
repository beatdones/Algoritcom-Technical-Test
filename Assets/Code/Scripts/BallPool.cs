using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Events;

namespace Algoritcom.TechnicalTest.BallSpawn
{
    public class BallPool : MonoBehaviour
    {
        #region VARIABLES
        [Header("REFERENCES")]
        [SerializeField] private AssetReference _assetReference;
        [SerializeField] private int _poolSize = 1;
        [SerializeField] private List<GameObject> _pool;

        private static BallPool _instance;
        public static BallPool Instance { get { return _instance; } }

        public UnityEvent unityEvent;
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
            if (_instance == null)
            {
                _instance = this;
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

        /// <summary>
        /// Receives an asynchronous operation handle for a GameObject, retrieves the result, disables the ball through BallEnabledOrDisabled, and adds it to a pool using AddBallsToPool.
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Iterates through a pool of GameObjects, activates the first inactive one, repositions it to the player's position, and returns the activated ball GameObject.
        /// </summary>
        /// <returns></returns>
        public GameObject RequestBall()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeSelf)
                {
                    BallEnabledOrDisabled(_pool[i], true);
                    _pool[i].transform.position = gameObject.transform.position;
                    unityEvent?.Invoke();
                    return _pool[i];
                }
            }
            return null;
        }
        #endregion
    }

}
