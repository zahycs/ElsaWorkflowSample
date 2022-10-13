using Elsa.Activities.Console;
using Elsa.Builders;

namespace ElsaWorkflowSample
{
    public class MainWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder.StartWith<WriteActivity>() 
            .Then<ReadActivity>();
        }
    }
}