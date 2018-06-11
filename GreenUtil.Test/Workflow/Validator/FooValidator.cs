using GreenUtil.Test.Dummy;
using GreenUtil.Workflow;

namespace GreenUtil.Test.Workflow.Validator
{
    public class FooValidator : IValidator<Foo>
    {
        public bool Validate(Foo container, ref string message)
        {
            if (container.IntProp == 42 && container.DecimalProp == 3.14M && container.StringProp == "This is a test")
                return true;
            else
                return false;
        }
    }
}
