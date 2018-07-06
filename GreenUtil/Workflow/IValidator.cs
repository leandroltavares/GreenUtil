namespace GreenUtil.Workflow
{
    /// <summary>
    /// Validation interface using a data container
    /// </summary>
    /// <typeparam name="T">Data container type</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validate if the data in the container is valid
        /// </summary>
        /// <param name="container">The data container</param>
        /// <param name="message">The validation message</param>
        /// <returns>True if thata is valid, false otherwise</returns>
        bool Validate(T container, ref string message);
    }
}
