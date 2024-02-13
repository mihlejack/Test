using Service.Models;

namespace Service.Interfaces
{
    public interface ITestService
    {
        Task<TestUser> Put(int Id, TestUser updatedTestUser);
        List<TestUser> Get();
        Task<TestUser> Post(TestUser testUser);
        Task<TestUser> Delete(int Id);
    }
}
