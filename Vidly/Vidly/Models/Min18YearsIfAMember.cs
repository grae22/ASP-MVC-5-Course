using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Dtos;

namespace Vidly.Models
{
  public class Min18YearsIfAMember : ValidationAttribute
  {
    protected override ValidationResult IsValid( object value,
                                                 ValidationContext validationContext )
    {
      dynamic customer;
      
      if( typeof( Customer ).IsAssignableFrom( validationContext.ObjectInstance.GetType() ) )
      {
        customer = (Customer)validationContext.ObjectInstance;
      }
      else
      {
        customer = (CustomerDto)validationContext.ObjectInstance;
      }

      if( customer.MembershipTypeId == MembershipType.Unknown ||
          customer.MembershipTypeId == MembershipType.PayAsYouGo )
      {
        return ValidationResult.Success;
      }

      if( customer.BirthDate == null )
      {
        return new ValidationResult( "Birth-date is required." );
      }

      var age = DateTime.Today.Year - customer.BirthDate.Year;

      return ( age >= 18 ) ?
        ValidationResult.Success :
        new ValidationResult( "Customer must be at least 18 years old." );
    }
  }
}