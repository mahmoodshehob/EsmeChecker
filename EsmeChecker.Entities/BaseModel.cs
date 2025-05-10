using System.ComponentModel.DataAnnotations;

namespace EsmeChecker.Entities
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

		public DateTime ModifyDate { get; set; }
	}
}
