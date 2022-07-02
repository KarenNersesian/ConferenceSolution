using ConferenceShareModel.Db;
using ConferenceShareModel.Models;

namespace ConferenceSolution.Data
{
    public static class DataSeeder
    {
        public static void Preperate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
            }
        }

        private static void SeedData(ApplicationDbContext context)
        {
            if (!context.Participants.Any())
            {
                context.Participants.AddRange(new List<Participant>()
                {
                    new Participant()
                    {
                        Id = "5e7db5e2-0bcf-4bf1-b9b7-fb40915d8882",
                        Name = "SomeName1",
                        LastName = "SomeLastName1",
                        BirthDate = new DateTime(1996, 3, 31)
                    },
                    new Participant()
                    {
                        Id = "218d66f0-de32-48ff-bb54-d861d1cbdbf2",
                        Name = "SomeName2",
                        LastName = "SomeLastNam2",
                        BirthDate = new DateTime(1994, 12, 7)
                    },
                });

                context.SaveChanges();
            }
        }
    }
}
