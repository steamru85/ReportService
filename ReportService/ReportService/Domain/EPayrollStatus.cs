using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts.Payroll
{
    public enum EPayrollStatus
    {
        //Черновик
        Draft,
        //На подписи
        InSigning,
        //В обработке
        SendToBank,
        //Исполнен
        Executed,
        //Отказан
        Rejected,
        //Частично исполнен
        PartiallyExecuted
    }
}