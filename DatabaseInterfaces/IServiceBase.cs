

namespace DatabaseInterfaces
{
    public interface IServiceBase
    {
        string ConnectionString { get; }
        string DomainAssemblyName { get; }
        IRepository<T> CreateRepository<T>() where T : class, new();
    }
}