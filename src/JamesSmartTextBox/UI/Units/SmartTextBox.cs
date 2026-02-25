using System.Windows;
using System.Windows.Controls;
using JamesSmartTextBox.Local.Validators;

namespace JamesSmartTextBox.UI.Units;

public class SmartTextBox : TextBox
{
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SmartTextBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(
            nameof(Placeholder),
            typeof(string),
            typeof(SmartTextBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(SmartTextBox),
            new PropertyMetadata(new CornerRadius(4)));

    public static readonly DependencyProperty ValidatorProperty =
        DependencyProperty.Register(
            nameof(Validator),
            typeof(IValidator),
            typeof(SmartTextBox),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IsValidProperty =
        DependencyProperty.Register(
            nameof(IsValid),
            typeof(bool),
            typeof(SmartTextBox),
            new PropertyMetadata(true));

    public static readonly DependencyProperty ErrorMessageProperty =
        DependencyProperty.Register(
            nameof(ErrorMessage),
            typeof(string),
            typeof(SmartTextBox),
            new PropertyMetadata(string.Empty));

    static SmartTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SmartTextBox),
            new FrameworkPropertyMetadata(typeof(SmartTextBox)));
    }

    public SmartTextBox()
    {
        TextChanged += OnTextChanged;
    }

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public IValidator? Validator
    {
        get => (IValidator?)GetValue(ValidatorProperty);
        set => SetValue(ValidatorProperty, value);
    }

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (Validator is null)
        {
            IsValid = true;
            ErrorMessage = string.Empty;
            return;
        }

        IsValid = Validator.Validate(Text);
        ErrorMessage = IsValid ? string.Empty : Validator.Message;
    }
}
