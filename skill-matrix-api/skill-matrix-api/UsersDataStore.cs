using skill_matrix_api.Models.Users;

namespace skill_matrix_api
{
    public class UsersDataStore
    {
        public List<User> Users { get; set; }
        public static UsersDataStore Current { get; } = new UsersDataStore();

        public UsersDataStore()
        {
            //init dummy data
            Users = new List<User>() 
            {
                new User() { UserId=1, Name="Matteo", Email="matteo.ristoro@divini.org", Role="intern"},
                new User() { UserId=2, Name="Marco", Email="marco.ristoro@divini.org", Role="employer"},
                new User() { UserId=3, Name="Jacopo", Email="matteo.ristoro@divini.org", Role="employee"}
            };  
        }
    }
}
