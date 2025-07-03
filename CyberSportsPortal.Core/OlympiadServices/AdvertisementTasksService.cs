using System;
using System.Collections.Generic;
using CyberSportsPortal.Data.Entities;

public class AdvertisementTasksService
{
    public List<KeyValuePair<int, int>> GetProbabilities(List<AdvertisingCompany> companies)
    {
        var currentYear = DateTime.UtcNow.Year;
        var startDate = new DateTime(currentYear, 1, 1);
        var endDate = new DateTime(currentYear, 12, 31, 23, 59, 59);

        var paymentsPerCompany = new List<KeyValuePair<int, decimal>>();
        decimal totalPayments = 0m;

        foreach (var company in companies)
        {
            decimal yearlySum = 0m;

            foreach (var payment in company.AdvertisementPaymentInfos)
            {
                if (payment.PaymentDate >= startDate && payment.PaymentDate <= endDate)
                {
                    yearlySum += payment.PaymentSum;
                }
            }

            paymentsPerCompany.Add(new KeyValuePair<int, decimal>(company.Id, yearlySum));
            totalPayments += yearlySum;
        }

        var probabilities = new List<KeyValuePair<int, int>>();

        if (totalPayments == 0)
        {
            foreach (var company in companies)
            {
                probabilities.Add(new KeyValuePair<int, int>(company.Id, 1));
            }
            return probabilities;
        }

        foreach (var companyPayment in paymentsPerCompany)
        {
            decimal percentage = (companyPayment.Value / totalPayments) * 100;
            int probability = (int)Math.Floor(percentage);
            if (probability < 1)
                probability = 1;

            probabilities.Add(new KeyValuePair<int, int>(companyPayment.Key, probability));
        }

        return probabilities;
    }
}