namespace CourseRegistrationSystem.Models
{
    public class StudentResponse
    {
        public List<AddStudentRequest> Students { get; set; } = new();
        public StatusEnum Status { get; set; } = StatusEnum.None;

        public string Message { get; set; }
    }
}
