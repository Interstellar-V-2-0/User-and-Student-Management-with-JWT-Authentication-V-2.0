namespace UserStudentMgmt.Application.DTOs.Students;

public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int DocTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime EnrollmentDate { get; set; }
}