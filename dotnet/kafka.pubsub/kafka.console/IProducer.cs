namespace kafka.pubsub.console
{
    public interface IProducer<T>
    {
        Task ProduceAsync(T data);
    }
}