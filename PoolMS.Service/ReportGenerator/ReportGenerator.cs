using PoolMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace PoolMS.Service.ReportGenerator
{
    public class ReportGenerator
    {
        public static string GeneratePoolUsageReport(Pool pool)
        {
            var filePath = System.IO.Path.GetTempFileName();

            using (DocX document = DocX.Create(filePath))
            {
                document.InsertParagraph("Pool Usage Report").FontSize(20).SpacingAfter(50);

                foreach (var visit in pool.Visits)
                {
                    document.InsertParagraph($"Visit ID: {visit.Id}, Date: {visit.Date}, Stay Time: {visit.StayTime}, User ID: {visit.User.Id}, Pool ID: {visit.Pool.Id}");
                }

                document.Save();
            }

            return filePath;
        }
        public static void GenerateIncomeReport(List<Payment> payments, string filePath)
        {
            using (DocX document = DocX.Create(filePath))
            {
                document.InsertParagraph("Income Report").FontSize(20).SpacingAfter(50);

                foreach (var payment in payments)
                {
                    document.InsertParagraph($"Payment ID: {payment.Id}, User ID: {payment.User.Id}, Amount: {payment.Amount}, Payment Day: {payment.PaymentDay}");
                }

                document.Save();
            }
        }
        public static void GenerateMonthlyIncomeReport(List<Payment> payments, string filePath)
        {
            var monthlyIncome = payments
                .GroupBy(p => new { p.PaymentDay.Year, p.PaymentDay.Month })
                .Select(g => new { Date = new DateTime(g.Key.Year, g.Key.Month, 1), TotalIncome = g.Sum(p => p.Amount) })
                .OrderBy(d => d.Date)
                .ToList();

            using (DocX document = DocX.Create(filePath))
            {
                document.InsertParagraph("Monthly Income Report").FontSize(20).SpacingAfter(50);

                foreach (var income in monthlyIncome)
                {
                    document.InsertParagraph($"Date: {income.Date:yyyy-MM}, Total Income: {income.TotalIncome}");
                }

                document.Save();
            }
        }
    }
}
