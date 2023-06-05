namespace skill_matrix_api.Services
{
    public interface ICrudRepository
    {
        public Task<int> SaveChangesAsync();
    }
}
