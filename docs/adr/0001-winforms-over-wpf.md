# WinForms over WPF

The project spec described WPF, but the existing codebase uses WinForms. We're keeping WinForms.

WinForms is simpler for a study project — no XAML, no MVVM complexity, no dependency injection boilerplate for views. The trade-off is less modern UI and no data-binding magic, but that's fine for a library management tool where Forms are mostly grids and dialogs.

EF Core will replace Dapper for the data layer, giving us code-first migrations and LINQ queries without changing the UI framework.
