using System.ComponentModel.DataAnnotations;

namespace backend.Models.Entities
{
    public class Vote
    {
        [Key]
        public string Key { get; set; }
        public string ClientId { get; set; }
        public string PollId { get; set; }
        public Poll Poll { get; set; }

        public Option Option { get; set; }
        public string OptionKey { get; set; }
    }
}
