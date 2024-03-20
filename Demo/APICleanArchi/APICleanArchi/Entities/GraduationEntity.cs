namespace APICleanArchi.Entities
{
    public class GraduationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
