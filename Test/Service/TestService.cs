using Service.Interfaces;
using Service.Models;

namespace Service
{
    public class TestService : ITestService
    {
        public async Task<TestUser> Delete(int Id)
        {
            string[] lines = await System.IO.File.ReadAllLinesAsync("TestUser.txt");
            var deletedLine = lines.FirstOrDefault(line => line.Contains($"ID: {Id}"));
            lines = lines.Where(line => !line.Contains($"ID: {Id}")).ToArray();
            await System.IO.File.WriteAllLinesAsync("TestUser.txt", lines);
            if (deletedLine == null)
            {
                return null;
            }
            var parts = deletedLine.Split(", ");
            return new TestUser
            {
                Id = int.Parse(parts[0].Split(": ")[1]),
                FirstName = parts[1].Split(": ")[1],
                LastName = parts[2].Split(": ")[1],
                ContactNo = parts[3].Split(": ")[1]
            };
        }


        public List<TestUser> Get()
        {
            var testUsers = new List<TestUser>();
            string[] lines = System.IO.File.ReadAllLines("TestUser.txt");

            foreach (var line in lines)
            {
                var parts = line.Split(", ");
                var testUser = new TestUser
                {
                    Id = int.Parse(parts[0].Split(": ")[1]),
                    FirstName = parts[1].Split(": ")[1],
                    LastName = parts[2].Split(": ")[1],
                    ContactNo = parts[3].Split(": ")[1]
                };
                testUsers.Add(testUser);
            }

            return testUsers;
        }

        public async Task<TestUser> Post(TestUser testUser)
        {
            var test = new TestUser
            {
                Id = testUser.Id,
                FirstName = testUser.FirstName,
                LastName = testUser.LastName,
                ContactNo = testUser.ContactNo
            };

            // Convert the TestUser object to a string
            var testUserString = $"ID: {test.Id}, FirstName: {test.FirstName}, LastName: {test.LastName}, ContactNo: {test.ContactNo}";

            // Write the string to a new file named "TestUser.txt".
            await System.IO.File.AppendAllTextAsync("TestUser.txt", testUserString + Environment.NewLine);

            return test;
        }


        public async Task<TestUser> Put(int Id, TestUser updatedTestUser)
        {
            string[] lines = await System.IO.File.ReadAllLinesAsync("TestUser.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains($"ID: {Id}"))
                {
                    lines[i] = $"ID: {updatedTestUser.Id}, FirstName: {updatedTestUser.FirstName}, LastName: {updatedTestUser.LastName}, ContactNo: {updatedTestUser.ContactNo}";
                    break;
                }
            }

            await System.IO.File.WriteAllLinesAsync("TestUser.txt", lines);

            return updatedTestUser;
        }

    }
}
