using NUnit.Framework;
using System;

namespace vue
{
    public class ToDo_Tests
    {
        const string task = "Task";
        const string taskOne = "Task 1";
        const string taskTwo = "Task 2";
        const string taskThree = "Task 3";
        const string taskFour = "Task 4";
        const string taskFive = "Task 5";

        [Test]
        [Property("Test case ID", "1")]
        [Property("Test case ID", "2")]
        [Property("Test case ID", "3")]
        [Property("Test case ID", "4")]
        [Property("Test case ID", "5")]
        [Property("Test case ID", "6")]
        [Property("Test case ID", "17")]
        public void AddAndDeleteTask()
        {
            var client = new WebClient();
            int taskCounter = 0;

            Console.WriteLine("Navigate to the TODO page");
            client.StartAndNavigateToPage("https://todomvc.com/examples/vue/#/all");

            Console.WriteLine("Add a task");
            client.ToDo_WebPage.AddTask(taskOne);

            taskCounter++;

            Console.WriteLine("Refresh the page");
            client.Resfresh();

            Console.WriteLine("Verify the elements and the counter after refresh");
            Assert.Multiple(() =>
            {
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskOne).Displayed, "The task is not added");
                Assert.That(client.ToDo_WebPage.GetTaskCheckboxByName(taskOne).Enabled, "The checkbox is not enabled");
                Assert.That(client.ToDo_WebPage.Tab_All.Displayed, "The 'All' tab is not displayed");
                Assert.That(client.ToDo_WebPage.Tab_Active.Displayed, "The 'Active' tab is not displayed");
                Assert.That(client.ToDo_WebPage.Tab_Completed.Displayed, "The 'Completed' tab is not displayed");
                Assert.That(client.ToDo_WebPage.GetTheDeleteButtonOnTask(taskOne).Displayed, "The delete button is not displayed");
                Assert.That(client.ToDo_WebPage.GetTasksCount() == taskCounter, "The tasks number is wrong");
                Assert.That(client.ToDo_WebPage.ItemCounter.Text, Does.Not.Contain("items"), "The word is wrong");
            });

            Console.WriteLine("Add second task");
            client.ToDo_WebPage.AddTask(taskTwo);

            taskCounter++;

            Console.WriteLine("Verify the counter");
            Assert.That(client.ToDo_WebPage.GetTasksCount() == taskCounter, "The tasks number is wrong");

            Console.WriteLine("Delete a task");
            client.ToDo_WebPage.DeleteTask(taskTwo);

            taskCounter--;

            Console.WriteLine("Verify the counter");
            Assert.That(client.ToDo_WebPage.GetTasksCount() == taskCounter, "The tasks number is wrong");

            client.CloseBrowser();
        }

        [Test]
        [Property("Test case ID", "6")]
        [Property("Test case ID", "7")]
        [Property("Test case ID", "8")]
        [Property("Test case ID", "9")]
        [Property("Test case ID", "10")]
        public void CompleteTasks()
        {
            var client = new WebClient();

            Console.WriteLine("Navigate to the TODO page");
            client.StartAndNavigateToPage("https://todomvc.com/examples/vue/#/all");

            Console.WriteLine("Add four tasks");
            client.ToDo_WebPage.AddMoreThanOneTask(4, task);

            Console.WriteLine("Complete three tasks");
            client.ToDo_WebPage.SetTaskChecked(taskOne);
            client.ToDo_WebPage.SetTaskChecked(taskTwo);
            client.ToDo_WebPage.SetTaskChecked(taskThree);

            Console.WriteLine("Clear completed tasks");
            client.ToDo_WebPage.ClearCompleted();

            Console.WriteLine("Verify only the completed tasks are deleted");
            Assert.Multiple(() =>
            {
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskOne), Is.Null, "The task is not deleted");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskTwo), Is.Null, "The task is not deleted");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskThree), Is.Null, "The task is not deleted");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskFour).Displayed, "The task is deleted");
            });

            Console.WriteLine("Complete all tasks and clear them");
            client.ToDo_WebPage.CompleteAllTasks();
            client.ToDo_WebPage.ClearCompleted();

            Console.WriteLine("Complete all tasks and clear them");
            Assert.That(client.ToDo_WebPage.ItemCounter.Displayed, Is.False, "The tasks are not deleted");

            client.CloseBrowser();
        }

        [Test]
        [Property("Test case ID", "11")]
        [Property("Test case ID", "12")]
        [Property("Test case ID", "13")]
        [Property("Test case ID", "14")]
        public void TabsInteraction()
        {
            var client = new WebClient();

            Console.WriteLine("Navigate to the TODO page");
            client.StartAndNavigateToPage("https://todomvc.com/examples/vue/#/all");

            Console.WriteLine("Add 3 tasks");
            client.ToDo_WebPage.AddMoreThanOneTask(3, task);

            Console.WriteLine("Go to Active tab");
            client.ToDo_WebPage.SelectActiveTab();

            Console.WriteLine("Verify the three tasks are present");
            Assert.Multiple(() =>
            {
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskOne).Displayed, "The task is not found");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskTwo).Displayed, "The task is not found");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskThree).Displayed, "The task is not found");
            });

            Console.WriteLine("Add a task");
            client.ToDo_WebPage.AddTask(taskFour);

            Console.WriteLine("Verify the task appeared in the Active tab");
            Assert.That(client.ToDo_WebPage.GetTaskByName(taskFour).Displayed, "The task is not found");


            Console.WriteLine("To to completed tab");
            client.ToDo_WebPage.SelectCompleteTab();

            Console.WriteLine("Verify there are no tasks shown");
            Assert.Multiple(() =>
            {
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskOne), Is.Null, "The task is present");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskTwo), Is.Null, "The task is present");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskThree), Is.Null, "The task is present");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskFour), Is.Null, "The task is present");
            });

            Console.WriteLine("Add a task");
            client.ToDo_WebPage.AddTask(taskFive);

            Console.WriteLine("Verify the task does't appeared in the Completed tab");
            Assert.That(client.ToDo_WebPage.GetTaskByName(taskFive), Is.Null, "The task is present");

            Console.WriteLine("To to All tab and check two tasks");
            client.ToDo_WebPage.SelectAllTab();
            client.ToDo_WebPage.SetTaskChecked(taskOne);
            client.ToDo_WebPage.SetTaskChecked(taskTwo);

            Console.WriteLine("To to Active tab");
            client.ToDo_WebPage.SelectActiveTab();

            Console.WriteLine("Verify the correct tasks are shown");
            Assert.Multiple(() =>
            {
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskOne), Is.Null, "The task is present");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskTwo), Is.Null, "The task is present");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskThree).Displayed, "The task is not found");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskFour).Displayed, "The task is not found");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskFive).Displayed, "The task is not found");
            });

            Console.WriteLine("To to Completed tab");
            client.ToDo_WebPage.SelectCompleteTab();

            Console.WriteLine("Verify the correct tasks are present");
            Assert.Multiple(() =>
            {
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskOne).Displayed, "The task is not found");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskTwo).Displayed, "The task is not found");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskThree), Is.Null, "The task is present");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskFour), Is.Null, "The task is present");
                Assert.That(client.ToDo_WebPage.GetTaskByName(taskFive), Is.Null, "The task is present");
            });

            client.CloseBrowser();
        }
    }
}