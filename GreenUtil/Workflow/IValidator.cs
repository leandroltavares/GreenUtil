namespace GreenUtil.Workflow
{
    /// <summary>
    /// Interface para a regra de validação
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T>
    {
        bool Validate(T container, ref string message);
    }
}
