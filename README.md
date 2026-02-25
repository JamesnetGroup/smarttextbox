# SmartTextBox

A WPF custom TextBox control with built-in input validation, built with Claude Opus 4.6.

[![.NET](https://img.shields.io/badge/.NET-10-512BD4)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-CustomControl-blue)](https://github.com/dotnet/wpf)
[![AI](https://img.shields.io/badge/AI-Claude%20Opus%204.6-orange)](https://claude.ai/)

## Overview

SmartTextBox is a reusable, extensible WPF CustomControl that inherits from TextBox with real-time input validation support. Validation logic is decoupled through the IValidator interface — swap the Validator in XAML, no control code changes required.

Built from scratch with 3 progressive prompts, each shorter than the last, because the architecture was right from the start.

## Project Structure

```
SmartTextBoxApp/
├── DemoApp/                             # WPF App — Validator demos
└── JamesSmartTextBox/                   # Class Library
    ├── Properties/AssemblyInfo.cs
    ├── Local/Validators/IValidator.cs   # IValidator interface + all Validators
    ├── Themes/Units/SmartTextBox.xaml   # ControlTemplate
    ├── Themes/Generic.xaml
    └── UI/Units/SmartTextBox.cs         # CustomControl (inherits TextBox)
```

## Core Design

**6 DependencyProperties:**

| Property | Type | Description |
|----------|------|-------------|
| Header | string | Label above the input field |
| Placeholder | string | Watermark text |
| CornerRadius | CornerRadius | Border corner radius |
| Validator | IValidator | Validation logic interface |
| IsValid | bool | Whether validation passed |
| ErrorMessage | string | Validation error message |

**Validation Flow:** TextChanged → Validator.Validate() → Update IsValid & ErrorMessage → ControlTemplate Triggers handle visual state

## Built-in Validators

| Validator | Description |
|-----------|-------------|
| RequiredValidator | Non-empty field check |
| EmailValidator | Email format (regex) |
| PasswordValidator | ≥8 chars, uppercase + lowercase + digit |
| UrlValidator | HTTP/HTTPS format |
| PassportValidator | 1-2 letters + 6-9 digits |
| CreditCardValidator | Luhn algorithm |

## Usage

```xml
<smart:SmartTextBox Header="Credit Card"
                    Placeholder="Enter card number"
                    Margin="0,0,0,16">
    <smart:SmartTextBox.Validator>
        <validators:CreditCardValidator/>
    </smart:SmartTextBox.Validator>
</smart:SmartTextBox>
```

To extend, simply implement IValidator — no changes to SmartTextBox required:

```csharp
public class PhoneValidator : IValidator
{
    public string Message { get; set; } = "Invalid phone number.";
    public bool Validate(string value) => Regex.IsMatch(value ?? "", @"^\+?[\d\-\s]{10,15}$");
}
```

## AI Prompts

### Step 1. Project Setup
```
Framework: .NET 10
Solution Name: SmartTextBoxApp
Output Format: ZIP file
Solution Structure:
- DemoApp (WPF App)
- JamesSmartTextBox (Class Library)
DemoApp:
- SmartTextBox examples in MainWindow.xaml
JamesSmartTextBox:
- SmartTextBox.cs (CustomControl, inherits TextBox)
- SmartTextBox.xaml (ResourceDictionary)
Project Structure:
1. UI/Units/SmartTextBox.cs
2. Themes/Units/SmartTextBox.xaml
3. Themes/Generic.xaml (Merge SmartTextBox.xaml)
Important:
- Include AssemblyInfo.cs
- Generic.xaml merges SmartTextBox.xaml
- SmartTextBox.cs sets DefaultStyleKey
```

### Step 2. Add Validation
```
SmartTextBox DependencyProperty (6):
- Header (string)
- PlaceHolder (string)
- CornerRadius (CornerRadius)
- Validator (IValidator)
- IsValid (bool)
- ErrorMessage (string)
IValidator:
- Message (string)
- Validate(string value) → bool
Behavior:
- Auto-validate on Text change, show red border and ErrorMessage on invalid
Validator setup:
- IValidator + 2 Validators in same file
- Path: JamesSmartTextBox/Local/Validators/IValidator.cs
DemoApp:
- Apply Validators in MainWindow.xaml
Important:
- Only 6 DPs
- No comments/regions
```

### Step 3. Built-in Validators
```
Add Validators inheriting IValidator:
- Email
- Password (min 8 chars, uppercase + lowercase + numbers)
- URL
- Passport
- CreditCard
DemoApp:
- Demonstrate all Validators in MainWindow.xaml
```

