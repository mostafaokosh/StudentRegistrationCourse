using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationSystem.Models
{
    public class AddStudentRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public string Level { get; set; }

        public DateTime? StartDate { get; set; }

        public string? Notes { get; set; }

    }
}
