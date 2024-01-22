using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableController : MonoBehaviour
{
    [SerializeField] private List<AssetReference> _assetReferences;

    [SerializeField] private List<GameObject> _addressableObjects;

    private GameObject addressableObject;

    private void Awake()
    {
        for (int i = 0; i < _assetReferences.Count; i++)
        {
            var asyncOperationHandle = _assetReferences[i].InstantiateAsync();
            asyncOperationHandle.Completed += OnCompleted;
        }
    }

    private void OnCompleted(AsyncOperationHandle<GameObject> obj)
    {
        _addressableObjects.Add(obj.Result);
    }

    private void Release()
    {
        Addressables.ReleaseInstance(addressableObject);
    }


}
