using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RUser
{
	[Key]
	public int RUserId {get;set;}

	[Required(ErrorMessage="First name is required!")]
	[MinLength(2, ErrorMessage="Name must be at least 2 characters in length!")]
	public string FirstName {get;set;}

	[Required(ErrorMessage="Last name is required!")]
	[MinLength(2, ErrorMessage="Name must be at least 2 characters in length!")]
	public string LastName {get;set;}

	[Required(ErrorMessage="Email is required!")]
	[EmailAddress]
	public string Email {get;set;}

	[Required(ErrorMessage="Password is required!")]
	[MinLength(8, ErrorMessage="Password must be at least 8 characters in length!")]
	public string Password {get;set;}

	public DateTime CreatedAt {get;set;} = DateTime.Now;
	public DateTime UpdatedAt {get;set;} = DateTime.Now;

	[NotMapped]
	[Required(ErrorMessage="Must confirm password!")]
	[Compare("Password", ErrorMessage="Passwords must match!")]
	[DataType(DataType.Password)]
	public string Confirm {get;set;}
}