using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using Elsa.Attributes;
using Elsa.Expressions;  

namespace ElsaWorkflowSample
{
    public class ReadActivity : Activity
    {
        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine($"`Hello Elsa!` from : {nameof(ReadActivity)}");
            return Done();
        } 
    }
}