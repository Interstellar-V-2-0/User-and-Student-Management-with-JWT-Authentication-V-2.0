namespace UserStudentMgmt.Domain.Entities;

public abstract class Person
{
    public string Name {get; set;}
    public string LastName {get; set;}
    
    public int DocTypeId {get; set;}
    public DocumentType? DocType {get; set;}
    
    public string DocumentNumber { get; set; }
    public string Email {get; set;}
    public string PhoneNumber {get; set;}
}