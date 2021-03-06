﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
  public class Customer
  {
    public int Id { get; set; }

    [Required( ErrorMessage="Please supply a name." )]
    [StringLength( 255 )]
    public string Name { get; set; }

    [Min18YearsIfAMember]
    public DateTime? BirthDate { get; set; }

    public bool IsSubscribedToNewsletter { get; set; }

    public MembershipType MembershipType { get; set; }

    [Required( ErrorMessage="Membership type is required." )]
    public byte MembershipTypeId { get; set; }
  }
}