using CargoPayAPI.Service.Interfaces;

namespace CargoPayAPI.Service
{
    public class PaymentFeeService : IPaymentFeeService
    {
        private static readonly object _lock = new();
        private static decimal _currentFee = 1.0m;
        private static readonly Random _random = new();
        private static bool _isUpdating = false;

        public PaymentFeeService()
        {
            StartUpdatingFee();
        }

        public decimal GetCurrentFee()
        {
            lock (_lock)
            {
                return _currentFee;
            }
        }

        private void StartUpdatingFee()
        {
            if (!_isUpdating)
            {
                _isUpdating = true;
                Task.Run(UpdateFeeEveryHour);
            }
        }
        private async Task UpdateFeeEveryHour()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(5)); 
                ForceUpdateFee();
            }
        }

        public void ForceUpdateFee()
        {
            lock (_lock)
            {
                decimal randomMultiplier = (decimal)(_random.NextDouble() * 2);
                _currentFee *= randomMultiplier;
                Console.WriteLine($"[UFE] New Fee Applied: {_currentFee}");
            }
        }

    }
}
