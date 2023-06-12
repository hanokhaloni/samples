using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace kafka.pubsub.console
{

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var producer = new Producer<Message>();

            var consumer = new Consumer<Message>();
            var consumeTask = Task.Run(() => consumer.ConsumeAsync());

            for (int i = 0; i < 25; i++)
            {
                await producer.ProduceAsync(new Message
                {
                    Data = $"Pushing Data {i} !!",
                });

                await Task.Delay(1000);
            }

            Console.WriteLine("Publish Success!");
            Console.ReadKey();

        }
    }
}