using Confluent.Kafka;


namespace kafka.pubsub.console
{
    public class Producer<T> : IProducer<T>
    {

        readonly string _host;
        readonly int _port;
        readonly string _topic;


        public Producer(string host = "localhost", int port = 9092, string topic = "producer_logs")
        {
            _host = host;
            _port = port;
            _topic = topic;
        }

        ProducerConfig GetProducerConfig()
        {
            return new ProducerConfig
            {
                BootstrapServers = $"{_host}:{_port}"
            };
        }

        public async Task ProduceAsync(T data)
        {
            using (var producer = new ProducerBuilder<Null, T>(GetProducerConfig())
                                                 .SetValueSerializer(new CustomValueSerializer<T>())
                                                 .Build())
            {
                await producer.ProduceAsync(_topic, new Message<Null, T> { Value = data });
                Console.WriteLine($"{data} published");
            }
        }
    }
}