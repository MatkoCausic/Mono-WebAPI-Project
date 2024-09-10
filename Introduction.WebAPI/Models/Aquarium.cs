namespace Introduction
{
    public class Aquarium
    {
        public Guid Id { get; set; }
        public string? OwnerName { get; set; }
        public string? Shape { get; set; }
        public bool IsHandmade { get; set; }
        public double? Volume { get; set; }
        public List<Fish> fishes { get; set; }
    }
}
