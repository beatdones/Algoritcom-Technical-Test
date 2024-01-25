using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Algoritcom.TechnicalTest.Addressable
{
    public class AddressableController : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> _assetReferences;
        [SerializeField] private List<GameObject> _addressableObjects;

        private GameObject _addressableObject;

        private static AddressableController _instance;
        public static AddressableController Instance { get { return _instance; } }

        private void Awake()
        {
            CreateSingleton();


            for (int i = 0; i < _assetReferences.Count; i++)
            {
                var asyncOperationHandle = _assetReferences[i].InstantiateAsync();
                asyncOperationHandle.Completed += OnCompleted;
            }
        }

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

        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            _addressableObjects.Add(obj.Result);
        }

        private void Release()
        {
            Addressables.ReleaseInstance(_addressableObject);
        }

    }
}

