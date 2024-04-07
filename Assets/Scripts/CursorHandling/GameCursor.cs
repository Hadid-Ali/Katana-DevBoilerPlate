using UnityEngine;

public static class CursorStateHandler
{
    private static int m_Showcalls = 0;

    public static void ShowCursor()
    {
        SetCursorLock(false);
        m_Showcalls++;
    }

    public static void HideCursor()
    {
        m_Showcalls--;
        Debug.LogError($"Cursor Count {m_Showcalls}");   
        if (m_Showcalls > 0)
            return;
        
        SetCursorLock(true);
    }
    
    private static void SetCursorLock(bool isLockState)
    {
        Cursor.visible = !isLockState;
        Cursor.lockState = isLockState ? CursorLockMode.Locked : CursorLockMode.Confined;
    }
}
