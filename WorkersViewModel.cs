using System.Linq;
using Lab4.Models;

namespace Lab4.ViewModels
{
    public enum SortState
    {
        No,
        ModelAsc,
        ModelDesc,
        ColourAsc,
        ColourDesc,
        DateReceiptDesc,
        DateReceiptAsc,
        DateCompletionAsc,
        DateCompletionDesc,
        FioOwnerAsc,
        FioOwnerDesc,
        FioWorkerAsc,
        FioWorkerDesc,
        SalaryAsc,
        SalaryDesc,
        //DateAsc,
        //DateDesc,
        NameAsc,
        NameDesc
        //PriceAsc,
        //PriceDesc,
        //SurnameAsc,
        //SurnameDesc,

    }
    public class WorkersViewModel
    {
        public Worker WorkerViewModel { get; set; }
        public IQueryable<Worker> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}
