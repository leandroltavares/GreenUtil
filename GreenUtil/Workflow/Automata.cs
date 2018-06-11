using GreenUtil.Assets;
using System.Collections.Generic;

namespace GreenUtil.Workflow
{
    /// <summary>
    /// Classe para representar um automato
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Automata<T> 
    {
        private Dictionary<Transition<T>, Transition<T>> transitions;

        /// <summary>
        /// Construtor do automato
        /// </summary>
        public Automata()
        {
            transitions = new Dictionary<Transition<T>, Transition<T>>();
        }

        /// <summary>
        /// Adiciona transições com base em um conjunto de status de origem e um conjunto de status de destino
        /// </summary>
        /// <param name="previousStates">Conjunto de status anteriores</param>
        /// <param name="nextStates">Conjunto de status seguintes</param>
        /// <param name="validator">Regra de validação a ser aplicada ao realizar a transição</param>
        public void AddTransition(string[] previousStates, string[] nextStates, IValidator<T> validator)
        {
            foreach(var statusAnterior in previousStates)
            {
                foreach(var statusNovo in nextStates)
                {
                    var transicao = new Transition<T>(statusAnterior, statusNovo, validator);

                    transitions.Add(transicao, transicao);
                }
            }
        }

        /// <summary>
        /// Adiciona transições com base em um status de origem e um status de destino
        /// </summary>
        /// <param name="previousState">Status anterior</param>
        /// <param name="nextState">Status seguinte</param>
        /// <param name="validator">Regra de validação a ser aplicada ao realizar a transição</param>
        public void AddTransition(string previousState, string nextState, IValidator<T> validator)
        {
            var transicao = new Transition<T>(previousState, nextState, validator);

            transitions.Add(transicao, transicao);
        }

        /// <summary>
        /// Avalia se uma transição é valida ou não
        /// </summary>
        /// <param name="container">Container de dados</param>
        /// <param name="previousState">Status anterior</param>
        /// <param name="nextState">Status seguinte</param>
        /// <param name="message">Mensagem de erro, caso a validação falhe</param>
        /// <returns>Verdadeiro se a transição é valida, falso caso contrário</returns>
        public bool Evaluate(T container, string previousState, string nextState, ref string message)
        {
            Transition<T> transicaoAtual;

            if(transitions.TryGetValue(new Transition<T>(previousState, nextState), out transicaoAtual))
            {
                if (transicaoAtual.Validator != null)
                {
                   return transicaoAtual.Validator.Validate(container, ref message);
                }
                else
                {
                    message = string.Empty;
                    return true;
                }
            }
            else
            {
                message = string.Format(Messages.AutomataInvalidTransition, nextState, previousState);
                return false;
            }
        }
    }
}
