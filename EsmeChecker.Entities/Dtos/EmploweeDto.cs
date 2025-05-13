using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsmeChecker.Entities.Dtos
{
    public class EmploweeDto : BaseModelDto
    {
		[Required]
		public string Msisdn { get; set; }

		public string? Name { get; set; }

		public string? Email { get; set; }

		public string? Postion { get; set; }

		public Boolean Allow { get; set; } = true;


		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category Category { get; set; }
	}
}
