using Confluent.Kafka;
using System.Threading;


namespace kafka.pubsub.console
{
    public class Consumer<T> : IConsumer
    {
        readonly string _host;
        readonly int _port;
        readonly string _topic;

        public Consumer(string host = "localhost", int port = 9092, string topic = "producer_logs")
        {
            _host = host;
            _port = port;
            _topic = topic;
        }

        ConsumerConfig GetConsumerConfig()
        {
            return new ConsumerConfig
            {
                BootstrapServers = $"{_host}:{_port}",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public async Task ConsumeAsync(CancellationToken cancellationToken = default)
        {
            using (var consumer = new ConsumerBuilder<Ignore, T>(GetConsumerConfig())
                .SetValueDeserializer(new CustomValueDeserializer<T>())
                .Build())
            {
                consumer.Subscribe(_topic);

                Console.WriteLine($"Subscribed to {_topic}");

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken);
                        Task.Delay(100);

                        Console.WriteLine($"Data Received - {consumeResult.Message.Value}");
                    }
                    catch (ConsumeException ex)
                    {
                        // Handle or log the exception
                        Console.WriteLine($"Error consuming message: {ex.Message}");
                    }
                    catch (OperationCanceledException)
                    {
                        // Handle or log the cancellation
                        Console.WriteLine("Consumer cancelled.");
                    }
                    
                }
            }
        }
    }
}