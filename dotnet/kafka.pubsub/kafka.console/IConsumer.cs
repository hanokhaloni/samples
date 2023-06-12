namespace kafka.pubsub.console
{
    public interface IConsumer
    {
        Task ConsumeAsync(CancellationToken cancellationToken = default);
    }
}