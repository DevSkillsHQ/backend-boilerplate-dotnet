using System.ComponentModel.DataAnnotations;

namespace backend.Models.Entities
{
    public class Option
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        
    }
}
