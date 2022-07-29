using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;

namespace vue
{
    public class ToDo_WebPage : WebClient
    {
        public IWebElement NewTaskTextField => GetFirst(Elements.NewTaskTextField);
        public IList<IWebElement> AllTasks => Driver.FindElements(By.ClassName("view"));
        public IWebElement CheckAllTasksButton => GetFirst(Elements.CheckAllTasksButton);
        public IWebElement ItemCounter => GetFirst(Elements.ItemCounter);
        public IWebElement Tab_All => GetFirst(Elements.Tab_All);
        public IWebElement Tab_Active => GetFirst(Elements.Tab_Active);
        public IWebElement Tab_Completed => GetFirst(Elements.Tab_Completed);
        public IWebElement ClearComplitedButton => GetFirst(Elements.ClearComplitedButton);

        public static IWebElement GetFirst(By by)
        {
            return Driver.FindElement(by);
        }

        public void AddTask(string text)
        {
            NewTaskTextField.SendKeys(text);
            NewTaskTextField.SendKeys(Keys.Enter);
        }

        public void AddMoreThanOneTask(int tasksNumber, string text)
        {
            for (int i = 1; i <= tasksNumber; i++)
            {
                AddTask(text + " " + i.ToString());
            }
        }

        public IWebElement GetTaskByName(string name)
        {
            IList<IWebElement> tasks = new List<IWebElement>();
            tasks = Driver.FindElements(By.CssSelector(".view"));
            return tasks.Where(e => e.Text.Equals(name)).FirstOrDefault();
        }

        public IList<IWebElement> GetAllTasks()
        {
            return AllTasks;
        }

        public void SetTaskChecked(string name, bool complete = true)
        {
            GetTaskCheckboxByName(name).Click();
        }

        public IWebElement GetTaskCheckboxByName(string name)
        {
            return GetTaskByName(name).FindElement(By.CssSelector("input"));
        }

        public int GetTasksCount()
        {
            return  int.Parse(ItemCounter.FindElement(By.CssSelector("strong")).Text);
        }

        public void SelectAllTab()
        {
            Tab_All.Click();
        }

        public void SelectActiveTab()
        {
            Tab_Active.Click();
        }

        public void SelectCompleteTab()
        {
            Tab_Completed.Click();
        }

        public void ClearCompleted()
        {
            ClearComplitedButton.Click();
        }

        public IWebElement GetTheDeleteButtonOnTask(string name)
        {
            Actions action = new Actions(Driver);
            var task = GetTaskByName(name);
            action.MoveToElement(task).Perform();
            return task.FindElement(By.ClassName("destroy"));
        }

        public void DeleteTask(string name)
        {
            GetTheDeleteButtonOnTask(name).Click();
        }

        public void CompleteAllTasks()
        {
            CheckAllTasksButton.Click();
        }

        public void EditTaskName(string taskName, string newName)
        {
            Actions action = new Actions(Driver);
            var task = GetTaskByName(taskName).FindElement(By.CssSelector("label"));
            action.MoveToElement(task).Perform();
            action.DoubleClick(task).Perform();
            action.SendKeys(Keys.LeftControl + "a").Build().Perform();
            action.SendKeys(newName);
        }
    }
}
