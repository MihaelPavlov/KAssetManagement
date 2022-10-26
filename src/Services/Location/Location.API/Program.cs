using Asset.EventBus.Messages;
using Location.API.EventBusConsumer;
using Location.Service;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);
// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<CreateAssetLocationConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.AssetCreateLocationQueue, c =>
        {
            c.ConfigureConsumer<CreateAssetLocationConsumer>(ctx);
        });
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.Run();
