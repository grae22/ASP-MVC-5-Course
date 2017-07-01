using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display( Name="Email" )]
    public string Email
    {
      get; set;
    }

    [Required]
    [Display( Name="Driving LIcense" )]
    [StringLength( 255 )]
    public string DrivingLicense
    {
      get; set;
    }

    [Required]
    [Display( Name="Phone Number" )]
    [StringLength( 50 )]
    public string PhoneNumber
    {
      get; set;
    }
  }
}