namespace ApplicationLayer.Services.Random
{
    public interface IRandomFacade
    {
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
    }
}