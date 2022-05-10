using System;
using UnityEngine;

public class CellPrefab : MonoBehaviour
{
    [SerializeField] private Material aliveMaterial;
    [SerializeField] private Material deadMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    public bool _isAlive;
    private Vector3 _originalPos;
    private int _arrayPosX;
    private int _arrayPosY;

    private void Awake()
    {
        _originalPos = gameObject.transform.position;
        meshRenderer.material = deadMaterial;
        _isAlive = false;
    }

    public void SetArrayPosition(int posX, int posY)
    {
        _arrayPosX = posX;
        _arrayPosY = posY;
        SetPosition(posX, posY);
    }

    public int GetPosX()
    {
        return _arrayPosX;
    }

    public int GetPosY()
    {
        return _arrayPosY;
    }

    private void SetPosition(int x, int y)
    {
        SetOriginalPosition();

        gameObject.transform.position = GenerateNewPosition(x, y);
    }

    private Vector3 GenerateNewPosition(int x, int y)
    {
        Vector3 newPosition = _originalPos;
        newPosition.x += x;
        newPosition.y += y;
        return newPosition;
    }

    private void SetOriginalPosition()
    {
        gameObject.transform.position = _originalPos;
    }

    public void SwitchLifeState(bool state)
    {
        if (state)
        {
            Revive();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        meshRenderer.material = deadMaterial;
        _isAlive = false;
    }
    
    private void Revive()
    {
        meshRenderer.material = aliveMaterial;
        _isAlive = true;
    }
}
