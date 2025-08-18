using System;
using UnityEngine;

public class ImGuiObjBase : MonoBehaviour
{
    [SerializeField]
    protected UImGui.UImGui _uimGuiInstance;

    protected virtual void Awake()
    {
        if (_uimGuiInstance == null)
        {
            Debug.LogError("Must assign a UImGuiInstance or use UImGuiUtility with Do Global Events on UImGui component.");
        }

        _uimGuiInstance.Layout += OnLayout;
        _uimGuiInstance.OnInitialize += OnInitialize;
        _uimGuiInstance.OnDeinitialize += OnDeinitialize;
    }

    protected virtual void Start()
    {
    }

    private void OnLayout(UImGui.UImGui obj)
    {
        // Unity Update method. 
        // Your code belongs here! Like ImGui.Begin... etc.

        OnDrawImGui(obj);
    }

    private void OnInitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnEnable();
        OnImGuiEnable(obj);
    }

    private void OnDeinitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnDisable();
        OnImGuiDisable(obj);
    }

    private void OnDisable()
    {
        _uimGuiInstance.Layout -= OnLayout;
        _uimGuiInstance.OnInitialize -= OnInitialize;
        _uimGuiInstance.OnDeinitialize -= OnDeinitialize;
    }
    
    protected virtual void OnDrawImGui(UImGui.UImGui obj) {}
    protected virtual void OnImGuiEnable(UImGui.UImGui obj) {}
    protected virtual void OnImGuiDisable(UImGui.UImGui obj) {}
}