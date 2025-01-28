namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Define o contrato para representação de um objeto sale no sistema.
    /// </summary>
    public interface ISale
    {
        /// <summary>
        /// Obtém o identificador único do objeto sale.
        /// </summary>
        /// <returns>O ID como uma string.</returns>
        public string Id { get; }

        /// <summary>
        /// SaleNumber
        /// </summary>
        /// <returns>SaleNumber.</returns>
        public string SaleNumber { get; }

        /// <summary>
        /// Customer
        /// </summary>
        /// <returns>Customer</returns>
        public string Customer { get; }
    }
}
