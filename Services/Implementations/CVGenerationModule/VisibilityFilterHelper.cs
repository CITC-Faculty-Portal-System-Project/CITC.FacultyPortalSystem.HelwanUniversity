namespace Services.Implementations.CVGenerationModule
{
    public static class VisibilityFilterHelper
    {
        public static void HideIfFalse(bool condition, Action hideAction)
        {
            if (!condition)
                hideAction();
        }
    }
}