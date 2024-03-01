using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenusController : MonoBehaviour
{
    [SerializeField] private List<UIMenuBase> m_Menus;
    [SerializeField] private MenuName m_StartMenu;
    
    [SerializeField] private MenuName m_CurrentMenuStates;
    private Stack<UIMenuBase> m_MenuStack = new();

    private void Start()
    {
        SetMenuState(m_StartMenu, false);
    }

    protected virtual void OnEnable()
    {
        GameEvents.MenuEvents.MenuTransitionEvent.Register(OnMenuTransition);
        GameEvents.MenuEvents.MenuTransitionBackEvent.Register(GoBack);
    }

    protected virtual  void OnDisable()
    {
        GameEvents.MenuEvents.MenuTransitionEvent.UnRegister(OnMenuTransition);
        GameEvents.MenuEvents.MenuTransitionBackEvent.UnRegister(GoBack);
    }
    
    protected void SetMenuState(MenuName menuName, bool SetBack)
    {
        if(menuName is MenuName.NoAction || m_CurrentMenuStates == menuName)
            return;
        
        SetMenuState_Internal(menuName);
        HideAllMenus();

        if (menuName is MenuName.None)
            return;
        
        UIMenuBase Menu =  m_Menus.Find(x => x.MenuName == menuName);
        Menu.SetMenuActiveState(true);
        
        if(!SetBack)
            m_MenuStack.Push(Menu);
    }

    protected void GoBack()
    {
        if(m_MenuStack.Count <= 1 || m_CurrentMenuStates == MenuName.None)
            return;

        m_MenuStack.Pop();

        MenuName menuName = m_MenuStack.Peek().MenuName;
        SetMenuState(menuName, true);
    }
    
    private void OnMenuTransition(MenuName menuName)
    {
        SetMenuState(menuName, false);
    }

    private void SetMenuState_Internal(MenuName menuName)
    {
        m_CurrentMenuStates = menuName;
    }
    
    private void HideAllMenus()
    {
        for (int i = 0; i < m_Menus.Count; i++)
        {
            m_Menus[i].SetMenuActiveState(false);
        }
    }
}