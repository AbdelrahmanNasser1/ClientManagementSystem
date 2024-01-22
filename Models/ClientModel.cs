
using System.ComponentModel;

namespace ClientManagementSystemMVC.Models;

public class ClientModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "This Field is required.")]
    [MaxLength(50, ErrorMessage = "Maximum 20 characters only")]
    [Column(TypeName = "nvarchar(20)")]
    public string Name { get; set; }

    [Required(ErrorMessage = "This Field is required.")]
    [MaxLength(15, ErrorMessage = "Maximum 15 characters only")]
    [Column(TypeName = "nvarchar(15)")]
    [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]

    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "This Field is required.")]
    [MaxLength(50, ErrorMessage = "Maximum 20 characters only")]
    [Column(TypeName = "nvarchar(20)")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "This Field is required.")]
    [MaxLength(100, ErrorMessage = "Maximum 100 characters only")]
    [Column(TypeName = "nvarchar(100)")]
    public string HomeAddress { get; set; }
}
