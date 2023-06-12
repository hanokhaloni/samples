using kafka.pubsub.console;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace kafka.pubsub.test
{
    [TestClass]
    public class PubSubTests
    {
        [TestMethod]
        public void Producer_PublishMessage_Success()
        {
            // Arrange
            var producerMock = new Mock<IProducer<string>>();

            var topic = "test_topic";
            var producer = new Producer<string>(topic = topic);

            
            var message = "Hello, Kafka!";

            // Act
            producer.ProduceAsync(message);

            // Assert
            producerMock.Verify(p => p.ProduceAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task Consumer_ConsumeAsync_ReceivedMessage()
        {
            // Arrange
            var consumerMock = new Mock<IConsumer>();

            var topic = "test_topic";
            var consumer = new Consumer<string>(topic = topic);

            string? message = "Hello, Kafka!";

            consumerMock.SetupSequence(c => c.ConsumeAsync(It.IsAny<CancellationToken>()))
                //.Returns<string>(message)
                .Throws(new OperationCanceledException()); // To exit the loop

            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            await consumer.ConsumeAsync(cancellationTokenSource.Token);

            // Assert
            consumerMock.Verify(c => c.ConsumeAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}
