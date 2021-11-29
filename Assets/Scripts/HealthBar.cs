using System;
using System.ComponentModel;
using System.Reflection;
using TopDownAction;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    [Description("The width of one health point")]
    private float width;
    [SerializeField]
    private SpriteRenderer _renderer;
    private IDestroyable _destroyable;
    private Quaternion rotation;

    public void Constructor(IDestroyable destroyable)
    {
        _destroyable = destroyable;
        _destroyable.OnChangeHealth += UpdateRender;
        //rotate to the camera once if the camera does not rotate later
        rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Vector3.up);
        UpdateRender();
    }
    
    private void UpdateRender()
    {
        _renderer.size = new Vector2(width * _destroyable.Health, _renderer.size.y);
    }

    private void Update()
    {
        transform.rotation = rotation;
    }
}
