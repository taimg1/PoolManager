using PoolMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace PoolMS.Service.ReportGenerator
{
    public class ReportGenerator
    {
        public static MemoryStream GeneratePoolUsageReport(Pool pool)
        {
            var stream = new MemoryStream();
            IEnumerable<Visit> visits = pool.Visits;

            using (DocX document = DocX.Create(stream))
            {
                document.InsertParagraph($"Pool Usage Report id: {pool.Id}").FontSize(20).SpacingAfter(50);
                foreach (var visit in visits)
                {
                    var paragraphText = $"Visit ID: {visit.Id}, User ID: {visit.User.Id}, Date: {visit.Date}; StayTime: {visit.StayTime}";

                    Console.WriteLine(paragraphText);
                    document.InsertParagraph(paragraphText);
                }

                document.Save();
            }

            stream.Position = 0;
            return stream;
        }

        public static MemoryStream GenerateIncomeReport(Payment payment)
        {
            var stream = new MemoryStream();

            using (DocX document = DocX.Create(stream))
            {
                document.InsertParagraph("Payment Report").FontSize(20).SpacingAfter(50);

              
                document.InsertParagraph($"Payment ID: {payment.Id}, User ID: {payment.User.Id}, Amount: {payment.Amount}, Payment Day: {payment.PaymentDay}");
                

                document.Save();
            }

            stream.Position = 0;
            return stream;
        }

        public static MemoryStream GenerateMonthlyIncomeReport(List<Payment> payments)
        {
            var stream = new MemoryStream();
            var monthlyIncome = payments
                .GroupBy(p => new { p.PaymentDay.Year, p.PaymentDay.Month })
                .Select(g => new { Date = new DateTime(g.Key.Year, g.Key.Month, 1), TotalIncome = g.Sum(p => p.Amount) })
                .OrderBy(d => d.Date)
                .ToList();

            using (DocX document = DocX.Create(stream))
            {
                document.InsertParagraph("Monthly Income Report").FontSize(20).SpacingAfter(50);

                foreach (var income in monthlyIncome)
                {
                    document.InsertParagraph($"Date: {income.Date:yyyy-MM}, Total Income: {income.TotalIncome}");
                }

                document.Save();
            }

            stream.Position = 0;
            return stream;
        }

       
    }
}
