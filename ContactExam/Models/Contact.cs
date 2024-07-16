namespace ContactExam.Models
{
    public class Contact
    {
        public Contact() { }
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
    }
}
