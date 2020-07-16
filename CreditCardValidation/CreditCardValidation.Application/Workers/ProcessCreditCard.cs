using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreditCardValidation.Domain.CreditCardAggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreditCardValidation.Application.Workers
{
  public class ProcessCreditCard : BackgroundService
  {
    // private readonly ICreditCardUnitOfWork _creditCardUnitOfWork;
    private readonly IServiceProvider _service;
    private readonly ILogger<ProcessCreditCard> _logger;

    public ProcessCreditCard(ILogger<ProcessCreditCard> logger, IServiceProvider service)
    {
      _logger = logger;
      _service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await DoWork(stoppingToken);
        await Task.Delay(30 * 1000, stoppingToken);
      }
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
      await UpdateCreditCardProcess(stoppingToken);
    }

    private async Task UpdateCreditCardProcess(CancellationToken stoppingToken)
    {
      using(var scope = _service.CreateScope())
      {
        try {
          var builder = scope.ServiceProvider.GetRequiredService<CreditCardBuilder>();
          var creditCards = builder.Build().Get5CreditCardsToProcess();
          Console.WriteLine($"Count: {creditCards.Count()}");
          foreach (var creditCard in creditCards)
          {
            Console.WriteLine($"No: {creditCard.No}\tStatusId: ${creditCard.CreditCardStatusId}\tProvider Id: ${creditCard.CreditCardProviderId}");
            await builder.Copy(creditCard).Build().Update();
          }
          _logger.LogInformation("**** Updated: {time}", DateTimeOffset.Now);
        } catch(Exception e)
        {
          _logger.LogError(e.Message);
        }
      }
    }
  }
}