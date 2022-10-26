using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.API.Core.Manager;
using Product.API.Core.Models.Domain;
using Product.API.Core.Models.Response;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Product.API.Core.EventBus
{
    public class EventBusSubscriber : BackgroundService
    {
        private ConnectionFactory? _factory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private IConnection? _connection;
        private IModel? _channel;

        public EventBusSubscriber(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {

            _factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"]),
            };

            CreateEventBusConnection();
        }

        private void CreateEventBusConnection()
        {
            try
            {
                if (_factory != null)
                {
                    Console.WriteLine($"--> Host: {_factory.HostName}, Port: ${_factory.Port}");
                    _connection = _factory.CreateConnection();
                    _channel = _connection.CreateModel();

                    _channel.ExchangeDeclare("direct", type: ExchangeType.Direct);

                    Console.WriteLine("--> Connection created");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void RegisterNewCategoryConsumer()
        {
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, "direct", "new-category");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += NewCategoryMessageReceived;

            _channel.BasicConsume(queueName, true, consumer);
        }

        public void NewCategoryMessageReceived(object? sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var newCategory = JsonSerializer.Deserialize<CategoryResponseModel>(Encoding.UTF8.GetString(body));

            if (newCategory != null)
            {
                var newProduct = new ProductModel { CategoryId = newCategory.Id, ProductName = $"Product from {newCategory.CategoryName}", Cost = 10.99M, };
                using var scope = _scopeFactory.CreateScope();

                var productManager = scope.ServiceProvider.GetRequiredService<IProductManager>();
                productManager.CreateProduct(newProduct);
                productManager.SaveChanges();
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            RegisterNewCategoryConsumer();

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (_connection != null && _connection.IsOpen)
            {
                _channel?.Close();
                _connection.Close();
            }

            base.Dispose();
        }
    }
}