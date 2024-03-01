
public static partial class GameEvents
{
    public static class MenuEvents
    {
        public static readonly GameEvent<MenuName> MenuTransitionEvent = new();
        public static readonly GameEvent MenuTransitionBackEvent = new();

        public static readonly GameEvent OnNextClicked = new();
        public static readonly GameEvent OnBackClicked = new();
    }
}
