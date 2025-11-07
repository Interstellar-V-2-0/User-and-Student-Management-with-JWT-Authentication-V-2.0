namespace UserStudentMgmt.Domain.Entities;

public class DocumentType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<User> Users { get; set; } = new List<User>();
}