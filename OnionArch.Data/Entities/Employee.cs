namespace OnionArch.Data.Entities
{
    public class Employee
    {
       public Guid Id {get; set;} 
       public string FullName {get;set;}
       public string UserName { get;set;}
        public string Email { get; set; }

        public string Password { get; set; }

        public string Job { get; set; }
        public PositionType PositionType { get; set; }
        public Guid PositionTypeId { get; set; }

        public Boolean isAdmin { get; set; } = false;
    }
}