using Elsa.ActivityResults;
using Elsa.Services;

namespace ElsaWorkflowSample
{
    public class WriteActivity : Activity
    {
        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine($"`Hello Elsa!` from : {nameof(WriteActivity)}");

            return Done();
        }
    }
}