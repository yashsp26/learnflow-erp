using LearnFlowERP.Domain.Entities;

public class Designation : BaseEntity
{
    public long DesignationId { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}