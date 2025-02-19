namespace CargoPayAPI.Service.Interfaces
{
    public interface IAuthService
    {
        public string Authenticate(string username, string password);
    }
}
