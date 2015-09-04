using CrederaSampleApp.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrederaSampleApp.ViewModels {
    public class TaskViewModel {
        public TaskViewModel() {
            LoginOrCreateAccount();
            CurrentTask = new UserTask();
            UserTasks = new ObservableCollection<UserTask>();
        }
        // CURRENT TASK -> Inside TaskList
        public UserTask CurrentTask { get; set; }

        // LIST OF TASKS -> Inside TaskList:ListView
        public ObservableCollection<UserTask> UserTasks { get; set; }


        public async Task GetUserTasks() {
            // Get current user's tasks
            ParseQuery<ParseObject> userTaskQuery = new ParseQuery<ParseObject>("UserTask").WhereEqualTo("owner", ParseUser.CurrentUser);
            IEnumerable<ParseObject> poUserTasks = await userTaskQuery.FindAsync();

            // Add each task to UserTasks collection (which will be viewed in WebView)
            foreach (var poUserTask in poUserTasks) {
                UserTask toAdd = new UserTask() {
                    Title = poUserTask.Get<string>("title"),
                    Description = poUserTask.Get<string>("description"),
                    DueDate = poUserTask.Get<DateTime>("dueDate")
                };
                UserTasks.Add(toAdd);
                UserTasks.OrderBy(x => x.DueDate);
            }
        }

        // Add new UserTask to database
        public async void AddNewUserTask() {
            // new ParseObject("tableName")
            ParseObject newTask = new ParseObject(className: "UserTask");
            // parseObject["columnName"] = value
            newTask["title"] = CurrentTask.Title;
            newTask["description"] = CurrentTask.Description;
            newTask["dueDate"] = CurrentTask.DueDate;
            newTask["owner"] = ParseUser.CurrentUser;
            await newTask.SaveAsync();
            UserTasks.Add(CurrentTask);
            UserTasks.OrderBy(x => x.DueDate);
        }


        public async void LoginOrCreateAccount() {
            try {
                await ParseUser.LogInAsync("test", "test");
            }
            catch {
                ParseUser newUser = new ParseUser() {
                    Username = "test",
                    Password = "test"
                };
                await newUser.SignUpAsync();
            }
            await GetUserTasks();
        }


    }
}
