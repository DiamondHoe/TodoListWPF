using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Core.Entities;
using TodoList.Database;

namespace TodoListWPF.Models
{
    public class AssignmentModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DestinatedDay { get; set; }

        #region Create methods
        public void Create() => Create(this);
        public void Create(AssignmentModel assignmentModel)
        {
            DatabaseLocator.Database.Assignments.Add(new Assignment
            {
                Title = assignmentModel.Title,
                Description = assignmentModel.Description,
                CreatedDate = assignmentModel.CreatedDate,
                DestinatedDay = assignmentModel.DestinatedDay,
            });

            DatabaseLocator.Database.SaveChanges();
        }
        #endregion

        #region Get Method
        public List<AssignmentModel> GetAllForSelectedDate(DateTime selectedDate)
        {
            return DatabaseLocator.Database.Assignments.Where(x => x.DestinatedDay == selectedDate).Select(assignment =>
                new AssignmentModel
                {
                    Id = assignment.Id,
                    Title = assignment.Title,
                    Description = assignment.Description,
                    CreatedDate = assignment.CreatedDate,
                    DestinatedDay = assignment.DestinatedDay,
                }).ToList();
        }
        #endregion

        #region Update Methods
        public void Update() => Update(this);
        public void Update(AssignmentModel assignmentModel)
        {
            var assignmentToUpdate = DatabaseLocator.Database.Assignments.Where(x => x.Id == assignmentModel.Id).FirstOrDefault();

            assignmentToUpdate.Title = assignmentModel.Title;
            assignmentToUpdate.Description = assignmentModel.Description;
            assignmentToUpdate.CreatedDate = assignmentModel.CreatedDate;
            assignmentToUpdate.DestinatedDay = assignmentModel.DestinatedDay;

            DatabaseLocator.Database.Assignments.Update(assignmentToUpdate);
            DatabaseLocator.Database.SaveChanges();
        }
        #endregion

        #region Delete Methods
        public void Delete() => Delete(Id);
        public void Delete(int id)
        {
            var assignmentToRemove = DatabaseLocator.Database.Assignments.Where(x => x.Id == id).FirstOrDefault();
            DatabaseLocator.Database.Assignments.Remove(assignmentToRemove);
            DatabaseLocator.Database.SaveChanges();
        }
        #endregion
    }
}
