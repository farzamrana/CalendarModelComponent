# CalendarModelComponent

A reusable ASP.NET Core model for selecting and validating Persian (Jalali) dates in forms.

## Features

- Supports single or range date pickers
- Custom validation for Persian date format
- Logical date range comparison (FromDate <= ToDate)
- Enforces rules like "date should be after today"
- Easily integrates into any ASP.NET Core MVC or Razor Pages project

## How to Use

1. Add the `CalendarModel.cs` file to your project
2. Create a form using `FromDate` and `ToDate` fields
3. Use the validation results in controller or Razor view

```csharp
[HttpPost]
public IActionResult Search(CalendarModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    // Proceed with valid dates
}
