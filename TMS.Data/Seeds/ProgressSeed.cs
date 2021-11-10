using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Data.Entities;

namespace TMS.Data.Seeds
{
    public class ProgressSeed
    {
        public static async Task SeedProgressData(DataContext context)
        {
            if (!context.Progresses.Any())
            {
                var progresses = new List<Progress>
                {
                    new Progress
                    {
                        ProgressName = "Planned",
                        TaskModels=null
                    },
                    new Progress
                    {
                        ProgressName = "InProgress",
                        TaskModels=null
                    },
                    new Progress
                    {
                        ProgressName = "Completed",
                        TaskModels=null
                    }
                };
                await context.Progresses.AddRangeAsync(progresses);
                await context.SaveChangesAsync();
            }
        }

    }
}

