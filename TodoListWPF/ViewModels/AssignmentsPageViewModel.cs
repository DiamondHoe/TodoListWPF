using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TodoListWPF.Helpers;
using TodoListWPF.Models;

namespace TodoListWPF.ViewModels
{
    public class AssignmentsPageViewModel : BindableBase
    {
        private AssignmentModel selectedAssignment;
        private string editAssignmentTitle;
        private string editAssignmentDescription;

        public AssignmentsPageViewModel()
        {
            AddNewAssignmentCommand = new RelayCommand(AddNewAssignment);
            DeleteSelectedAssignmentsCommand = new RelayCommand(DeleteSelectedAssignments);
            EditAssignmentCommand = new RelayCommand(EditAssignment);
            CancelEditingAssignmentCommand = new RelayCommand(CancelEditingAssignment);

            EditAssignmentDestinatedDay = DateTime.UtcNow;

            LoadAssignmentList();
        }

        #region Public parameters
        public BindingList<AssignmentModel> AssignmentList { get; set; }
        public AssignmentModel SelectedAssignment
        {
            get
            {
                return selectedAssignment;
            }

            set
            {
                if (SetProperty<AssignmentModel>(ref selectedAssignment, value))
                {
                    LoadAssignmentToEditForm();

                    RaisePropertyChanged(nameof(AddButtonVisibility));
                    RaisePropertyChanged(nameof(ManageButtonsVisibility));
                    RaisePropertyChanged(nameof(CurrentAction));
                }
            }
        }

        public DateTime selectedDate = DateTime.UtcNow;
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }

            set
            {
                if (SetProperty<DateTime>(ref selectedDate, value))
                {
                    EditAssignmentDestinatedDay = selectedDate;
                    LoadAssignmentList();
                }
            }
        }
        public int EditAssignmentId { get; set; }
        public string EditAssignmentTitle
        {
            get => editAssignmentTitle; set
            {
                editAssignmentTitle = value;
                RaisePropertyChanged(nameof(EditAssignmentTitle));
            }
        }
        public string EditAssignmentDescription
        {
            get => editAssignmentDescription; set
            {
                editAssignmentDescription = value;
                RaisePropertyChanged(nameof(EditAssignmentDescription));
            }
        }

        public DateTime editAssignmentDestinatedDay = DateTime.Now;
        public DateTime EditAssignmentDestinatedDay
        {
            get
            {
                return editAssignmentDestinatedDay;
            }
            set
            {
                if (SetProperty<DateTime>(ref editAssignmentDestinatedDay, value))
                {
                    RaisePropertyChanged(nameof(EditAssignmentDestinatedDay));
                }

            }
        }
        #endregion

        #region UI propeties managament
        public Visibility AddButtonVisibility => SelectedAssignment == null ? Visibility.Visible : Visibility.Hidden;
        public Visibility ManageButtonsVisibility => SelectedAssignment == null ? Visibility.Hidden : Visibility.Visible;
        public string CurrentAction => SelectedAssignment == null ? "Dodawanie zadania" : "Edycja zadania";
        #endregion

        #region Commands
        public ICommand AddNewAssignmentCommand { get; set; }
        public ICommand DeleteSelectedAssignmentsCommand { get; set; }
        public ICommand EditAssignmentCommand { get; set; }
        public ICommand CancelEditingAssignmentCommand { get; set; }
        #endregion

        #region Methods
        public void LoadAssignmentList()
        {
            var assignment = new AssignmentModel();
            AssignmentList = new BindingList<AssignmentModel>(assignment.GetAllForSelectedDate(selectedDate));
            RaisePropertyChanged(nameof(AssignmentList));
        }
        private void AddNewAssignment()
        {
            var newAssignment = new AssignmentModel
            {
                Title = EditAssignmentTitle,
                Description = EditAssignmentDescription,
                CreatedDate = DateTime.Now,
                DestinatedDay = EditAssignmentDestinatedDay,
            };

            newAssignment.Create();

            EditAssignmentTitle = string.Empty;
            EditAssignmentDescription = string.Empty;

            LoadAssignmentList();
        }
        private void DeleteSelectedAssignments()
        {
            SelectedAssignment.Delete();
            LoadAssignmentList();
        }
        private void LoadAssignmentToEditForm()
        {
            if (selectedAssignment != null)
            {
                EditAssignmentTitle = SelectedAssignment.Title;
                EditAssignmentDescription = SelectedAssignment.Description;
                EditAssignmentDestinatedDay = SelectedAssignment.DestinatedDay;
            }
            else
            {
                EditAssignmentTitle = string.Empty;
                EditAssignmentDescription = string.Empty;
            }
        }
        private void EditAssignment()
        {
            SelectedAssignment.Title = EditAssignmentTitle;
            SelectedAssignment.Description = EditAssignmentDescription;
            SelectedAssignment.DestinatedDay = EditAssignmentDestinatedDay;

            SelectedAssignment.Update();

            EditAssignmentTitle = string.Empty;
            EditAssignmentDescription = string.Empty;
            LoadAssignmentList();
        }
        private void CancelEditingAssignment()
        {
            SelectedAssignment = null;
            LoadAssignmentList();
        }
        #endregion
    }
}
