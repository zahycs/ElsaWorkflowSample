using System.Threading.Tasks;
using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Models;
using Elsa.Serialization;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ElsaWorkflowSample
{
    class Program
    {
        /*  private static async Task Main() 
         {
             // Create a service container with Elsa services.
             var services = new ServiceCollection()
                 .AddElsa(options => options
                     .AddConsoleActivities()
                     .AddActivitiesFrom<Program>()
                     .AddWorkflow<HelloWorld>()
                     .AddWorkflow<MainWorkflow>()) // Defined a little bit below.
                 .BuildServiceProvider();

             // Get a workflow runner.
             var workflowRunner = services.GetRequiredService<IBuildsAndStartsWorkflow>();

             // Run the workflow.
             await workflowRunner.BuildAndStartWorkflowAsync<HelloWorld>();
             await workflowRunner.BuildAndStartWorkflowAsync<MainWorkflow>();
         }*/
        private static async Task Main()
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddActivitiesFrom<Program>())
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();
              // Serialize workflow definition to JSON.
            var serializer = services.GetRequiredService<IContentSerializer>();
 
            var json = await File.ReadAllTextAsync("wf-definition.json");
            // Deserialize workflow definition from JSON.
            var deserializedWorkflowDefinition = serializer.Deserialize<WorkflowDefinition>(json);

            // Materialize workflow.
            var materializer = services.GetRequiredService<IWorkflowBlueprintMaterializer>();
            var workflowBlueprint = await materializer.CreateWorkflowBlueprintAsync(deserializedWorkflowDefinition);

            // Execute workflow.
            var workflowRunner = services.GetRequiredService<IStartsWorkflow>();
            await workflowRunner.StartWorkflowAsync(workflowBlueprint);
        }
    }


    /// <summary>
    /// A basic workflow with just one WriteLine activity.
    /// </summary>
    public class HelloWorld : IWorkflow
    {
        public void Build(IWorkflowBuilder builder) => builder.WriteLine("Hello ...World!");
    }
}