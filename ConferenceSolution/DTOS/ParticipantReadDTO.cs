namespace ConferenceSolution.DTOS
{
    public class ParticipantReadDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;
    }
}
