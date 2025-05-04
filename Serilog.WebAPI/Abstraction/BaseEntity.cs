namespace Serilog.WebAPI.Abstraction
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
