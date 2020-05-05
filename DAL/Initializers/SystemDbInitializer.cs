using System;
using System.Data.Entity;
using DAL.Entities;
using DAL.Enums;

namespace DAL.Initializers
{
    public class SystemDbInitializer : DropCreateDatabaseAlways<DbContextPV179>
    {
        protected override void Seed(DbContextPV179 context)
        {
            var applicant1 = new Applicant
            {
                Id = Guid.Parse("aa05dc64-5c07-40fe-a916-175165b9b90f"),
                Username = "Robin",
                PasswordHash = "xyegje2SXESF9gXsiP1Uec5TiTw=", // Heslo: robinrobin
                PasswordSalt = "NM9IIMkcZyihpUClndUzBg==",
                Email = "robin.mutina@gmail.com",
                Tel = "+420999888999",
                FirstName = "Robin",
                LastName = "Mutina",
                Education = "Bachelor degree - Masaryk University, Business Informatics",
                Skills = "Intermediate level of C# and Java, fluent in English",
                Experience = "3 years of experience in programming",
                Roles = "Applicant"
            };

            var applicant2 = new Applicant
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Username = "Klára",
                PasswordHash = "x",
                PasswordSalt = "x",
                Email = "klara.minsterova@gmail.com",
                Tel = "+420999777999",
                FirstName = "Klára",
                LastName = "Minsterová",
                Education = "Bachelor degree",
                Skills = "Knowledge of C#, Java and SQL, fluent in English, beginner in Russian",
                Experience = "3 years of experience in programming",
                Roles = "Applicant"
            };

            var applicant3 = new Applicant
            {
                Id = Guid.NewGuid(),
                Username = "Ondra",
                PasswordHash = "x",
                PasswordSalt = "x",
                Email = "ondrej.melichar@gmail.com",
                Tel = "+420777666555",
                FirstName = "Ondřej",
                LastName = "Melichar",
                Education = "Master degree - Brno University of Technology",
                Skills = "English, German, presentation skills",
                Experience = "5 years of experience in telecommunications",
                Roles = "Applicant"
            };

            context.Applicants.Add(applicant1);
            context.Applicants.Add(applicant2);
            context.Applicants.Add(applicant3);

            var keywords1 = new KeywordsApplicant
            {
                Id = Guid.NewGuid(),
                ApplicantId = applicant1.Id,
                Keyword = Keyword.IT
            };

            var keywords2 = new KeywordsApplicant
            {
                Id = Guid.NewGuid(),
                ApplicantId = applicant1.Id,
                Keyword = Keyword.Finance
            };

            var keywords3 = new KeywordsApplicant
            {
                Id = Guid.NewGuid(),
                ApplicantId = applicant2.Id,
                Keyword = Keyword.IT
            };

            var keywords4 = new KeywordsApplicant
            {
                Id = Guid.NewGuid(),
                ApplicantId = applicant2.Id,
                Keyword = Keyword.Administration
            };

            var keywords5 = new KeywordsApplicant
            {
                Id = Guid.NewGuid(),
                ApplicantId = applicant3.Id,
                Keyword = Keyword.Telecommunications
            };

            context.KeywordsApplicants.Add(keywords1);
            context.KeywordsApplicants.Add(keywords2);
            context.KeywordsApplicants.Add(keywords3);
            context.KeywordsApplicants.Add(keywords4);
            context.KeywordsApplicants.Add(keywords5);

            var company1 = new Company
            {
                Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                Username = "uniscomp",
                PasswordHash = "xyegje2SXESF9gXsiP1Uec5TiTw=", // Heslo: robinrobin
                PasswordSalt = "NM9IIMkcZyihpUClndUzBg==",
                Name = "Unis Computers",
                Address = "Jundrovská 31, Brno",
                Email = "info@uniscomp.cz",
                Tel = "+420123456789",
                Roles = "Company"
            };

            var company2 = new Company
            {
                Id = Guid.Parse("ed010198-ee81-4c71-ada5-b8e11601ad50"),
                Username = "muni",
                PasswordHash = "x",
                PasswordSalt = "x",
                Name = "Masaryk University",
                Address = "Žerotínovo nám. 9, Brno",
                Email = "info@muni.cz",
                Tel = "+420123456789",
                Roles = "Company"
            };

            var company3 = new Company
            {
                Id = Guid.Parse("ed010198-fa81-4c71-ada5-b8e11601ad50"),
                Username = "tieto",
                PasswordHash = "x",
                PasswordSalt = "x",
                Name = "Tieto Czech",
                Address = "28. října 91, Ostrava",
                Email = "info@tieto.cz",
                Tel = "+420123456789",
                Roles = "Company"
            };

            context.Companies.Add(company1);
            context.Companies.Add(company2);
            context.Companies.Add(company3);

            var jobOffer1 = new JobOffer
            {
                Id = Guid.Parse("ac91dc64-5c07-40fe-a916-175165b9b90f"),
                CompanyId = company1.Id,
                Location = "Brno",
                Position = "C# developer",
                Description = "Company with 20 years of experience in ERP system development is looking for new talents."
            };

            var jobOffer2 = new JobOffer
            {
                Id = Guid.Parse("0a8fad5b-d9cb-469f-a165-70867728950e"),
                CompanyId = company2.Id,
                Location = "Brno",
                Position = "Lecturer",
                Description = "Do you have expert knowledge and want to share it with others? We will give you an opportunity for that."
            };

            var jobOffer3 = new JobOffer
            {
                Id = Guid.Parse("0f8ffc5b-d9cb-469f-a165-70867728950e"),
                CompanyId = company2.Id,
                Location = "Brno",
                Position = "Accountant",
                Description = "University is looking for new accountant. Great opportunity for fresh graduates."
            };

            var jobOffer4 = new JobOffer
            {
                Id = Guid.Parse("ac91dc64-5c07-40fe-a916-175689b9b90f"),
                CompanyId = company1.Id,
                Location = "Brno",
                Position = "Tester",
                Description = "Company with 20 years of experience in ERP system development is looking for new talents."
            };

            var jobOffer5 = new JobOffer
            {
                Id = Guid.Parse("bd91dc64-5d07-40fe-a916-175689b9b90f"),
                CompanyId = company3.Id,
                Location = "Ostrava",
                Position = "Assistant in HR department",
                Description = "Our HR department needs your help."
            };

            var jobOffer6 = new JobOffer
            {
                Id = Guid.Parse("bd91dc64-5d07-40fe-a916-234689b9b90f"),
                CompanyId = company3.Id,
                Location = "Ostrava",
                Position = "Java developer",
                Description = "Become part of Java team in well-known international company."
            };

            context.JobOffers.Add(jobOffer1);
            context.JobOffers.Add(jobOffer2);
            context.JobOffers.Add(jobOffer3);
            context.JobOffers.Add(jobOffer4);
            context.JobOffers.Add(jobOffer5);
            context.JobOffers.Add(jobOffer6);

            var keywordsJobOffer1 = new KeywordsJobOffer
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer1.Id,
                Keyword = Keyword.IT
            };

            var keywordsJobOffer2 = new KeywordsJobOffer
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer2.Id,
                Keyword = Keyword.Education
            };

            var keywordsJobOffer3 = new KeywordsJobOffer
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer3.Id,
                Keyword = Keyword.Finance
            };

            var keywordsJobOffer4 = new KeywordsJobOffer
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer4.Id,
                Keyword = Keyword.IT
            };

            var keywordsJobOffer5 = new KeywordsJobOffer
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer5.Id,
                Keyword = Keyword.HR
            };

            var keywordsJobOffer6 = new KeywordsJobOffer
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer5.Id,
                Keyword = Keyword.Administration
            };

            var keywordsJobOffer7 = new KeywordsJobOffer
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer6.Id,
                Keyword = Keyword.IT
            };

            context.KeywordsJobOffers.Add(keywordsJobOffer1);
            context.KeywordsJobOffers.Add(keywordsJobOffer2);
            context.KeywordsJobOffers.Add(keywordsJobOffer3);
            context.KeywordsJobOffers.Add(keywordsJobOffer4);
            context.KeywordsJobOffers.Add(keywordsJobOffer5);
            context.KeywordsJobOffers.Add(keywordsJobOffer6);
            context.KeywordsJobOffers.Add(keywordsJobOffer7);

            var application1 = new Application
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer1.Id,
                ApplicantId = applicant1.Id,
                Text = @"I would like to express my interest in your job offer. 
                         I already have working experience on similar position
                         and I believe I could use my skills for the benefit of your company.
                         Thank you for your time and consideration.",
                State = ApplicationState.Undecided
            };

            var application2 = new Application
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer1.Id,
                ApplicantId = applicant2.Id,
                Text = "Please hire me, I deserve it.",
                State = ApplicationState.Accepted
            };

            var application3 = new Application
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer6.Id,
                ApplicantId = applicant1.Id,
                Text = @"I would like to express my interest in your job offer. 
                         I already have working experience on similar position
                         and I believe I could use my skills for the benefit of your company.
                         Thank you for your time and consideration.",
                State = ApplicationState.Rejected
            };

            context.Applications.Add(application1);
            context.Applications.Add(application2);
            context.Applications.Add(application3);

            var favoriteJob1 = new FavoriteJob
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOffer3.Id,
                ApplicantId = applicant1.Id
            };

            context.FavoriteJobs.Add(favoriteJob1);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
