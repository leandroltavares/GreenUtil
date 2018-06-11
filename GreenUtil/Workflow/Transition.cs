using System;

namespace GreenUtil.Workflow
{
    /// <summary>
    /// Classe para representar as transições entre os estados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Transition<T> : IEquatable<Transition<T>>
    {
        /// <summary>
        /// Status de origem (anterior)
        /// </summary>
        public string PreviousState { get; protected set; }

        /// <summary>
        /// Status de destino (próximo)
        /// </summary>
        public string NextState { get; protected set; }

        /// <summary>
        /// Regra de validação adicional para a transição
        /// </summary>
        public IValidator<T> Validator { get; protected set; }

        internal Transition(string previousState, string nextState, IValidator<T> validator = null)
        {
            this.PreviousState = previousState;
            this.NextState = nextState;
            this.Validator = validator;



            //if (this.PreviousState == null)
            //    throw new ArgumentNullException(nameof(previousState));

            //if (this.NextState == null)
            //    throw new ArgumentNullException(nameof(nextState));
        }

        public override int GetHashCode()
        {
            //tratativa para valores nullos, usa o max value
            return PreviousState?.GetHashCode() ?? 1 >> NextState?.GetHashCode() ?? 1;
        }

        public bool Equals(Transition<T> other)
        {
            if (other == null || this.PreviousState != other.PreviousState || this.NextState != other.NextState)
                return false;

            return true;
        }
    }
}
