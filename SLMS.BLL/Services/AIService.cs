using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.Shared.DTOs.AI;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
namespace SLMS.BLL.Services;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IConfiguration _configuration;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILibraryResourceRepository _libraryResourceRepository;
    public AIService(
        HttpClient httpClient,
        IDashboardRepository dashboardRepository,
        IEmployeeRepository employeeRepository,
        ILibraryResourceRepository libraryResourceRepository,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _dashboardRepository = dashboardRepository;
        _employeeRepository = employeeRepository;
        _libraryResourceRepository = libraryResourceRepository;
        _configuration = configuration;
    }

    public async Task<AIResponseDto> AskAsync(AIRequestDto request)
    {
        var dashboard =
            await _dashboardRepository.GetDashboardAsync();

        var analytics =
            await _dashboardRepository.GetAnalyticsAsync();
        string question =
    request.Question.ToLower().Trim();

        // =======================================================
        // Greetings
        // =======================================================

        if (question == "hi" ||
            question == "hello" ||
            question == "hey" ||
            question == "good morning" ||
            question == "good afternoon" ||
            question == "good evening")
        {
            return new AIResponseDto
            {
                Answer =
        @"👋 Hello!

I am the SECON Library AI Assistant.

I can help you with:

📊 Dashboard Summary

📚 Library Resources

👨 Employees

🏢 Departments

📖 Digital Library

📈 Analytics

💡 Recommendations"
            };
        }

        // =======================================================
        // Dashboard Summary
        // =======================================================

        if (question.Contains("dashboard") ||
            question.Contains("summary") ||
            question.Contains("overview") ||
            question.Contains("statistics"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"📊 LIBRARY DASHBOARD

🏢 Departments : {dashboard.TotalDepartments}

👨 Employees : {dashboard.TotalEmployees}

📚 Resources : {dashboard.TotalResources}

📂 Categories : {dashboard.TotalCategories}

🗄 Shelves : {dashboard.TotalShelves}

📖 Issued Books : {dashboard.TotalIssuedBooks}

✅ Returned Books : {dashboard.TotalReturnedBooks}

⚠ Overdue Books : {dashboard.TotalOverdueBooks}

💻 Digital Contents : {dashboard.TotalDigitalContents}

📥 Requests : {dashboard.TotalRequests}

👥 Users : {dashboard.TotalUsers}"
            };
        }

        // =======================================================
        // Departments
        // =======================================================

        if (
        question.Contains("department") ||
        question.Contains("departments") ||
        question.Contains("division"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"🏢

There are currently

{dashboard.TotalDepartments}

departments in the library."
            };
        }
        // =======================================================
        // Employee Details
        // =======================================================

        var employees = await _employeeRepository.GetAllAsync();

        var employee = employees.FirstOrDefault(e =>
            question.Contains(e.FullName.ToLower()));

        if (employee != null)
        {
            return new AIResponseDto
            {
                Answer =
        $@"👨 EMPLOYEE DETAILS

Employee Number : {employee.EmployeeNumber}

Name : {employee.FullName}

Email : {employee.Email}

Phone : {employee.Phone}

Designation : {employee.Designation}

Department : {employee.Department?.DepartmentName}"
            };
        }
        // =======================================================
        // Employees
        // =======================================================

        if (
        question.Contains("employee") ||
        question.Contains("employees") ||
        question.Contains("staff"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"👨

There are currently

{dashboard.TotalEmployees}

employees registered."
            };
        }

        // =======================================================
        // Categories
        // =======================================================

        if (
        question.Contains("category") ||
        question.Contains("categories"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"📂

There are

{dashboard.TotalCategories}

categories available."
            };
        }

        // =======================================================
        // Book Details
        // =======================================================
        var books = await _libraryResourceRepository.GetAllWithShelfAsync();
        var book = books.FirstOrDefault(b =>
            question.Contains(b.Title.ToLower()));

        if (book != null)
        {
            return new AIResponseDto
            {
                Answer =
        $@"📚 BOOK DETAILS

Title : {book.Title}

Author : {book.Author}

Category : {book.Category?.Name}

Shelf : {book.Shelf?.ShelfName}

Publisher : {book.Publisher}"
            };
        }
        // =======================================================
        // Books / Resources
        // =======================================================
        bool isBookCount =
            question.Contains("how many books") ||
            question.Contains("total books") ||
            question.Contains("book count") ||
            question == "books" ||
            question == "resources";

        if (isBookCount)
        {
            return new AIResponseDto
            {
                Answer =
        $@"📚

The library currently contains

{dashboard.TotalResources}

library resources."
            };
        }

        // =======================================================
        // Shelves
        // =======================================================

        if (
        question.Contains("shelf") ||
        question.Contains("shelves"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"🗄

There are

{dashboard.TotalShelves}

shelves available."
            };
        }
        // =======================================================
        // Issued Books
        // =======================================================

        if (
        question.Contains("issued") ||
        question.Contains("issue") ||
        question.Contains("borrowed"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"📖

Currently

{dashboard.TotalIssuedBooks}

books are issued."
            };
        }

        // =======================================================
        // Returned Books
        // =======================================================

        if (
        question.Contains("returned") ||
        question.Contains("return"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"✅

A total of

{dashboard.TotalReturnedBooks}

books have been returned."
            };
        }

        // =======================================================
        // Overdue Books
        // =======================================================

        if (
        question.Contains("overdue") ||
        question.Contains("late") ||
        question.Contains("pending"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"⚠

There are currently

{dashboard.TotalOverdueBooks}

overdue books."
            };
        }

        // =======================================================
        // Digital Library
        // =======================================================

        if (
        question.Contains("digital") ||
        question.Contains("ebook") ||
        question.Contains("e-book") ||
        question.Contains("pdf") ||
        question.Contains("content"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"💻

The Digital Library contains

{dashboard.TotalDigitalContents}

digital contents."
            };
        }

        // =======================================================
        // Requests
        // =======================================================

        if (
        question.Contains("request") ||
        question.Contains("requests"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"📥

There are

{dashboard.TotalRequests}

pending requests."
            };
        }

        // =======================================================
        // Users
        // =======================================================

        if (
        question.Contains("user") ||
        question.Contains("users") ||
        question.Contains("login"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"👥

There are

{dashboard.TotalUsers}

registered users."
            };
        }

        // =======================================================
        // Audit Logs
        // =======================================================

        if (
        question.Contains("audit") ||
        question.Contains("logs") ||
        question.Contains("history"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"📋

The system contains

{dashboard.TotalAuditLogs}

audit log records."
            };
        }
        // =======================================================
        // Most Borrowed Book
        // =======================================================

        if (
        question.Contains("most borrowed") ||
        question.Contains("popular book") ||
        question.Contains("top book") ||
        question.Contains("highest borrowed"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"📚 Most Borrowed Book

{analytics.MostBorrowedBook}"
            };
        }

        // =======================================================
        // Least Borrowed Book
        // =======================================================

        if (
        question.Contains("least borrowed") ||
        question.Contains("lowest borrowed") ||
        question.Contains("rarely borrowed"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"📕 Least Borrowed Book

{analytics.LeastBorrowedBook}"
            };
        }

        // =======================================================
        // Most Active Employee
        // =======================================================

        if (
    question.Contains("active employee") ||
    question.Contains("most active") ||
    question.Contains("top employee") ||
    question.Contains("best employee") ||
    question.Contains("employee performance") ||
    question.Contains("employe"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"👨 Most Active Employee

{analytics.MostActiveEmployee}"
            };
        }

        // =======================================================
        // Most Active Department
        // =======================================================

        if (
        question.Contains("active department") ||
        question.Contains("top department") ||
        question.Contains("best department"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"🏢 Most Active Department

{analytics.MostActiveDepartment}"
            };
        }

        // =======================================================
        // Recommendation
        // =======================================================

        if (
        question.Contains("recommend") ||
        question.Contains("suggest") ||
        question.Contains("improve"))
        {
            return new AIResponseDto
            {
                Answer =
        $@"💡 AI Recommendation

{analytics.Recommendation}"
            };
        }

        // =======================================================
        // Library Health
        // =======================================================

        if (
        question.Contains("health") ||
        question.Contains("status") ||
        question.Contains("performance"))
        {
            string status;

            if (dashboard.TotalOverdueBooks == 0)
                status = "🟢 Excellent";

            else if (dashboard.TotalOverdueBooks <= 5)
                status = "🟡 Good";

            else
                status = "🔴 Needs Attention";

            return new AIResponseDto
            {
                Answer =
        $@"📊 Library Health

Overall Status

{status}

Overdue Books : {dashboard.TotalOverdueBooks}

Issued Books : {dashboard.TotalIssuedBooks}

Resources : {dashboard.TotalResources}"
            };
        }

        // =======================================================
        // AI Introduction
        // =======================================================

        if (
        question.Contains("who are you") ||
        question.Contains("what can you do"))
        {
            return new AIResponseDto
            {
                Answer =
        @"🤖 I am the SECON Library AI Assistant.

I can help you with:

• Dashboard statistics

• Library analytics

• Book information

• Employees

• Departments

• Recommendations

• Digital Library

• Reports"
            };
        }
        if (
question.Contains("sudeep") ||
question.Contains("borrowed by") ||
question.Contains("issued to") ||
question.Contains("who borrowed"))
        {
            return new AIResponseDto
            {
                Answer =
        @"📚 I don't yet have access to individual book issue records.

Currently I can answer dashboard statistics and analytics.

Support for employee-wise book tracking will be added in the next version."
            };
        }
        var prompt = $"""
You are SECON AI Assistant.

Answer library questions using the provided library statistics.

If the question is unrelated to the library,
answer using your general knowledge.

Library Data

Departments : {dashboard.TotalDepartments}
Employees : {dashboard.TotalEmployees}
Resources : {dashboard.TotalResources}
Issued Books : {dashboard.TotalIssuedBooks}
Returned Books : {dashboard.TotalReturnedBooks}
Overdue Books : {dashboard.TotalOverdueBooks}

Most Borrowed Book : {analytics.MostBorrowedBook}
Most Active Department : {analytics.MostActiveDepartment}

Recommendation :

{analytics.Recommendation}

Question:

{request.Question}
""";

        try
        {
            var apiKey = _configuration["Gemini:ApiKey"];

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            var body = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = prompt
                    }
                }
            }
        }
            };

            var json = JsonSerializer.Serialize(body);

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(content);

            var answer = document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return new AIResponseDto
            {
                Answer = answer ?? "No response generated."
            };
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("429"))
            {
                return new AIResponseDto
                {
                    Answer = "⚠ General AI is temporarily unavailable due to API rate limits. Please try again later. Library queries continue to work."
                };
            }

            return new AIResponseDto
            {
                Answer = $"⚠ AI Error: {ex.Message}"
            };
        }


    }
}
  