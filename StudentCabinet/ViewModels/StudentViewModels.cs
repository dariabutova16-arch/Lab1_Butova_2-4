using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using StudentCabinet.Models;

namespace StudentCabinet.ViewModels;

public class StudentViewModel : INotifyPropertyChanged
{
    private Student _student = new Student();

    public StudentViewModel()
    {
        AddStudentCommand = new Command(AddStudent, CanAddStudent);
    }

    public string FullName
    {
        get => _student.FullName;
        set
        {
            if (_student.FullName != value)
            {
                _student.FullName = value;
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(Greeting));
                ((Command)AddStudentCommand).ChangeCanExecute();
            }
        }
    }

    public string Group
    {
        get => _student.Group;
        set
        {
            if (_student.Group != value)
            {
                _student.Group = value;
                OnPropertyChanged(nameof(Group));
                OnPropertyChanged(nameof(Greeting));
            }
        }
    }

    public double AverageScore
    {
        get => _student.AverageScore;
        set
        {
            if (_student.AverageScore != value)
            {
                _student.AverageScore = value;
                OnPropertyChanged(nameof(AverageScore));
                OnPropertyChanged(nameof(IsGoodScore));
            }
        }
    }

    public string Greeting => $"Студент: {FullName}, група {Group}";
    public bool IsGoodScore => AverageScore >= 4.0;

    public ObservableCollection<Student> Students { get; } = new();
    public ICommand AddStudentCommand { get; }

    private void AddStudent()
    {
        Students.Add(new Student
        {
            FullName = FullName,
            Group = Group,
            AverageScore = AverageScore
        });

        FullName = string.Empty;
        Group = string.Empty;
    }

    private bool CanAddStudent() => !string.IsNullOrWhiteSpace(FullName);

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}