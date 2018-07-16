namespace CORE.Entities
{
    public enum EPayrollStatus
    {
        /// <summary>
        /// Черновик
        /// </summary>
        DRAFT,
        /// <summary>
        /// На подписи
        /// </summary>
        INSIGNING,
        /// <summary>
        /// В обработке
        /// </summary>
        SEND_TO_BANK,
        /// <summary>
        /// Исполнен
        /// </summary>
        EXECUTED,
        /// <summary>
        /// Отказан
        /// </summary>
        REJECTED,
        /// <summary>
        /// Частично исполнен
        /// </summary>
        PARTIALLY_EXECUTED
    }
}