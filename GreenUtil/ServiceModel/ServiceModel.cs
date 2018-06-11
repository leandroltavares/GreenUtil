using System;
//using System.ServiceModel;
using System.Threading;

namespace GreenUtil.ServiceModel
{
    //TODO: Leandro
    ///// <summary>
    ///// Classe responsável por encapsular o consumo de um WebService
    ///// </summary>
    ///// <typeparam name="TClient">Tipo do cliente a executar (proxy)</typeparam>
    ///// <typeparam name="TService">Tipo de serviço (interface do contrato)</typeparam>
    //public class ServiceUtil<TClient, TService> where TClient : ClientBase<TService>, ICommunicationObject, IDisposable, new() where TService : class
    //{
    //    private static string endpointConfigurationName;

    //    private static string remoteAddress;

    //    private static int intervaloTentativas = 1000;

    //    /// <summary>
    //    /// Mutex
    //    /// </summary>
    //    private static readonly object mutex = new object();

    //    /// <summary>
    //    /// Define uma configuração global para todas as chamadas
    //    /// </summary>
    //    /// <param name="endpointConfigurationName">Nome do endpoint a ser utilizado</param>
    //    /// <param name="remoteAddress">Endereço do serviço</param>
    //    /// <param name="intervaloTentativas">Intervalo de tempo entre as tentativa (ms)</param>
    //    public static void SetBaseConfiguration(string endpointConfigurationName, string remoteAddress, int intervaloTentativas)
    //    {
    //        lock (mutex)
    //        {
    //            ServiceUtil<TClient, TService>.endpointConfigurationName = endpointConfigurationName;
    //            ServiceUtil<TClient, TService>.remoteAddress = remoteAddress;
    //            ServiceUtil<TClient, TService>.intervaloTentativas = intervaloTentativas;
    //        }
    //    }

    //    /// <summary>
    //    /// Executa um método do serviço que possui retorno
    //    /// </summary>
    //    /// <typeparam name="TResult">Tipo do retorno</typeparam>
    //    /// <param name="method">Método a ser executado</param>
    //    /// <param name="endpointConfigurationName">Nome do endpoint a ser usado</param>
    //    /// <param name="remoteAddress">Endereço remoto</param>
    //    /// <param name="tentativas">Quantidade de tentativas</param>
    //    /// <returns>Retorno do método</returns>
    //    public static TResult Execute<TResult>(Func<TClient, TResult> method, int tentativas, string endpointConfigurationName = null, string remoteAddress = null)
    //    {
    //        return ExecuteServiceMethod<Func<TClient, TResult>, TResult>(method, tentativas, endpointConfigurationName, remoteAddress);
    //    }

    //    /// <summary>
    //    /// Executa um método do serviço que não possui retorno
    //    /// </summary>
    //    /// <param name="method">Método a ser executado</param>
    //    /// <param name="tentativas">Quantidade de tentativas</param>
    //    /// <param name="endpointConfigurationName">Nome do endpoint a ser usado</param>
    //    public static void Execute(Action<TClient> method, int tentativas, string endpointConfigurationName = null, string remoteAddress = null)
    //    {
    //        ExecuteServiceMethod<Action<TClient>, object>(method, tentativas, endpointConfigurationName, remoteAddress);
    //    }

    //    /// <summary>
    //    /// Executa um metodo na proxy
    //    /// </summary>
    //    /// <typeparam name="TMethod">Método</typeparam>
    //    /// <typeparam name="TResult">Resultado</typeparam>
    //    /// <param name="proxy">Proxy</param>
    //    /// <param name="method">Método obj</param>
    //    /// <returns>Retorno do método caso tenha</returns>
    //    private static TResult ProxyExecute<TMethod, TResult>(TClient proxy, object method)
    //    {
    //        lock (mutex)
    //        {
    //            try
    //            {
    //                proxy.Open();

    //                if (typeof(TMethod) == typeof(Action<TClient>))
    //                    ((Action<TClient>)method)(proxy);

    //                else if (typeof(TMethod) == typeof(Func<TClient, TResult>))
    //                    return ((Func<TClient, TResult>)method)(proxy);

    //                else
    //                    throw new FormatException("Formato de método de serviço a ser executado não suportado.");

    //                proxy.Close();

    //                return default(TResult);
    //            }
    //            catch (Exception ex)
    //            {
    //                proxy.Abort();

    //                throw ex;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Obtem a instancia do client
    //    /// </summary>
    //    /// <param name="endpointConfigurationName">Nome do endpoint</param>
    //    /// <param name="remoteAddress">Endereço</param>
    //    /// <returns></returns>
    //    private static TClient GetInstance(string endpointConfigurationName, string remoteAddress)
    //    {
    //        if (endpointConfigurationName == null)
    //            endpointConfigurationName = ServiceUtil<TClient, TService>.endpointConfigurationName;

    //        if (remoteAddress == null)
    //            remoteAddress = ServiceUtil<TClient, TService>.remoteAddress;

    //        return (TClient)Activator.CreateInstance(typeof(TClient), endpointConfigurationName, remoteAddress);
    //    }

    //    /// <summary>
    //    /// Executa um método do serviço que possui ou não retorno
    //    /// </summary>
    //    /// <typeparam name="TMethod">Tipo do método a ser executado. (<see cref="Action&lt;GreenDocServiceClient&gt;" /> ou <see cref="Func&lt;GreenDocServiceClient, TResult&gt;"/>)</typeparam>
    //    /// <typeparam name="TResult">tipo do retorno</typeparam>
    //    /// <param name="endpointConfigurationName">Nome do endpoint</param>
    //    /// <param name="remoteAddress">Endereço</param>
    //    /// <returns></returns>
    //    private static TResult ExecuteServiceMethod<TMethod, TResult>(object method, int tentativas, string endpointConfigurationName = null, string remoteAddress = null )
    //    {
    //        Exception ultimaexcecao = null;

    //        using (var proxy = GetInstance(endpointConfigurationName, remoteAddress))
    //        {
    //            try
    //            {
    //                return ProxyExecute<TMethod, TResult>(proxy, method);
    //            }
    //            catch (Exception ex)
    //            {
    //                ultimaexcecao = ex;

    //                Thread.Sleep(intervaloTentativas);

    //                return ExecuteServiceMethod<TMethod, TResult>(method, --tentativas, endpointConfigurationName, remoteAddress);

    //            }
    //        }

    //        throw ultimaexcecao;
    //    }

    //}
}
