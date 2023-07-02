namespace StorageApp.DTO
{
    public class UpdateArticelDTO
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Guid Storage { get; set; } = Guid.Empty;
    }
}
