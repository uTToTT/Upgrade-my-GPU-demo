public interface IEnergyConsumer
{
    float GetRatedConsumptionPerSecond();
    float GetConsumptionPerSecond();
    void SetEnergyFactor(float factor);

}