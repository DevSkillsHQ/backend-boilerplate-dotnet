namespace backend.Models.Entities
{
    public class Poll
    {
        public string PollId { get; set; }
        public string Question { get; set; }
        public string OptionsKey { get; set; }
        public Option Options { get; set; }
    }
}
