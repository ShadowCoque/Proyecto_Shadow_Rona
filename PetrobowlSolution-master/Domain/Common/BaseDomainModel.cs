namespace Domain.Common;

public abstract class BaseDomainModel
{
    public int id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}
