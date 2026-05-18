namespace UdemyCarBook.Domain.Entities
{
    public class AppUser
    {
        public int AppUserId { get; set; }

        // BUNU TUTUYORUZ: Handler ve Context ile uyumlu olan bu.
        public string UserName { get; set; }

        // DİKKAT: 'public string Username { get; set; }' satırı varsa MUTLAKA SİL.

        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
        public string ImageUrl { get; set; }
    }
}