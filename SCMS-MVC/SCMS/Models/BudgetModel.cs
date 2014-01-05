using System;
using System.Collections.Generic;

namespace SCMS.Models
{
    public class BudgetMaster
    {
        public string MasterId { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string CalendarYear { get; set; }
        public string BudgetType { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string EnteredBy { get; set; }
        public string EnteredDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public int? BudgetTypeRevision { get; set; }
        public int Success { get; set; }
        public List<BudgetDetail> ListBudgetDetail { get; set; }
    }

    public class BudgetDetail
    {
        public string DetailId { get; set; }
        public string MasterId { get; set; }
        public string Account { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Month1 { get; set; }
        public decimal? Month2 { get; set; }
        public decimal? Month3 { get; set; }
        public decimal? Month4 { get; set; }
        public decimal? Month5 { get; set; }
        public decimal? Month6 { get; set; }
        public decimal? Month7 { get; set; }
        public decimal? Month8 { get; set; }
        public decimal? Month9 { get; set; }
        public decimal? Month10 { get; set; }
        public decimal? Month11 { get; set; }
        public decimal? Month12 { get; set; }
        public string Remarks { get; set; }
    }
}