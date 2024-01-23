# NotePad Clone

#### About
Notepad Clone is a modern reimagining of the classic Windows Notepad application, specifically tailored to mimic the Windows 11 version. This project was developed to enhance my software development skills, showcasing a blend of traditional text editing with a contemporary, user-friendly interface. Designed primarily for Windows 10 users, it offers a visually appealing alternative to the bright and simplistic native Notepad, with a default dark theme for a more comfortable user experience.

#### Dependencies
- `MaterialDesignColors` (Version 2.1.4)
- `MaterialDesignThemes` (Version 4.9.0)
- `WpfEssentials`: This is a custom dependency. The repository can be found at [WpfEssentials GitHub Repo](https://github.com/thomaswening/WpfEssentials). Please follow the build instructions in the repository's README to compile the library and add the resulting DLL as a reference or include the project as a shared project in the solution.

#### Building the Project
1. **Prerequisites**:
   - .NET 8.0 SDK.
   - Visual Studio 2022 or later.

2. **Clone and Setup**:
   - Clone this repository to your local machine.
   - Clone the [WpfEssentials repository](https://github.com/thomaswening/WpfEssentials), build it, and add it to the solution as mentioned above.

3. **Restore NuGet Packages**:
   - Open the solution in Visual Studio.
   - Restore NuGet packages.

4. **Build and Run**:
   - Set NotepadClone as the startup project.
   - Build and run the application with F5 or "Start".

#### Features
- Clone of the Windows 11 Notepad for Windows 10 users.
- Dark theme UI (with plans to add a toggleable theme feature).
- Basic text editing functions: open, edit, and save files.
- StatusBar with document statistics.
- Custom window chrome to resemble Windows 11 style.
- Developed using MVVM architecture for maintainability.

#### Planned features 
- *Unit tests! Because, let's be neat and tidy.*
- More items on the menu bar, e.g. 'Edit', 'View', 'Settings', ...
- Toggle to switch between bright and dark theme.
- Tabs to open several files in the same window.
- Functionality that keeps the last open documents if they have not yet been saved before the window is closed.
- Autosave feature.
- Search and replace feature.
- Possibly an alternative UI based on WinUI 3.0 but I will first have to learn using that technology.
- Whatever useful comes to mind.

## License
This project is licensed under the GPL-3.0 License - see the [LICENSE](LICENSE.txt) file for details.