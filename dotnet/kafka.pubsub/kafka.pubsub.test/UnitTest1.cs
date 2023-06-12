using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
            var producer = new Producer<string>();

            var topic = "test_topic";
            var message = "Hello, Kafka!";

            // Act
            producer.Publish(topic, message);

            // Assert
            producerMock.Verify(p => p.ProduceAsync(topic, It.IsAny<Message<Null, string>>()), Times.Once);
        }

        [TestMethod]
        public async Task Consumer_ConsumeAsync_ReceivedMessage()
        {
            // Arrange
            var consumerMock = new Mock<IConsumer<Ignore, string>>();
            var consumer = new Consumer<string>(consumerMock.Object);

            var topic = "test_topic";
            var message = "Hello, Kafka!";

            var consumeResult = new ConsumeResult<Ignore, string>
            {
                Message = new Message<Ignore, string>
                {
                    Value = message
                }
            };

            consumerMock.SetupSequence(c => c.Consume(It.IsAny<CancellationToken>()))
                .Returns(consumeResult)
                .Throws(new OperationCanceledException()); // To exit the loop

            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            await consumer.ConsumeAsync(cancellationTokenSource.Token);

            // Assert
            consumerMock.Verify(c => c.Subscribe(topic), Times.Once);
            consumerMock.Verify(c => c.Consume(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            consumerMock.Verify(c => c.Close(), Times.Once);
        }
    }
}
