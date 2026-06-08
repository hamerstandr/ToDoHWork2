# ToDoH - Task Management Application

A simple and clean task management application built with WPF and .NET 6.

## Features

- **Multiple Work Lists**: Organize your tasks into different work lists
- **Task Management**: Add, complete, and delete individual tasks
- **Persistent Storage**: Your data is automatically saved to XML format
- **Clean UI**: Modern and intuitive user interface
- **No External Dependencies**: Built entirely with standard .NET libraries

## Requirements

- .NET 6.0 SDK or later
- Windows OS (WPF applications are Windows-only)

## Building the Project

```bash
cd ToDoHWork2
dotnet build
```

## Running the Application

```bash
dotnet run
```

## Project Structure

```
ToDoHWork2/
├── App.xaml              # Application definition
├── App.xaml.cs           # Application logic
├── MainWindow.xaml       # Main window UI
├── MainWindow.xaml.cs    # Main window logic
├── Page.xaml             # Task page UI
├── Page.xaml.cs          # Task page logic
├── InputDialog.xaml      # Input dialog UI
├── InputDialog.xaml.cs   # Input dialog logic
├── Database.cs           # Database handling
├── WorkBook.cs           # Workbook data model
├── Tasks.cs              # Tasks collection model
├── task.cs               # Individual task model
└── Resources/            # Application resources (icons, images)
```

## Data Storage

Application data is stored in XML format at:
- Windows: `%APPDATA%\ToDoHWork2\Data.xml`

## Usage

1. **Create a New Work List**: Click the green "+" button in the top-right corner
2. **Add Tasks**: Type in the text box and click "افزودن" (Add) button
3. **Mark as Complete**: Check the checkbox next to any task
4. **Delete a Task**: Click the red "X" button next to a task
5. **Delete a Work List**: Navigate to the work list you want to delete

## Technical Notes

- The application uses XML serialization for data persistence
- All dependencies are part of the standard .NET 6.0 WPF framework
- No third-party libraries required

## License

This project is provided as-is for educational and personal use.
